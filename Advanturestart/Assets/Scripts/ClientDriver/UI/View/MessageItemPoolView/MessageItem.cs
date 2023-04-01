using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageItem 
{
    public Text msg_Text;
    public MessageItem(Transform t)
    {
        msg_Text = t.Find("msg").GetComponent<Text>();
    }
}
