using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移动速度

    private void Start()
    {
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // 获取水平方向的输入值
        transform.position += new Vector3(horizontal, 0, 0) * moveSpeed * Time.deltaTime; // 更新物体位置
    }
}