using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using SuperScrollView;
using UnityEngine.SceneManagement;

public class LevelSevenView : Abstract_LevelSevenView
{
    private int EnemyCount = 7;
    /// <summary>
    /// 可以在Awake中注册事件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        EnemyCount = 7;
        EventDispatcher.RegistNotify(EnumNotify.SubEnemyCount,(Action)SubEnemyCountEvent,false);
    }

    private void SubEnemyCountEvent()
    {
        if (SceneManager.GetActiveScene().name == "GameSeven")
        {
            EnemyCount--;
            Debug.Log(":"+EnemyCount);
            if (EnemyCount == 0)
            {
                Debug.Log("当前过关");
                PopUpManager.Instance.ShowMessage("获得记忆水晶片");
                UIManager.Instance.OpenWindows(ResourceConst.UI.ReStartView);

                //EventDispatcher.Notify(EnumNotify.AddBookText,"二级跳");

                //出现漩涡们
                //UIManager.Instance.OpenWindows(ResourceConst.UI.DoorView);
            }
        }
      
    }

    /// <summary>
    /// 重新打开面板
    /// </summary>
    public override void ReOpen()
    {
        base.ReOpen();
        EnemyCount = 7;

    }
    /// <summary>
    /// 销毁时需要移除事件
    /// </summary>
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
