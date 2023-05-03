using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //如果触发的时武器，并且武器正在攻击状态
        if (other.gameObject.tag == "Enemy" )
        {
            var enemyController = other.GetComponent<EnemyController>();
            if (enemyController == null)
                enemyController=other.GetComponent<EnemyHurtController>()?.enemyController;
            if (enemyController == null)
                other.transform.parent.parent.parent?.parent?.GetComponent<EnemyController>();
            if (enemyController == null)
                other.transform.parent.parent.GetComponent<EnemyController>();
            
            if (enemyController?.currentHealth > 0)
            {
                Debug.Log("受到敌人伤害");
                // 主角受到攻击，生命值减1
                EventDispatcher.Notify(EnumNotify.PlayerHurt, 1);
                // EventDispatcher.Notify(EnumNotify.EnemyHurt,enemyIndex);
            }
           
        }
    }
}
