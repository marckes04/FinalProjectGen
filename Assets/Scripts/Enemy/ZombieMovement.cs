using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

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

    private float patrol_Radius = 30f;
    private float patrol_Timer = 10f;
    private float timer_Count;

    public float move_Speed = 3.5f;
    public float run_Speed = 5f;

    private Transform player_Target;
    public float chase_Distance = 7f;
    public float attack_Distance = 1f;
    public float chase_Player_After_Attack_Distance = 1f;

    private float wait_Before_Attack_Time = 3f;
    private float attack_Timer;

    private bool flipX  = false;



    private bool enemyDied;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<EnemyAnimations>();
        instance = this;
    }

    void Start()
    {

        timer_Count = patrol_Timer;
        enemyState = EnemyState.PATROL;

        player_Target = GameObject.FindGameObjectWithTag("Player").transform;
        attack_Timer = wait_Before_Attack_Time;
    }

    void Update()
    {

        if (enemyDied)
        {
            return;
        }

        if (enemyState == EnemyState.PATROL)
        {
            Patrol();
        }

        // if we are not chasing AND we are not attacking
        if (enemyState != EnemyState.CHASE && enemyState != EnemyState.ATTACK)
        {

            if (Vector3.Distance(transform.position, player_Target.position) <= chase_Distance)
            {

                enemyState = EnemyState.CHASE;

                enemyAnim.StopAnimation();
            }

        } // if we should chase

        if (enemyState == EnemyState.CHASE)
        {
            ChasePlayer();
        }

        if (enemyState == EnemyState.ATTACK)
        {
            AttackPlayer();
        }


        // Update the flipX value based on the movement direction
        if (navAgent.velocity.x < 0)
        {
            flipX = true;
        }
        else if (navAgent.velocity.x > 0)
        {
            flipX = false;
        }

        // Flip the sprite based on flipX value
        FlipSpriteRenderer();



    } // update

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
            // stop the nav agent from moving
            navAgent.velocity = Vector3.zero;
        }

        if (navAgent.velocity.sqrMagnitude == 0)
        {

            enemyAnim.Walk(false);

        }
        else
        {

           enemyAnim.Walk(true);

        }

    }

    void SetNewRandomDestination()
    {

        Vector3 newDestionation = RandomNavSphere(transform.position, patrol_Radius, -1);
        navAgent.SetDestination(newDestionation);

        if(move_Speed < 0)
        {

        }
    }

    Vector3 RandomNavSphere(Vector3 originPos, float dist, int layerMask)
    {

        Vector3 randDir = Random.insideUnitSphere * dist;
        randDir += originPos;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, dist, layerMask);

        return navHit.position;
    }

    void ChasePlayer()
    {

        navAgent.SetDestination(player_Target.position);
        navAgent.speed = run_Speed;

        if (navAgent.velocity.sqrMagnitude == 0)
        {

            enemyAnim.Run(false);

        }
        else
        {

           enemyAnim.Run(true);

        }

        // we are in the range of the attack
        if (Vector3.Distance(transform.position, player_Target.position) <= attack_Distance)
        {

            enemyState = EnemyState.ATTACK;

        }
        else if (Vector3.Distance(transform.position, player_Target.position)
                  > chase_Distance)
        {

            timer_Count = patrol_Timer;
            enemyState = EnemyState.PATROL;
            enemyAnim.Run(false);

        }

    } // chase player

    void AttackPlayer()
    {

        // stop the velocity meaning the game object will stop moving
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        enemyAnim.Run(false);
        enemyAnim.Walk(false);

        attack_Timer += Time.deltaTime;

        if (attack_Timer > wait_Before_Attack_Time)
        {

            //enemyAnim.NormalAttack_1();

            //attack_Timer = 0f;

        }

        if (Vector3.Distance(transform.position, player_Target.position) >
           attack_Distance + chase_Player_After_Attack_Distance)
        {

            // enable the agent to move again
            navAgent.isStopped = false;

            enemyState = EnemyState.CHASE;

        }

    } // attack player

    void FlipSpriteRenderer()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = flipX;
        }
    }


   


}
