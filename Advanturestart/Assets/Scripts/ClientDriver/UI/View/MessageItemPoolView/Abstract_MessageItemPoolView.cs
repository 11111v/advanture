using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Abstract_MessageItemPoolView : UIBase
{
    protected GameObject messageTemplate_GameObject;
    protected override void Awake()
    {
        base.Awake();
        messageTemplate_GameObject = transform.Find("messageTemplate").gameObject;
    }
}



