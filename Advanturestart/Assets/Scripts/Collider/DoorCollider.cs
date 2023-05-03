using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.OpenWindows(ResourceConst.UI.EndLodingView);
        
    }


}
