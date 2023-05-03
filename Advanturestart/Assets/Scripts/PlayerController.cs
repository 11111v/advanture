using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isOnStep = false; // 角色是否在台阶上面的标志

    public float moveSpeed = 5.0f; // 移动速度
    public float jumpSpeed = 7f; // 跳跃速度
    private TextMesh hpText;
    private int hp = 4;
    private int currentHp;
    public bool isNormal=false;
    private Animator normalAnim;
    private float jumpTimer = 0;
    private bool isTwoJump=false;
    private GameObject controllerObj;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        normalAnim = transform.Find("normal").GetComponent<Animator>();
        // transform.Find("normal").gameObject.SetActive(false);
        // transform.Find("small").gameObject.SetActive(true);
        // controllerObj = transform.Find("small").gameObject;
        controllerObj = transform.Find("normal").gameObject;
        hpText = transform.Find("Hp").GetComponent<TextMesh>();
        currentHp = hp;
        hpText.text = $"{currentHp}/{hp}";
        EventDispatcher.RegistNotify(EnumNotify.GetTwoJump,(Action)GetTwoJump);

        EventDispatcher.RegistNotify(EnumNotify.ChangeNormal,(Action)ChangeNormal);
        EventDispatcher.RegistNotify(EnumNotify.PlayerHurt,(Action<int>)PlayerHurt);
    }

    private void GetTwoJump()
    {
        isTwoJump = true;
        jumpSpeed = 9.5f;
    }

    private void PlayerHurt(int obj)
    {
        if (currentHp - obj > 0)
        {
            currentHp -= obj;
            Hurt();
        }
        else
        {
            currentHp = 0;
            Die();
        }

       
        hpText.text = $"{currentHp}/{hp}";
    }

    private void ChangeNormal()
    {
        isNormal = true;
        transform.Find("normal").gameObject.SetActive(true);
        transform.Find("small").gameObject.SetActive(false);
        
        controllerObj = transform.Find("normal").gameObject;

    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // 获取水平方向的输入值
        transform.position += new Vector3(horizontal, 0, 0) * moveSpeed * Time.deltaTime; // 更新物体位置
        jumpTimer += Time.deltaTime;
        bool jump = Input.GetKeyDown(KeyCode.Space);
            
        if (horizontal < 0)
        {
            controllerObj.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if(horizontal>0)
        {
            controllerObj.transform.localEulerAngles = new Vector3(0, -90, 0);
        }
        else
        {
            controllerObj.transform.localEulerAngles = new Vector3(0, 0, 0);

        }
        
        normalAnim?.SetBool("isWalking", horizontal != 0);
        //normalAnim?.SetBool("isJumping", jump);
        if (jump&& jumpTimer>2)
        {
            jumpTimer = 0;
            //transform.position += new Vector3(0, 1, 0) * jumpSpeed * Time.deltaTime; 
            JumpAction();
        }
        //释放攻击
        if(isNormal&& Input.GetMouseButtonUp(0))
        {
            normalAnim.Play("attack1");
            //normalAnim.SetBool("isAttacking1", true);
        }
        //释放攻击
        if(isNormal&& Input.GetMouseButtonUp(1))
        {
            normalAnim.Play("attack2");
        }
        //normalAnim.SetBool("isAttacking", attack);
        //跳跃到台阶上
        if(this.transform.GetComponent<SphereCollider>().isTrigger){
            if(this.transform.GetComponent<Rigidbody>().velocity.y<=0f){
                RaycastHit tempRaycastHit;
                if(Physics.Raycast(this.transform.Find("normal/Bip001").position,Vector3.down,out tempRaycastHit,0.05f)){
                    transform.GetComponent<SphereCollider>().isTrigger = false;
                    
                }
            }
        }
    }
    private void JumpAction()
    {
        
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        transform.GetComponent<SphereCollider>().isTrigger = true;
    }
    // 角色受伤
    public void Hurt()
    {
        normalAnim.SetBool("isHurt", true);
        Invoke("EndHurt", 0.5f);
    }

    private void EndHurt()
    {
        normalAnim.SetBool("isHurt", false);
    }

    // 角色死亡
    public void Die()
    {
        
        normalAnim.SetBool("isDead", true);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
       
        UIManager.Instance.OpenWindows(ResourceConst.UI.ReStartView);
        Time.timeScale = 0;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            transform.GetComponent<SphereCollider>().isTrigger = false;

            isOnStep = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Step"))
        {
            isOnStep = false;
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveNotify(EnumNotify.ChangeNormal,(Action)ChangeNormal);
        EventDispatcher.RemoveNotify(EnumNotify.PlayerHurt,(Action<int>)PlayerHurt);
    }
}