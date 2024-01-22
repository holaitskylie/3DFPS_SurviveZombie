using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float chaseRange = 5f; //target과의 거리
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

    public void OnDamaged()
    {
        isProvoked= true;
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
}
