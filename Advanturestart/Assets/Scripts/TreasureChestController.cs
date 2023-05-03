using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PopUpManager.Instance.ShowMessage("获得二段跳");
        
        EventDispatcher.Notify(EnumNotify.GetTwoJump);
        this.gameObject.SetActive(false);
    }
}
