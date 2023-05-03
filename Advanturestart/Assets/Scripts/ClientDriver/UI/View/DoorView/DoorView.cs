using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using SuperScrollView;
using UnityEngine.SceneManagement;

public class DoorView : Abstract_DoorView
{
    /// <summary>
    /// 可以在Awake中注册事件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// 重新打开面板
    /// </summary>
    public override void ReOpen()
    {
        base.ReOpen();
        if (SceneManager.GetActiveScene().name == "GameOne")
        {
            door_GameObject.transform.localPosition = new Vector3(833, -34f,
                door_GameObject.transform.localPosition.z);
            ((RectTransform)door_GameObject.transform).sizeDelta = new Vector2(254, 442);
            door_GameObject.GetComponent<BoxCollider>().size = new Vector3(
                260,
                door_GameObject.GetComponent<BoxCollider>().size.y,
                door_GameObject.GetComponent<BoxCollider>().size.z);
            return;
        }
        if (SceneManager.GetActiveScene().name == "GameTwo")
        {
            door_GameObject.transform.localPosition = new Vector3(833, 83f,
                door_GameObject.transform.localPosition.z);
            ((RectTransform)door_GameObject.transform).sizeDelta = new Vector2(254, 442);
            door_GameObject.GetComponent<BoxCollider>().size = new Vector3(
               260,
               door_GameObject.GetComponent<BoxCollider>().size.y,
                door_GameObject.GetComponent<BoxCollider>().size.z);
            return;   
        }
        if (SceneManager.GetActiveScene().name == "GameThree")
        {
             door_GameObject.transform.localPosition = new Vector3(833, -96f,
                 door_GameObject.transform.localPosition.z);
             door_GameObject.GetComponent<BoxCollider>().size = new Vector3(
                 150,
                 door_GameObject.GetComponent<BoxCollider>().size.y,
                 door_GameObject.GetComponent<BoxCollider>().size.z);
            ((RectTransform)door_GameObject.transform).sizeDelta = new Vector2(157, 255);
            // return;   
        }
        if (SceneManager.GetActiveScene().name == "GameFour")
        {
            door_GameObject.transform.localPosition = new Vector3(833, -69f,
                door_GameObject.transform.localPosition.z);
            door_GameObject.GetComponent<BoxCollider>().size = new Vector3(
               260,
                door_GameObject.GetComponent<BoxCollider>().size.y,
                door_GameObject.GetComponent<BoxCollider>().size.z);
            ((RectTransform)door_GameObject.transform).sizeDelta = new Vector2(254, 442);
            // return;   
        }
        if (SceneManager.GetActiveScene().name == "GameFive")
        {
            door_GameObject.transform.localPosition = new Vector3(524, -8f,
                door_GameObject.transform.localPosition.z);
            door_GameObject.GetComponent<BoxCollider>().size = new Vector3(
                150,
                door_GameObject.GetComponent<BoxCollider>().size.y,
                door_GameObject.GetComponent<BoxCollider>().size.z);
            ((RectTransform)door_GameObject.transform).sizeDelta = new Vector2(157, 255);
            // return;   
        }
        if (SceneManager.GetActiveScene().name == "GameSix")
        {
            door_GameObject.transform.localPosition = new Vector3(386, 419,
                door_GameObject.transform.localPosition.z);
            door_GameObject.GetComponent<BoxCollider>().size = new Vector3(
                150,
                door_GameObject.GetComponent<BoxCollider>().size.y,
                door_GameObject.GetComponent<BoxCollider>().size.z);
            ((RectTransform)door_GameObject.transform).sizeDelta = new Vector2(157, 255);
            // return;   
        }
    }
    /// <summary>
    /// 销毁时需要移除事件
    /// </summary>
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
