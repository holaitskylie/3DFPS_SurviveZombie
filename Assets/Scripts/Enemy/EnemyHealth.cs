using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : Entity
{
    //[SerializeField] private float hitPoints = 100f;
    //private bool isDead = false;
    private Animator animator;
    [SerializeField] private ParticleSystem hitFX;
    

    [Header("Sounds")]
    private AudioSource enemyAudioPlayer;
    [SerializeField] private AudioClip deatClip;
    [SerializeField] private AudioClip hitClip;

    [Header("AI Navigation")]
    private NavMeshAgent navMeshAgent;
    private PlayerHealth player;
    [SerializeField] private Transform target;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float chaseRange = 5f; //target과의 거리
    private float distanceToTarget = Mathf.Infinity;
    private bool isProvoked = false;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAudioPlayer = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<PlayerHealth>();
        target = player.transform;
    }
    
    public void Setup(float newHealth, float newDamage)
    {
        startingHealth = newHealth;

        int _newDamage = Mathf.RoundToInt(newDamage);
        GetComponent<EnemyAttack>().damage = _newDamage;       

    }

    private void Update()
    {
        if (isDead)
            return;

        if (target.GetComponent<PlayerHealth>().IsDead())
        {
            animator.SetBool("attack", false);
            animator.SetTrigger("idle");
            return;
        }

        //target과의 거리 구하기
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange) //target과의 거리가 chaseRange보다 작을 때 
        {
            isProvoked = true;
        }

    }

    private void EngageTarget()
    {
        FaceTarget();

        //target과의 거리가 제동 거리보다 같거나 크다면 계속 추적 
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
            ChaseTarget();

        //현재 target과의 거리가 제동 거리보다 같거나 작으면 공격
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
            AttackTarget();
    }

    private void FaceTarget()
    {
        //target의 방향을 찾아 회전
        Vector3 dir = (target.position - transform.position).normalized;

        //target을 향해 회전해야 할 쿼터니언 회전을 구한다
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

        //적의 현재 회전과 목표 회전(lookRotation) 사이를 부드럽게 회전하도록 보간
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void ChaseTarget()
    {
        animator.SetBool("attack", false);

        navMeshAgent.SetDestination(target.position);
        animator.SetTrigger("move");
    }

    private void AttackTarget()
    {
        animator.SetBool("attack", true);
               
    }  

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

    }
        

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        if(!isProvoked)
            isProvoked = true;

        if(!isDead)
        {
            //Instantiate(hitFX, hitPoint, Quaternion.LookRotation(hitNormal));
            
            ParticleSystem effect = Instantiate(hitFX, hitPoint,Quaternion.LookRotation(hitNormal));
            if(effect != null)
            {
                effect.Play();
                Destroy(effect.gameObject, 1f);                
            }
            
                                            
            enemyAudioPlayer.PlayOneShot(hitClip);
        }
       
    }

    public override void Die()
    {
        base.Die();

        //Nav Mesh Agent 비활성화
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        //사망 애니메이션 & 효과음 재생
        animator.SetTrigger("die");
        enemyAudioPlayer.PlayOneShot(deatClip);
    }


    
}
