using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;

/// <summary>
/// 脚本生成代码，不允许任何手写逻辑，被覆盖不负责任。
/// </summary>
public class Abstract_ReStartView : UIBase
{
    #region Member
    protected Button start_Button;
    protected Button quit_Button;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        start_Button = transform.Find("Start").GetComponent<Button>();
        quit_Button = transform.Find("Quit").GetComponent<Button>();
    }
}
