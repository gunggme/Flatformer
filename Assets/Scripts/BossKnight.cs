using UnityEngine;
using UnityEngine.InputSystem;

public class BossKnight : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CapsuleCollider2D coll;
    private Animator anim;

    public DetectionZone detectZone;
    public GameObject player;

    private bool isSpawn;

    private int _attackCnt = 0;

    private int AttackCnt
    {
        get => _attackCnt;

        set
        {
            _attackCnt = value;
            if (_attackCnt > 2)
                _attackCnt = 0;
        }
    }

    private int[] attackAnimation =
        { Animator.StringToHash("Attack1"), Animator.StringToHash("Attack2"), Animator.StringToHash("Attack3") };

    public void BossSpawn()
    {
        Debug.Log("BossSpawn");
        coll.isTrigger = true;
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

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (rigid.velocity.y != 0)
            return;
        if (!isSpawn)
        {
            isSpawn = true;
            anim.SetTrigger("StopFall");
            return;
        }

        if (transform.position.x - player.transform.position.x >= 0)
            transform.localScale = new Vector2(-2f, 2f);
        else
            transform.localScale = new Vector2(2f, 2f);
        Attack();
        Block();
        Roll();
    }

    private int stopAttack = Animator.StringToHash("StopAttack");
    private float attackCool = 0f;
    private float shortAttackCool = 0f;
    private void Attack()
    {
        if (shortAttackCool > 0f)
        {
            shortAttackCool -= Time.fixedDeltaTime;
            return;
        }
        if (attackCool > 0f)
        {
            attackCool -= Time.fixedDeltaTime;
            return;
        }

        if (detectZone.detectedCollider.Count > 0)
        {
            anim.SetBool(stopAttack, false);
            anim.SetTrigger(attackAnimation[AttackCnt++]);
            if (AttackCnt == 0)
                attackCool = 4f;

            shortAttackCool = 0.5f;
        }
        else
        {
            anim.SetBool(stopAttack, true);
            attackCool = 4f;
        }
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

    private readonly int roll = Animator.StringToHash("Roll");
    public bool IsRollin { get; set; } = false;

    private float rollCoolTime = 0f;

    private void Roll()
    {
        if (IsRollin)
            rigid.velocity = transform.localScale.x * Time.fixedDeltaTime * 300f * Vector2.right;
        if (rollCoolTime > 0f)
        {
            rollCoolTime -= Time.fixedDeltaTime;
            return;
        }

        if (Vector2.Distance(transform.position, player.transform.position) > 8.5f && !IsRollin)
        {
            rollCoolTime = 5f;
            anim.SetTrigger(roll);
            IsRollin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        coll.isTrigger = false;
    }
}