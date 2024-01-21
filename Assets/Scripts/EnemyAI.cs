using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 5f; //target���� �Ÿ�
    private float distanceToTarget = Mathf.Infinity;
    private bool isProvoked = false;   
    
    private NavMeshAgent navMeshAgent;
   
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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

    private void EngageTarget()
    {
        //target���� �Ÿ��� ���� �Ÿ����� ���ų� ũ�ٸ� ��� ���� 
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
            ChaseTarget();
        
        //���� target���� �Ÿ��� ���� �Ÿ����� ���ų� ������ ����
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
            AttackTarget();        

    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        Debug.Log("Enemy is Attacking " + target.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        
    }
}
