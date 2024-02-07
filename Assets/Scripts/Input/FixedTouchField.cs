using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Vector2 touchDist; //이전과 현재 터치 위치간 거리 저장
    [HideInInspector]
    public Vector2 previousPointer; //이전 터치 위치 저장
    [HideInInspector]
    protected int pointerId; //현재 터치 식별자 저장
    [HideInInspector]
    public bool isPressed;      
       
    void Update()
    {
        //isPressed가 true일 때, 마우스 또는 터치 입력 처리
        if (isPressed)
        {
            if (pointerId >= 0 && pointerId < Input.touches.Length)
            {
                //모바일 터치 입력 처리
                touchDist = Input.touches[pointerId].position - previousPointer;
                previousPointer = Input.touches[pointerId].position;
            }
            else
            {
                //마우스 입력 처리
                touchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - previousPointer;
                previousPointer = Input.mousePosition;
            }
        }
        else
        {
            //touchDist 초기화
            touchDist = new Vector2();
        }
    }

    //IPointerDownHandler 인터페이스 구현
    public void OnPointerDown(PointerEventData eventData)
    {
        //터치가 시작될 때 호출
        isPressed = true;
        pointerId = eventData.pointerId;
        previousPointer = eventData.position;
    }

    //IPointerUpHandler 인터페이스 구현
    public void OnPointerUp(PointerEventData eventData)
    {
        //터치가 끝났을 때 호출
        isPressed = false;
    }
}
