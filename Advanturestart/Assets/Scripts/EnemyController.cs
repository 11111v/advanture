using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // 怪物的移动速度
    public int maxHealth = 3; // 怪物的最大生命值
    public int attackDamage = 1; // 怪物的攻击伤害
    public Animator animator; // 怪物的动画控制器
    private TextMesh hpText;

    public Rigidbody rb; // 怪物的刚体组件
    [HideInInspector]
    public int currentHealth; // 怪物的当前生命值
    private bool isDead; // 怪物是否已经死亡的标志
    [HideInInspector]
    public bool isAttacking; // 怪物是否正在攻击的标志
    private int hitCount; // 怪物被攻击的次数
    private Vector3 initialPosition; // 怪物的初始位置
    public float moveRange = 2f; // 怪物的移动范围
    private float attackRange = 1f; // 怪物的攻击范围
    public int enemyIndex;
    private float moveTimer; // 怪物移动计时器
    private float moveInterval = 2f; // 怪物移动间隔
    private void Start()
    {
       // rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        initialPosition = transform.Find("Enemy").position;
        hpText = transform.Find("Enemy/Hp").GetComponent<TextMesh>();
        hpText.text = $"{currentHealth}/{maxHealth}";

        EventDispatcher.RegistNotify(EnumNotify.EnemyHurt,(Action<int>)EnemyHurt,false);
        moveTimer = moveInterval;

    }

    private void EnemyHurt(int obj)
    {
        if(obj!=enemyIndex) return;
        // 怪物自身生命值减1
        currentHealth--;
        if (currentHealth == 0)
        {
            // 怪物死亡，播放死亡动画
            isDead = true;
            hpText.text = $"0/0";
            animator.SetBool("isDead",true);
            EventDispatcher.Notify(EnumNotify.SubEnemyCount);
            StartCoroutine(Delay());
        }
        else if(currentHealth>0)
        {
            animator.SetBool("isHurt",true);
            hpText.text = $"{currentHealth}/{maxHealth}";
        }
       

    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveNotify(EnumNotify.EnemyHurt,(Action<int>)EnemyHurt);

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        DestroyImmediate(this.gameObject);
    }
    //上一次的移动方向
    private int preDir;
    private void Update()
    {
        if (!isDead)
        {
            // 如果主角不在怪物攻击范围内，则随机向左或向右移动2个单位
            if (!isAttacking)
            {
                
                moveTimer -= Time.deltaTime;
                
                if (moveTimer <= 0)
                {
                    moveTimer = moveInterval;

                    float randomValue = Random.value;
                    if (randomValue < 0.5f && transform.Find("Enemy").position.x - initialPosition.x > -moveRange)
                    {
                        transform.Find("Enemy").position += new Vector3(-moveSpeed*Time.deltaTime, 0f, 0f);
                        preDir = -1;
                    }
                    else if (transform.position.x - initialPosition.x < moveRange)
                    {
                        transform.Find("Enemy").position += new Vector3(moveSpeed*Time.deltaTime, 0f, 0f);
                        preDir = 1;
                    }
                }
                else
                {
                    if(Vector3.Distance(transform.Find("Enemy").position, initialPosition) < moveRange)
                        transform.Find("Enemy").position += new Vector3(preDir*moveSpeed*Time.deltaTime, 0f, 0f);

                }
            }
        }
    }

  


  
}