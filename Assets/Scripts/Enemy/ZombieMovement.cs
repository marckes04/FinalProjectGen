using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class ZombieMovement : MonoBehaviour
{
    public static ZombieMovement instance;

    private EnemyAnimations enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyState enemyState;
    private Stopwatch stopwatch; // Used for time measurement

    // UI Text elements for displaying state times
    public Text patrolTimeText;
    public Text chaseTimeText;
    public Text attackTimeText;

    private float patrol_Radius = 30f;
    private float patrol_Timer = 10f;
    private float timer_Count;

    public static float move_Speed = 3.5f;
    public static float run_Speed = 5f;

    private Transform player_Target;
    public float chase_Distance = 7f;
    public float attack_Distance = 1f;
    public float chase_Player_After_Attack_Distance = 1f;

    private float wait_Before_Attack_Time = 3f;
    private float attack_Timer;

    private bool flipX = false;
    private bool enemyDied;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<EnemyAnimations>();
        instance = this;
        stopwatch = new Stopwatch(); // Initialize the stopwatch
    }

    void Start()
    {
        timer_Count = patrol_Timer;
        enemyState = EnemyState.PATROL;
        player_Target = GameObject.FindGameObjectWithTag("Player").transform;
        attack_Timer = wait_Before_Attack_Time;

        stopwatch.Start(); // Start the stopwatch initially

        // Initialize UI text values
        if (patrolTimeText != null) patrolTimeText.text = "Patrol Time: 0 ms";
        if (chaseTimeText != null) chaseTimeText.text = "Chase Time: 0 ms";
        if (attackTimeText != null) attackTimeText.text = "Attack Time: 0 ms";
    }

    void Update()
    {
        if (enemyDied)
        {
            return;
        }

        switch (enemyState)
        {
            case EnemyState.PATROL:
                Patrol();
                break;
            case EnemyState.CHASE:
                ChasePlayer();
                break;
            case EnemyState.ATTACK:
                AttackPlayer();
                break;
        }

        if (enemyState != EnemyState.CHASE && enemyState != EnemyState.ATTACK)
        {
            if (Vector3.Distance(transform.position, player_Target.position) <= chase_Distance)
            {
                ChangeState(EnemyState.CHASE);
                enemyAnim.StopAnimation();
            }
        }

        if (navAgent.velocity.x < 0)
        {
            flipX = true;
        }
        else if (navAgent.velocity.x > 0)
        {
            flipX = false;
        }

        FlipSpriteRenderer();
    }

    void ChangeState(EnemyState newState)
    {
        if (newState != enemyState)
        {
            stopwatch.Stop();
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            // Update the corresponding UI text based on the current state
            switch (enemyState)
            {
                case EnemyState.PATROL:
                    if (patrolTimeText != null)
                    {
                        patrolTimeText.text = $"Patrol Time: {elapsedMilliseconds} ms";
                    }
                    break;
                case EnemyState.CHASE:
                    if (chaseTimeText != null)
                    {
                        chaseTimeText.text = $"Chase Time: {elapsedMilliseconds} ms";
                    }
                    break;
                case EnemyState.ATTACK:
                    if (attackTimeText != null)
                    {
                        attackTimeText.text = $"Attack Time: {elapsedMilliseconds} ms";
                    }
                    break;
            }

            // Change the state and reset the stopwatch
            enemyState = newState;
            stopwatch.Reset();
            stopwatch.Start();
        }
    }

    void Patrol()
    {
        timer_Count += Time.deltaTime;
        navAgent.speed = move_Speed;

        if (timer_Count > patrol_Timer)
        {
            SetNewRandomDestination();
            timer_Count = 0f;
        }

        if (navAgent.remainingDistance <= 0.5f)
        {
            navAgent.velocity = Vector3.zero;
        }

        enemyAnim.Walk(navAgent.velocity.sqrMagnitude != 0);
    }

    void SetNewRandomDestination()
    {
        Vector3 newDestination = RandomNavSphere(transform.position, patrol_Radius, -1);
        navAgent.SetDestination(newDestination);
    }

    Vector3 RandomNavSphere(Vector3 originPos, float dist, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;
        randDir += originPos;

        NavMesh.SamplePosition(randDir, out NavMeshHit navHit, dist, layerMask);

        return navHit.position;
    }

    void ChasePlayer()
    {
        navAgent.SetDestination(player_Target.position);
        navAgent.speed = run_Speed;

        enemyAnim.Run(navAgent.velocity.sqrMagnitude != 0);

        if (Vector3.Distance(transform.position, player_Target.position) <= attack_Distance)
        {
            ChangeState(EnemyState.ATTACK);
        }
        else if (Vector3.Distance(transform.position, player_Target.position) > chase_Distance)
        {
            timer_Count = patrol_Timer;
            ChangeState(EnemyState.PATROL);
            enemyAnim.Run(false);
        }
    }

    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        enemyAnim.Run(false);
        enemyAnim.Walk(false);

        attack_Timer += Time.deltaTime;

        if (attack_Timer > wait_Before_Attack_Time)
        {
            // Trigger attack animation (uncomment when animation is ready)
            // enemyAnim.NormalAttack_1();
            // attack_Timer = 0f;
        }

        if (Vector3.Distance(transform.position, player_Target.position) > attack_Distance + chase_Player_After_Attack_Distance)
        {
            navAgent.isStopped = false;
            ChangeState(EnemyState.CHASE);
        }
    }

    void FlipSpriteRenderer()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = flipX;
        }
    }
}
