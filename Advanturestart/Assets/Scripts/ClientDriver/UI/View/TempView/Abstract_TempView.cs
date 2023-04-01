using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;

/// <summary>
/// 脚本生成代码，不允许任何手写逻辑，被覆盖不负责任。
/// </summary>
public class Abstract_TempView : UIBase
{
    #region Member
    protected GameObject image_GameObject;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        image_GameObject = transform.Find("Image").gameObject;
    }
}
