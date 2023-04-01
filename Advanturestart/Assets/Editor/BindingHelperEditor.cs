using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BindingHelper))]
public class BindingHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {

        BindingHelper bindingHelper = (BindingHelper)target;
        if (GUILayout.Button("将临时对象增加到绑定对象中"))
        {
            if(bindingHelper.obj.obj!=null)
            {
                string formatName = bindingHelper.obj.obj.name.Substring(0, 1).ToLower() + bindingHelper.obj.obj.name.Substring(1);
                bindingHelper.obj.name = $"{formatName}_{bindingHelper.obj.type.ToString()}";
                bindingHelper.objsBinding.Add(bindingHelper.obj);
                bindingHelper.obj = null;
            }
        }

        base.OnInspectorGUI();
       
    }
}
