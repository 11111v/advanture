using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;

/// <summary>
/// 脚本生成代码，不允许任何手写逻辑，被覆盖不负责任。
/// </summary>
public class Abstract_MainView : UIBase
{
    #region Member
    protected GameObject book_GameObject;
    protected Text bookTxt_Text;
    protected Button bookBtn_Button;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        book_GameObject = transform.Find("Book").gameObject;
        bookTxt_Text = transform.Find("Book/BookTxt").GetComponent<Text>();
        bookBtn_Button = transform.Find("BookBtn").GetComponent<Button>();
    }
}
