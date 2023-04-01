using System;
using System.Collections;
using System.Collections.Generic;
public enum EnumNotify
{
    TEST1,
    TEST2,
}
public class EventDispatcher
{
    private static readonly Dictionary<EnumNotify, HashSet<MulticastDelegate>> m_EventMap = new Dictionary<EnumNotify, HashSet<MulticastDelegate>>();
    private static bool m_isRuning = false;
    private static Action m_endAction = null;
    private static Dictionary<EnumNotify, MulticastDelegate> m_notifyDict = new Dictionary<EnumNotify, MulticastDelegate>();
    #region register and remove
    //1.查询m_EventMap里是否有这个消息名，没有就给他添加一个消息名和多播委托
    //2.查询多播委托里是否有这个事件，如果没有就注册
    private static void RegisterNotify(EnumNotify evtName, MulticastDelegate evt)
    {
        HashSet<MulticastDelegate> evts;
        if (m_EventMap.TryGetValue(evtName, out evts))
        {
            if (evts == null)
            {
                evts = new HashSet<MulticastDelegate>();
                m_EventMap[evtName] = evts;
            }
        }
        else
        {
            evts = new HashSet<MulticastDelegate>();
            m_EventMap.Add(evtName, evts);
        }

        if (!evts.Contains(evt))
        {
            if (m_isRuning)
            {
                m_endAction += () => { evts.Add(evt); };
            }
            else
            {
                evts.Add(evt);
            }
        }
    }


    public static void RemoveNotify(EnumNotify evtName, MulticastDelegate evt)
    {
        if (evt == null)
        {
            return;
        }
        HashSet<MulticastDelegate> evts;
        if (m_EventMap.TryGetValue(evtName, out evts))
        {
            if (evts != null)
            {
                if (m_isRuning)
                {
                    m_endAction += () => { evts.Remove(evt); };
                }
                else
                {
                    evts.Remove(evt);
                }
            }
        }
    }
    #endregion

    #region notify
    //发消息并直接运行委托中的方法
    public static void Notify(EnumNotify evtName)
    {
        m_isRuning = true;
        HashSet<MulticastDelegate> evts;
        if (m_EventMap.TryGetValue(evtName, out evts))
        {
            var itr = evts.GetEnumerator();
            while (itr.MoveNext())
            {
                Action act = itr.Current as Action;
                if (act != null)
                {
                    try
                    {
                        act();
                    }
                    catch
                    {

                    }
                }
            }
            itr.Dispose();
        }
        m_isRuning = false;
        RunEndAction();
    }
    public static void Notify<T>(EnumNotify evtName, T V1)
    {
        m_isRuning = true;
        HashSet<MulticastDelegate> evts;
        if (m_EventMap.TryGetValue(evtName, out evts))
        {
            var itr = evts.GetEnumerator();
            while (itr.MoveNext())
            {
                Action<T> act = itr.Current as Action<T>;
                if (act != null)
                {
                    act(V1);
                }
            }
            itr.Dispose();
        }
        m_isRuning = false;
        RunEndAction();
    }
    public static void Notify<T, U>(EnumNotify evtName, T V1, U V2)
    {
        m_isRuning = true;
        HashSet<MulticastDelegate> evts;
        if (m_EventMap.TryGetValue(evtName, out evts))
        {
            var itr = evts.GetEnumerator();
            while (itr.MoveNext())
            {
                Action<T, U> act = itr.Current as Action<T, U>;
                if (act != null)
                {
                    act(V1, V2);
                }
            }
            itr.Dispose();
        }
        m_isRuning = false;
        RunEndAction();
    }
    public static void Notify<T, U, V>(EnumNotify evtName, T V1, U V2, V V3)
    {
        m_isRuning = true;
        HashSet<MulticastDelegate> evts;
        if (m_EventMap.TryGetValue(evtName, out evts))
        {
            var itr = evts.GetEnumerator();
            while (itr.MoveNext())
            {
                Action<T, U, V> act = itr.Current as Action<T, U, V>;
                if (act != null)
                {
                    act(V1, V2, V3);
                }
            }
            itr.Dispose();
        }
        m_isRuning = false;
        RunEndAction();
    }
    public static void Notify<T, U, V, W>(EnumNotify evtName, T V1, U V2, V V3, W V4)
    {
        m_isRuning = true;
        HashSet<MulticastDelegate> evts;
        if (m_EventMap.TryGetValue(evtName, out evts))
        {
            var itr = evts.GetEnumerator();
            while (itr.MoveNext())
            {
                Action<T, U, V, W> act = itr.Current as Action<T, U, V, W>;
                if (act != null)
                {
                    act(V1, V2, V3, V4);
                }
            }
            itr.Dispose();
        }
        m_isRuning = false;
        RunEndAction();
    }
    #endregion

    private static void RunEndAction()
    {
        Action action = m_endAction;
        m_endAction = null;
        if (action != null)
        {
            action();
        }
    }


    #region 对外接口
    public static void RemoveAllNotify()
    {
        var itr = m_notifyDict.GetEnumerator();
        while (itr.MoveNext())
        {
            EventDispatcher.RemoveNotify(itr.Current.Key, itr.Current.Value);
        }
        itr.Dispose();
        m_notifyDict.Clear();
    }

    //供外部使用的事件注册
    public static void RegistNotify(EnumNotify notify, MulticastDelegate action,bool isDelete=true)
    {
        // 防止一个事件反复注册，造成删除不掉
        MulticastDelegate oldAction;
        if (!m_notifyDict.TryGetValue(notify, out oldAction))
        {
            m_notifyDict.Add(notify, action);
            RegisterNotify(notify, action);
        }
        else
        {
            // 防止反复注册
            if(isDelete)
            {

                m_notifyDict[notify] = action;
                RemoveNotify(notify, oldAction);
            }
            RegisterNotify(notify, action);
        }
    }
    #endregion
}


