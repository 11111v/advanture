using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MessageItemPoolView : Abstract_MessageItemPoolView
{
    private Queue<MessageType> messages;
    protected override void Awake()
    {
        base.Awake();
        messageTemplate_GameObject.SetActive(false);
        messages = PopUpManager.Instance.GetMessageQuene();
        GameObjectPool.WarmPool(messageTemplate_GameObject, 1,parent:this.gameObject.transform);
    }
    protected override void Update()
    {
        base.Update();
        if(messages.Count>0)
        {
            var go = GameObjectPool.CreateObject(messageTemplate_GameObject, new Vector3(0,0,-300), Quaternion.identity,this.gameObject.transform);
            MessageType messageType=  messages.Dequeue();
            MessageItem messageItem = new MessageItem(go.transform);
            messageItem.msg_Text.text= messageType.msg;
            go.transform.DOLocalMoveY(140, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                GameObjectPool.ReleaseObject(go);
            });
        }
        // if(Input.GetKeyDown(KeyCode.S))
        // {
        //     PopUpManager.Instance.ShowMessage("mddd");
        // }
    }
}



