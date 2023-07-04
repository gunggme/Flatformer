using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossKnight : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CapsuleCollider2D coll;
    private Animator anim;

    public DetectionZone detectZone;
    
    public GameObject player;
    public BossDoorColor bossDoor;

    private bool isSpawn = false;
    private bool isFalled = false;
    private bool isDead = false;

    private readonly int[] attackAnimation =
        { Animator.StringToHash("Attack1"), Animator.StringToHash("Attack2"), Animator.StringToHash("Attack3") };

    // 보스 스폰은 외부 오브젝트로 구현 부탁.
    public void BossSpawn()
    {
        Debug.Log("BossSpawn");
        transform.position = new Vector2(180, 50);
        gameObject.SetActive(true);
        rigid.velocity = Vector2.down;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!isSpawn || isDead)
            return;
        if (!isFalled)
        {
            isFalled = true;
            anim.SetTrigger("StopFall");
            StartCoroutine(Pattern(true));
            return;
        }
        
        LookPos();
        Move();
        Roll();
    }

    private void LookPos()
    {
        if (isAttacking || isRolling || isBlocking)
            return;
        int xVal = transform.position.x - player.transform.position.x >= 0 ? -2 : 2;
        transform.localScale =  new Vector2(xVal, 2f);
    }

    #region Move
    private readonly int isRunning = Animator.StringToHash("isRunning");
    private float noMove = 0f;
    private bool isMoving = false;
    private void Move()
    {
        if (noMove > 0f)
        {
            noMove -= Time.fixedDeltaTime;
            return;
        }

        float dirX = transform.localScale.x * Time.fixedDeltaTime * 65;
        if (isAttacking || isRolling || isBlocking || detectZone.detectedCollider.Count > 0)
            dirX = 0f;

        anim.SetBool(isRunning, dirX != 0f);
        isMoving = dirX != 0f;
        rigid.velocity = Vector2.right * dirX;
    }
    #endregion

    #region Roll
    private readonly int roll = Animator.StringToHash("Roll");
    private bool isRolling = false;
    private float rollCoolTime = 0f;
    private void Roll()
    {
        if (isRolling)
            rigid.velocity = transform.localScale.x * Time.fixedDeltaTime * 300f * Vector2.right;
        
        if (isAttacking || isBlocking)
            return;
        if (rollCoolTime > 0f)
        {
            rollCoolTime -= Time.fixedDeltaTime;
            return;
        }

        if (Vector2.Distance(transform.position, player.transform.position) > 8.5f && !isRolling)
        {
            rollCoolTime = 5f;
            anim.SetTrigger(roll);
            isRolling = true;
        }
    }
    private void StopRoll()
    {
        rigid.velocity = Vector2.zero;
        isRolling = false;
        
        Attack(UnityEngine.Random.Range(0, 3));
    }
    #endregion
    
    private readonly WaitForSeconds[] waitTimes = {new(1f), new(2f), new(3f), new(4f)};
    private IEnumerator Pattern(bool firstWait)
    {
        if (firstWait)
            yield return waitTimes[0];
        while (isAttacking || isRolling || isBlocking || isMoving)
        {
            yield return null;
        }

        if (!isRolling)
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                case 1: //Attack
                    for (int i = 0; i < UnityEngine.Random.Range(0, 3); i++)
                        Attack(i);
                    break;
                
                case 2: //Block
                    Block();
                    yield return waitTimes[UnityEngine.Random.Range(1, 4)];
                    Block();
                    
                    break;
            }

        yield return waitTimes[1];
        
        StartCoroutine(Pattern(false));
    }
    
    #region Attack
    private void Attack(int atkIdx)
    {
        anim.SetTrigger(attackAnimation[atkIdx]);
    }
    private bool isAttacking = false;
    private void AttackTrue()
    {
        isAttacking = true;
    }
    private void AttackFalse()
    {
        isAttacking = false;
    }
    #endregion

    #region Block
    private bool isBlocking = false;

    public bool IsBlocking
    {
        get => isBlocking;
    }
    
    private readonly int block = Animator.StringToHash("Block");
    private readonly int onBlocked = Animator.StringToHash("onBlocked");
    private void Block()
    {
        isBlocking = !isBlocking;
        anim.SetBool(block, isBlocking);
    }
    #endregion

    private readonly int hurt = Animator.StringToHash("On3Attack");
    private void KnockBackBoss()
    {
        anim.SetTrigger(hurt);
    }

    public void Death()
    {
        isDead = true;
        bossDoor.Open();
        StopAllCoroutines();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isSpawn)
            return;
        isSpawn = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform trans = transform;
        Vector2 pos = trans.position;
        Vector2 scale = trans.localScale;
        float dirX = player.transform.position.x - pos.x;

        bool isLooking = (dirX >= 0 && scale.x >= 0) || (dirX < 0 && scale.x < 0);
        if (isBlocking && isLooking)
        {
            anim.SetTrigger(onBlocked);
            return;
        }

        
        if (other.name == "SwordAttack3")
        {
            StopCoroutine(Pattern(false));
            StopAllCoroutines();
            isMoving = false;
            isRolling = false;
            isBlocking = false;
            anim.SetBool(block, false);
            isAttacking = false;
            KnockBackBoss();
            StartCoroutine(Pattern(true));
        }
    }
}
