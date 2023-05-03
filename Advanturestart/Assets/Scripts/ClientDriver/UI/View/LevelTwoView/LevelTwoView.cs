using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.EventSystems;
using SuperScrollView;

public class LevelTwoView : Abstract_LevelTwoView
{
    /// <summary>
    /// 可以在Awake中注册事件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        EventDispatcher.RegistNotify(EnumNotify.LevelTwoDialog, (Action<List<string>,float>)SetDialogTxt,false);

    }

    private void SetDialogTxt(List<string> strs,float time)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            int index = i;
            //默认1.9f
            SetTimeOut(index * (time+0.1f), () =>
            {
                dialogTxt_Text.text = "";
                dialogTxt_Text.DOText(strs[index], time).OnComplete(() =>
                {
                    // dialogTxt_Text.text = "";
                });
            });
        }
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
        List<string> strs = new List<string>();
        strs.Add("我：有只狐狸？？？\n狐狸石碑：使用赋予你的能力，击败他们，找回记忆吧。");
        strs.Add("");

        SetDialogTxt(strs,1.9f);
        //EventDispatcher.Notify(EnumNotify.LevelOneDialog,strs);
    }
    /// <summary>
    /// 销毁时需要移除事件
    /// </summary>
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
