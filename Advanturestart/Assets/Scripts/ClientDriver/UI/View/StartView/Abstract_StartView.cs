using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;

/// <summary>
/// 脚本生成代码，不允许任何手写逻辑，被覆盖不负责任。
/// </summary>
public class Abstract_StartView : UIBase
{
    #region Member
    protected Button startButton_Button;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        startButton_Button = transform.Find("startButton").GetComponent<Button>();
    }
}
