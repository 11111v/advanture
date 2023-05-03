using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;

/// <summary>
/// 脚本生成代码，不允许任何手写逻辑，被覆盖不负责任。
/// </summary>
public class Abstract_LevelOneView : UIBase
{
    #region Member
    protected Text dialogTxt_Text;
    protected GameObject video_GameObject;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        dialogTxt_Text = transform.Find("DialogTxt").GetComponent<Text>();
        video_GameObject = transform.Find("Video").gameObject;
    }
}
