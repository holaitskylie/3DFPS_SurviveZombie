using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    float moveSpeed = 10f;
   
    void Start()
    {
        
    }
    
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //입력이 감지되었을 때
        if(xInput != 0 || zInput != 0)
        {
            //입력에 따라 이동 방향을 구한다
            Vector3 dir = xInput * Vector3.right + zInput * Vector3.forward;

            transform.rotation = Quaternion.LookRotation(dir);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        
    }
}
