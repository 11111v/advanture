using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;

/// <summary>
/// 脚本生成代码，不允许任何手写逻辑，被覆盖不负责任。
/// </summary>
public class Abstract_LoginView : UIBase
{
    #region Member
    protected Text title_Text;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        title_Text = transform.Find("Left/Title").GetComponent<Text>();
    }
}
