using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using SuperScrollView;
using UnityEngine.SceneManagement;

public class EndLodingView : Abstract_EndLodingView
{
    /// <summary>
    /// 可以在Awake中注册事件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
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
        animImage_GameObject.GetComponent<Animator>().Play(SceneManager.GetActiveScene().name);
        SetTimeOut(2, () =>
        {
            SceneLoaderManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            if (SceneManager.GetActiveScene().buildIndex + 1 == 2)
            {
                UIManager.Instance.CloseWindows(ResourceConst.UI.LevelOneView);

                UIManager.Instance.OpenWindows(ResourceConst.UI.LevelTwoView);
                GameObject.Find("Player").transform.position = new Vector3(-7.55f, -1.3f, 15.6f);
            }
            if (SceneManager.GetActiveScene().buildIndex + 1 == 3)
            {
                UIManager.Instance.CloseWindows(ResourceConst.UI.LevelThreeView);

                UIManager.Instance.OpenWindows(ResourceConst.UI.LevelThreeView);
                GameObject.Find("Player").transform.position = new Vector3(-8.57f, 0f, 15.6f);
            }
            if (SceneManager.GetActiveScene().buildIndex + 1 == 4)
            {
                UIManager.Instance.CloseWindows(ResourceConst.UI.LevelThreeView);

                UIManager.Instance.OpenWindows(ResourceConst.UI.LevelFourView);
                GameObject.Find("Player").transform.position = new Vector3(-9f, -1.34f, 15.6f);
            }
            if (SceneManager.GetActiveScene().buildIndex + 1 == 5)
            {
                UIManager.Instance.CloseWindows(ResourceConst.UI.LevelFourView);

                UIManager.Instance.OpenWindows(ResourceConst.UI.LevelFiveView);
                GameObject.Find("Player").transform.position = new Vector3(-5.43f, 0.31f, 15.6f);
            }
            if (SceneManager.GetActiveScene().buildIndex + 1 == 6)
            {
                UIManager.Instance.CloseWindows(ResourceConst.UI.LevelFiveView);

                UIManager.Instance.OpenWindows(ResourceConst.UI.LevelSixView);
                GameObject.Find("Player").transform.position = new Vector3(-5.73f, -1.94f, 15.6f);
            }
            if (SceneManager.GetActiveScene().buildIndex + 1 == 7)
            {
                UIManager.Instance.CloseWindows(ResourceConst.UI.LevelSixView);

                UIManager.Instance.OpenWindows(ResourceConst.UI.LevelSevenView);
                GameObject.Find("Player").transform.position = new Vector3(-5.73f, -1.41f, 15.6f);
            }
            //测试代码
            
            UIManager.Instance.CloseWindows(ResourceConst.UI.DoorView);
            this.gameObject.SetActive(false);
        });

    }
    /// <summary>
    /// 销毁时需要移除事件
    /// </summary>
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
