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

        //�Է��� �����Ǿ��� ��
        if(xInput != 0 || zInput != 0)
        {
            //�Է¿� ���� �̵� ������ ���Ѵ�
            Vector3 dir = xInput * Vector3.right + zInput * Vector3.forward;

            transform.rotation = Quaternion.LookRotation(dir);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        
    }
}
