using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossKnight : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CapsuleCollider2D coll;
    private Animator anim;

    public DetectionZone detectZone;
    public GameObject player;

    private bool isSpawn;
    private bool isTouched;

    public int atkIndex;

    private int[] attackAnimation =
        { Animator.StringToHash("Attack1"), Animator.StringToHash("Attack2"), Animator.StringToHash("Attack3") };

    // 보스 스폰은 외부 오브젝트로 구현 부탁.
    public void BossSpawn()
    {
        Debug.Log("BossSpawn");
        transform.position = new Vector2(180, 50);
        gameObject.SetActive(true);
        // velocity.y != 0 이 되게
        rigid.velocity = Vector2.down;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack(atkIndex);
        }
    }

    private void FixedUpdate()
    {
        if (!isTouched || isDead)
            return;
        if (!isSpawn)
        {
            isSpawn = true;
            anim.SetTrigger("StopFall");
            // StartCoroutine(Pattern());
            return;
        }

        LookPoint();
        Move();
        Roll();
    }

    private void LookPoint()
    {
        if (isAttack || isRollin)
            return;
        if (transform.position.x - player.transform.position.x >= 0)
            transform.localScale = new Vector2(-2f, 2f);
        else
            transform.localScale = new Vector2(2f, 2f);
    }

    private readonly int move = Animator.StringToHash("Move");
    private bool isMove = false;
    private void Move()
    {
        if (detectZone.detectedCollider.Count > 0)
        {
            isMove = false;
            rigid.velocity = Vector2.zero;
        }
        else
        {
            isMove = true;
            rigid.velocity = 3f * transform.localScale.x * Vector2.right;
        }
<<<<<<< HEAD
        // else
        // {
        //     isMove = false;
        //     rigid.velocity = Vector2.zero;
        // }
        anim.SetBool(move, isMove);
    } 
=======
    }

    
    private bool onBlock = false;
    public void OnBlock()
    {
        onBlock = true;
        blockTime = 3f;
    }
    private int block = Animator.StringToHash("Block");
    private float blockTime = 0f;
    private float blockCool = 0f;
    
    //끝나는 시간 추가 개선
    private void Block()
    {
        if (blockCool > 0f)
        {
            blockCool -= Time.fixedDeltaTime;
            return;
        }
        if (!onBlock || blockTime < 0f || detectZone.detectedCollider.Count == 0)
            return;
        
        blockTime -= Time.fixedDeltaTime;
        anim.SetBool(block, true);
    }
>>>>>>> 295f9f1df3ec097dec4fc9ee025e90ec364d840a

    #region Roll
    
    private readonly int roll = Animator.StringToHash("Roll");
<<<<<<< Updated upstream

    // get set 수정
    private bool isRollin = false;

    public bool IsRollin
    {
        // get set 이렇게 안쓰면 큰일난다.
        // get set과 프로퍼티 사용 방법
        // https://itmining.tistory.com/34
        get
        {
            return isRollin;  
        }
        set
        {
            isRollin = value;
        }  
    } 
=======
<<<<<<< HEAD
    private bool isRollin = false;
=======
>>>>>>> Stashed changes

    // get set 수정
    private bool isRollin = false;

    public bool IsRollin
    {
        // get set 이렇게 안쓰면 큰일난다.
        // get set과 프로퍼티 사용 방법
        // https://itmining.tistory.com/34
        get
        {
            return isRollin;  
        }
        set
        {
            isRollin = value;
        }  
    } 

>>>>>>> 295f9f1df3ec097dec4fc9ee025e90ec364d840a
    private float rollCoolTime = 0f;
    private void Roll()
    {
        if (isRollin)
            rigid.velocity = transform.localScale.x * Time.fixedDeltaTime * 300f * Vector2.right;
        if (rollCoolTime > 0f)
        {
            rollCoolTime -= Time.fixedDeltaTime;
            return;
        }
        
        if (Vector2.Distance(transform.position, player.transform.position) > 8.5f && !isRollin)
        {
            anim.SetTrigger(roll);
            isRollin = true;
        }
    }
    private void RollStop() // 애니메이션에서 호출
    {
        rigid.velocity = Vector2.zero;
        rollCoolTime = 4f;
        isRollin = false;
        Attack(0);
        Attack(1);
        Attack(2);
    }
    #endregion
    
    private readonly WaitForSeconds wait3 = new(3f);
    private readonly WaitForSeconds wait2 = new(2f);
    private readonly WaitForSeconds waitDot1 = new(0.1f);

    private IEnumerator Pattern()
    {
        while (isRollin || detectZone.detectedCollider.Count == 0)
            yield return null;
        
        int nextBehavior = UnityEngine.Random.Range(0, 2);

        switch (nextBehavior)
        {
            case 0: // Attack
                int attacks = UnityEngine.Random.Range(1, 4);
                for (int i = 0; i < attacks; i++)
                {
                    Attack(i);
                }
                break;
            
            case 1: // Block
                Block();
                yield return wait3;
                anim.SetBool(block, false);
                isBlock = false;
                break;
        }

        yield return wait2;
        
        StartCoroutine(Pattern());
    }

    #region Attack
    
    private bool isAttack = false;
    private void Attack(int atkIdx)
    {
        anim.SetTrigger(attackAnimation[atkIdx]);
    }
    private void AttackStart() // 애니메이터에서 호출
    {
        isAttack = true;
    }
    private void AttackStop() // 애니메이터에서 호출
    {
        isAttack = false;
    }
    #endregion

    #region Block

    private readonly int block = Animator.StringToHash("Block");
    private readonly int isBlocked = Animator.StringToHash("isBlocked");
    private bool isBlock = false;
    private void Block()
    {
        isBlock = true;
        anim.SetBool(block, true);
    }
    #endregion

    private bool isDead = false;
    public void OnDeath()
    {
        isDead = true;
        StopCoroutine(Pattern());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground"))
            return;

        isTouched = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isBlock)
        {
            anim.SetTrigger(isBlocked);
            return;
        }
    }
}