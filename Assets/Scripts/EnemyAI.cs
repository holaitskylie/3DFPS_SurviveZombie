using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float chaseRange = 5f; //target���� �Ÿ�
    private float distanceToTarget = Mathf.Infinity;
    private bool isProvoked = false;   
    
    private NavMeshAgent navMeshAgent;
    private Animator animator;
   
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
        
    void Update()
    {
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

    public void OnDamaged()
    {
        isProvoked= true;
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
}
