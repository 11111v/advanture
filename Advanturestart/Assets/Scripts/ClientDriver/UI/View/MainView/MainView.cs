using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using SuperScrollView;

public class MainView : Abstract_MainView
{
    /// <summary>
    /// 可以在Awake中注册事件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        bookBtn_Button.onClick.AddListener(() =>
        {
            book_GameObject.SetActive(true);
        });
        book_GameObject.GetComponent<Button>().onClick.AddListener(() => { book_GameObject.SetActive(false);});
        EventDispatcher.RegistNotify(EnumNotify.AddBookText,(Action<string>)AddBookTextEvent,false);
    }

    private void AddBookTextEvent(string obj)
    {
        bookTxt_Text.text += $"{obj}\n";
    }

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// 重新打开面板
    /// </summary>
    public override void ReOpen()
    {
        base.ReOpen();
    }
    /// <summary>
    /// 销毁时需要移除事件
    /// </summary>
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
