using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using SuperScrollView;
using UnityEngine.SceneManagement;

public class LevelSixView : Abstract_LevelSixView
{
    private int EnemyCount = 0;
    /// <summary>
    /// 可以在Awake中注册事件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        EnemyCount = 4;
        EventDispatcher.RegistNotify(EnumNotify.SubEnemyCount,(Action)SubEnemyCountEvent,false);
    }

    private void SubEnemyCountEvent()
    {
        if (SceneManager.GetActiveScene().name == "GameSix")
        {
            EnemyCount--;
            if (EnemyCount == 0)
            {
                Debug.Log("当前过关");
                //PopUpManager.Instance.ShowMessage("获得二级跳");
                //EventDispatcher.Notify(EnumNotify.AddBookText,"二级跳");

                //出现漩涡们
                UIManager.Instance.OpenWindows(ResourceConst.UI.DoorView);
            }
        }
      
    }

    /// <summary>
    /// 重新打开面板
    /// </summary>
    public override void ReOpen()
    {
        base.ReOpen();
        EnemyCount = 4;

    }
    /// <summary>
    /// 销毁时需要移除事件
    /// </summary>
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
