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
                //터치가 발생했을 때 터치의 위치 변화를 계산
                touchDist = Input.touches[pointerId].position - previousPointer;
                
                //현재 프레임 터치 위치를 저장
                //다음 프레임에서 이전 프레임의 터치 위치로 사용
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
        //터치가 시작될 때 호출 && 터치의 첫 프레임에서만 실행

        isPressed = true; //터치가 일어나고 있음을 알림
        pointerId = eventData.pointerId; //UI 요소에서 발생한 터치를 식별
        previousPointer = eventData.position; //UI 요소에서 발생한 터치의 위치
    }

    //IPointerUpHandler 인터페이스 구현
    public void OnPointerUp(PointerEventData eventData)
    {
        //터치가 끝났을 때 호출
        isPressed = false;
    }
}
