using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class StartScene : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    private Animator animator;
    
    void Start()
    {
        targetPoint = 0;
        animator = GetComponent<Animator>();
        
    }
    
    void Update()
    {
        animator.SetTrigger("move");
        transform.LookAt(patrolPoints[targetPoint].position);
        Vector3 dir = patrolPoints[targetPoint].position - transform.position;        

        if (transform.position == patrolPoints[targetPoint].position)
            IncreasePoint();
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(dir);
        }           
        
    }

    private void IncreasePoint()
    {
        targetPoint++;

        if(targetPoint >= patrolPoints.Length)
            targetPoint = 0;
    }
}
