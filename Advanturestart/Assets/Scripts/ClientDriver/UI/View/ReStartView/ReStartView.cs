using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using SuperScrollView;

public class ReStartView : Abstract_ReStartView
{
    /// <summary>
    /// 可以在Awake中注册事件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        start_Button.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            DestroyImmediate(GameObject.Find("Player"));
            UIManager.Instance.CloseWindows(ResourceConst.UI.LevelThreeView);
            UIManager.Instance.CloseWindows(ResourceConst.UI.LevelOneView);
            UIManager.Instance.CloseWindows(ResourceConst.UI.LevelTwoView);
            UIManager.Instance.CloseWindows(ResourceConst.UI.LevelFourView);
            UIManager.Instance.CloseWindows(ResourceConst.UI.LevelFiveView);
            UIManager.Instance.CloseWindows(ResourceConst.UI.LevelSixView);
            UIManager.Instance.CloseWindows(ResourceConst.UI.LevelSevenView);
            UIManager.Instance.OpenWindows(ResourceConst.UI.LevelOneView);
            SceneLoaderManager.Instance.LoadScene(1);
            this.gameObject.SetActive(false);
            
        });
        quit_Button.onClick.AddListener(() =>
        {
            Application.Quit();

        });
        
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
