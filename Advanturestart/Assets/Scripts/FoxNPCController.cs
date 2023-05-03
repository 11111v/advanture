using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxNPCController : MonoBehaviour
{
    private int index = 0;
    private bool isPlay = false;

    private void OnTriggerEnter(Collider other)
    {
        if (index == 0 && !isPlay)
        {
            isPlay = true;
            List<string> strs = new List<string>();
            strs.Add("请点击F键。");
            EventDispatcher.Notify(EnumNotify.LevelTwoDialog,strs,1.0f);
            StartCoroutine(Delay(2));
        }
      
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (index == 1&&!isPlay)
            {
                isPlay = true;
                List<string> strs = new List<string>();
                strs.Add("火之祝福");
                strs.Add("");
                EventDispatcher.Notify(EnumNotify.LevelTwoDialog,strs,1.0f);
                EventDispatcher.Notify(EnumNotify.AddBookText,"火之祝福");
                //改变自身模型
                EventDispatcher.Notify(EnumNotify.ChangeNormal);
                StartCoroutine(Delay(4));
                return;
            }
            
        }
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        index++;
        if (index == 2)
        {
            UIManager.Instance.OpenWindows(ResourceConst.UI.DoorView);
        }
        isPlay = false;
    }
}
