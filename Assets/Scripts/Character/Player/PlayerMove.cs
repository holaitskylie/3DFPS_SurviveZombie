using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
//CharancterController를 이용하여 플레이어의 이동 구현
//</summary>
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
        //게임 미션 안내중이거나 게임 종료면 리턴
        if (GameManager.instance.isDialogueActive || GameManager.instance.isGameOver)
            return;

        //플레이어 최종 이동 방향을 구한다
        //조이스틱 joystick의 입력값에 따른 플레이어의 좌우, 앞뒤 이동 방향을 구한다
        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;

        //CharacterContrller 컴포넌트의 Move()에 최종 이동 방향에 속도를 곱한 값을 넘겨 플레이어 이동 구현
        controller.Move(Move * moveSpeed * Time.deltaTime);

    }
}
