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
    [SerializeField] private float chaseRange = 5f; //target���� �Ÿ�
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

        //target���� �Ÿ� ���ϱ�
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange) //target���� �Ÿ��� chaseRange���� ���� �� 
        {
            isProvoked = true;
        }

    }

    private void EngageTarget()
    {
        FaceTarget();

        //target���� �Ÿ��� ���� �Ÿ����� ���ų� ũ�ٸ� ��� ���� 
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
            ChaseTarget();

        //���� target���� �Ÿ��� ���� �Ÿ����� ���ų� ������ ����
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
            AttackTarget();
    }

    private void FaceTarget()
    {
        //target�� ������ ã�� ȸ��
        Vector3 dir = (target.position - transform.position).normalized;

        //target�� ���� ȸ���ؾ� �� ���ʹϾ� ȸ���� ���Ѵ�
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

        //���� ���� ȸ���� ��ǥ ȸ��(lookRotation) ���̸� �ε巴�� ȸ���ϵ��� ����
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

        //Nav Mesh Agent ��Ȱ��ȭ
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        //��� �ִϸ��̼� & ȿ���� ���
        animator.SetTrigger("die");
        enemyAudioPlayer.PlayOneShot(deatClip);
    }


    
}
