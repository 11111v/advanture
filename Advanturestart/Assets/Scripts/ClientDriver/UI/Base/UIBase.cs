using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIBase : MonoBehaviour
{
    protected PETimer pt = new PETimer();

    /// <summary>
    /// 延迟调用
    /// </summary>
    /// <param name="delay">延迟时间</param>
    /// <param name="action">延迟调用事件</param>
    /// <returns></returns>
    protected Coroutine SetTimeOut(float delay, Action action)
    {
        Coroutine result = StartCoroutine(TimeOutExcute(delay,action));
        return result;
    }
    private IEnumerator TimeOutExcute(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        if (action != null)
        {
            action();
        }
    }
    public virtual void ReOpen()
    {

    }
    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        pt.Update();
    }
    protected virtual void OnDestroy()
    {
     
    }
    /// <summary>
    /// 为EventTrigger添加事件及事件监听。
    /// </summary>
    /// <param name="obj">带有或需要添加EventTrigger的对象</param>
    /// <param name="eventType">要添加的事件的类型</param>
    /// <param name="callback">事件的触发回调</param>
    public void AddUIEventTriggerEvent(GameObject obj, EventTriggerType eventType, UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = null;
        EventTrigger trigger = obj.GetComponent<EventTrigger>();

        if (trigger != null) // 已有EventTrigger
        {
            // 查找是否已经存在要注册的事件
            foreach (EventTrigger.Entry existingEntry in trigger.triggers)
            {
                if (existingEntry.eventID == eventType)
                {
                    entry = existingEntry;
                    break;
                }
            }
        }
        else // 添加新的EventTrigger
        {
            trigger = obj.gameObject.AddComponent<EventTrigger>();
        }

        // 如果这个事件不存在，就创建新的实例
        if (entry == null)
        {
            entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback = new EventTrigger.TriggerEvent();
        }

        // 添加触发回调并注册事件
        entry.callback.AddListener(callback);
        trigger.triggers.Add(entry);
    }
    /// <summary>
    /// 移除单个类型事件
    /// </summary>
    public void RemoveUIEventTriggerEvent(GameObject obj, EventTriggerType eventType)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = null;
        if (trigger == null)
            return;
        // 查找是否已经存在要注册的事件
        foreach (EventTrigger.Entry existingEntry in trigger.triggers)
        {
            if (existingEntry.eventID == eventType)
            {
                entry = existingEntry;
                break;
            }
        }
        if(entry!=null)
        {
            //移除单个
            //entry.callback.RemoveListener(callback);

            entry.callback.RemoveAllListeners();
        }
    }

    /// <summary>
    /// 移除所有事件
    /// </summary>
    public void RemoveAllUIEventTriggerEvent(GameObject obj)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
            return;
        // 查找是否已经存在要注册的事件

        trigger.triggers.Clear();
    }
}
