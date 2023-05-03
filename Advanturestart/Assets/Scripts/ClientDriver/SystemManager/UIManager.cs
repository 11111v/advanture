using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, UIBase> storeInMemoryHash = new Dictionary<string, UIBase>(); //存储在内存的UI
    private GameObject uIRoot;
    public void Init()
    {
        storeInMemoryHash.Clear();
        uIRoot = GameObject.FindGameObjectWithTag("UIRoot");
        for(int i=0;i<uIRoot.transform.childCount;i++)
        {
            if (uIRoot.transform.GetChild(i).GetComponent(uIRoot.transform.GetChild(i).name) == null)
                uIRoot.transform.GetChild(i).gameObject.AddComponent(Assembly.GetExecutingAssembly().GetType(uIRoot.transform.GetChild(i).name));
            if(uIRoot.transform.GetChild(i).GetComponent<UIBase>()!=null)
                storeInMemoryHash.Add(uIRoot.transform.GetChild(i).name, uIRoot.transform.GetChild(i).GetComponent<UIBase>());
            //Debug.Log($"已经添加:{uIRoot.transform.GetChild(i).name}");
        }
    }
    public UIBase OpenWindows(string uiName, Action<GameObject> CallBack = null,bool isInitTransform=true, bool isOpen=true)
    {
        UIBase uiBase = null;
        if (storeInMemoryHash.ContainsKey(uiName))
        {
            storeInMemoryHash[uiName].gameObject.SetActive(isOpen);
            storeInMemoryHash[uiName].ReOpen();
            if (CallBack != null)
                CallBack(storeInMemoryHash[uiName].gameObject);
            return storeInMemoryHash[uiName] as UIBase;
        }
        GameObject Prefab = (GameObject)Resources.Load("Prefabs/UI/" + uiName);
        if (Prefab != null)
        {
            if(uIRoot==null)
                uIRoot = GameObject.FindGameObjectWithTag("UIRoot");
            GameObject gameObject = GameObject.Instantiate(Prefab, uIRoot.transform);
            if(uIRoot!=null&&isInitTransform)
            {
                gameObject.transform.SetParent(uIRoot.transform);
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;
                gameObject.transform.localScale = Vector3.one;
            }
            else
                Debug.LogError("UIRoot is null");
            uiBase = gameObject.GetComponent<UIBase>();
            gameObject.SetActive(isOpen);
            storeInMemoryHash.Add(uiName, uiBase);
            if (CallBack != null)
                CallBack(gameObject);
            return uiBase;
        }
        Debug.LogError("创建的预制体不存在");
        return null;

    }
    public void CloseWindows(string uiName)
    {
        if (storeInMemoryHash.ContainsKey(uiName))
        {
            storeInMemoryHash[uiName].gameObject.SetActive(false);
            return;
        }
        Debug.LogError("没有此ui或此ui未加载过");
    }
    public UIBase GetWindow(string uiName)
    {
        if (storeInMemoryHash.ContainsKey(uiName))
        {
            return storeInMemoryHash[uiName];
        }
        Debug.LogError("没有此ui或此ui未加载过");
        return null;
    }
    public bool IsUiOpening(string uiName)
    {
        if (storeInMemoryHash.ContainsKey(uiName))
        {
            return storeInMemoryHash[uiName].gameObject.activeSelf;
        }
        return false;
    }
}
