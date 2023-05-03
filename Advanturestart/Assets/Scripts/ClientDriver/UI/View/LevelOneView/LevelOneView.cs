using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.EventSystems;
using SuperScrollView;
using UnityEngine.Video;

public class LevelOneView : Abstract_LevelOneView
{
    /// <summary>
    /// 可以在Awake中注册事件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        EventDispatcher.RegistNotify(EnumNotify.LevelOneDialog, (Action<List<string>>)SetDialogTxt,false);
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
        video_GameObject.SetActive(true);
        video_GameObject.GetComponent<VideoPlayer>().frame = 0;
        video_GameObject.GetComponent<VideoPlayer>().Play();
        SetTimeOut(21, () =>
        {
            video_GameObject.SetActive(false);
        });
    }
    /// <summary>
    /// 销毁时需要移除事件
    /// </summary>
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventDispatcher.RemoveNotify(EnumNotify.LevelOneDialog,(Action<List<string>>)SetDialogTxt);
    }

    private void SetDialogTxt(List<string> strs)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            int index = i;
            SetTimeOut(index * 2, () =>
            {
                dialogTxt_Text.text = "";
                dialogTxt_Text.DOText(strs[index], 1.9f).OnComplete(() =>
                {
                   // dialogTxt_Text.text = "";
                });
            });
        }
       
    }
}
