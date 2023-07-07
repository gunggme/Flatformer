using UnityEngine;

internal class AnimationStrings
{
    // internal static string isMoving = "isMoving";
    // internal static string isRunning = "isRunning";
    // internal static string isGrounded = "isGrounded";
    // internal static string yVelocity = "yVelocity";
    // internal static string jumpTrigger = "jump";
    // internal static string isOnWall = "isOnWall";
    // internal static string isOnCeiling = "isOnCeiling";
    // internal static string attackTrigger = "attack";
    // internal static string canMove = "canMove";
    // internal static string hasTarget = "hasTarget";
    // internal static string isAlive = "isAlive";
    // internal static string isHit = "isHit";
    // internal static string hitTrigger = "hit";
    // internal static string lockVelocity = "lockVelocity";
    // internal static string attackCoolDown = "attackCooldown";
    // internal static string rangeAttackTrigger = "rangeAttack";
    // internal static string isDash = "isDash";

    // Animator에서 String을 매번 Hash값으로 바꿔서 성능이 안좋다길래, 처음부터 Hash값으로 바꿔줌
    internal static int isMoving = Animator.StringToHash("isMoving");
    internal static int isRunning = Animator.StringToHash("isRunning");
    internal static int isGrounded = Animator.StringToHash("isGrounded");
    internal static int yVelocity = Animator.StringToHash("yVelocity");
    internal static int jumpTrigger = Animator.StringToHash("jump");
    internal static int isOnWall = Animator.StringToHash("isOnWall");
    internal static int isOnCeiling = Animator.StringToHash("isOnCeiling");
    internal static int attackTrigger = Animator.StringToHash("attack");
    internal static int canMove = Animator.StringToHash("canMove");
    internal static int hasTarget = Animator.StringToHash("hasTarget");
    internal static int isAlive = Animator.StringToHash("isAlive");
    internal static int isHit = Animator.StringToHash("isHit");
    internal static int hitTrigger = Animator.StringToHash("hit");
    internal static int lockVelocity = Animator.StringToHash("lockVelocity");
    internal static int attackCoolDown = Animator.StringToHash("attackCooldown");
    internal static int rangeAttackTrigger = Animator.StringToHash("rangeAttack");
    internal static int isDash = Animator.StringToHash("isDash");
}
