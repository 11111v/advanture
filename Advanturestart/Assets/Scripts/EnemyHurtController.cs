using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtController : MonoBehaviour
{
    public float attackDistance=1.7f;
    public int enemyIndex;
    public EnemyController enemyController;
    private void Awake()
    {
        UIManager.Instance.OpenWindows(ResourceConst.UI.MessageItemPoolVIew);

    }

    private void OnTriggerEnter(Collider other)
    {
        //如果触发的时武器，并且武器正在攻击状态
      
        if (other.gameObject.tag == "Weapon")
        {
            var currentAnimatorStateInfo = other.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            float distanceToPlayer = Vector3.Distance(transform.parent.position, other.transform.position);

            if (currentAnimatorStateInfo.IsName("attack1") || currentAnimatorStateInfo.IsName("attack2"))
            {
                Debug.Log("受到weapon");
                EventDispatcher.Notify(EnumNotify.EnemyHurt,enemyIndex);
                return;
            }
           
        }
        // 如果主角进入了怪物的攻击范围，则怪物开始攻击主角
        if ((other.CompareTag("Player")|| (other.transform.parent!=null&&other.transform.parent.CompareTag("Player")))&&enemyController.currentHealth>0)
        {
            float distanceToPlayer = Vector3.Distance(transform.parent.position, other.transform.position);
            if (distanceToPlayer < attackDistance&& !enemyController.isAttacking)
            {
                enemyController.isAttacking = true;
                AttackPlayer(other.gameObject);
            }
        }
    }
    
    private void AttackPlayer(GameObject player)
    {
        // 怪物向主角发起攻击
        enemyController.animator.Play("attack1");
        enemyController.isAttacking = false;

        // 主角受到攻击，生命值减1
        //EventDispatcher.Notify(EnumNotify.PlayerHurt, 1);
        
        
    }

}
