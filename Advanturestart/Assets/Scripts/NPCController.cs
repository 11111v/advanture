using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private int index = 0;
    private bool isPlay = false;
    
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(111);
            if (index == 0&&!isPlay)
            {
                isPlay = true;
                List<string> strs = new List<string>();
                strs.Add("你：我为什么会变成这个鬼样子，你们对我做了什么？？？");
                strs.Add("引导石碑：请到我这里来.......冒险者，我对你并无恶意，我只是想帮助你。");
                strs.Add("请点击对话处或者按F键。");
                EventDispatcher.Notify(EnumNotify.LevelOneDialog,strs);
                StartCoroutine(Delay());
                return;
            }
            if (index == 1&&!isPlay)
            {
                isPlay = true;
                List<string> strs = new List<string>();
                strs.Add("引导石碑：你的身体已经不能够再支撑你的记忆了，你应该比我更清楚你现在");
                strs.Add("的病情和身体状况，对吗？你现在还记得你自己的名字和所经历过的事情吗？");
                strs.Add("你：你说的帮助......是什么...");
                strs.Add("引导石碑：向前看。在你的前方。“");
                strs.Add("");
                EventDispatcher.Notify(EnumNotify.LevelOneDialog,strs);
                StartCoroutine(Delay());
                return;
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(6);
        index++;
        if (index == 2)
        {
            UIManager.Instance.OpenWindows(ResourceConst.UI.DoorView);
        }
        isPlay = false;
    }
}
