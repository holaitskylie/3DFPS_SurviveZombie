using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 5f; //target과의 거리
    private float distanceToTarget = Mathf.Infinity;
    private bool isProvoked = false;   
    
    private NavMeshAgent navMeshAgent;
   
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
        
    void Update()
    {
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
        //target과의 거리가 제동 거리보다 같거나 크다면 계속 추적 
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
            ChaseTarget();
        
        //현재 target과의 거리가 제동 거리보다 같거나 작으면 공격
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
