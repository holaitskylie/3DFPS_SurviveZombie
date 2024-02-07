using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] public Joystick joystick;
    private CharacterController controller;
    [SerializeField] private float moveSpeed = 5f;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }
    
    void Update()
    {
        if (GameManager.instance.isDialogueActive || GameManager.instance.isGameOver)
            return;

        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        controller.Move(Move * moveSpeed * Time.deltaTime);

    }
}
