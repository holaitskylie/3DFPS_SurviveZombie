using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Vector2 touchDist; //������ ���� ��ġ ��ġ�� �Ÿ� ����
    [HideInInspector]
    public Vector2 previousPointer; //���� ��ġ ��ġ ����
    [HideInInspector]
    protected int pointerId; //���� ��ġ �ĺ��� ����
    [HideInInspector]
    public bool isPressed;      
       
    void Update()
    {
        //isPressed�� true�� ��, ���콺 �Ǵ� ��ġ �Է� ó��
        if (isPressed)
        {
            if (pointerId >= 0 && pointerId < Input.touches.Length)
            {
                //����� ��ġ �Է� ó��
                touchDist = Input.touches[pointerId].position - previousPointer;
                previousPointer = Input.touches[pointerId].position;
            }
            else
            {
                //���콺 �Է� ó��
                touchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - previousPointer;
                previousPointer = Input.mousePosition;
            }
        }
        else
        {
            //touchDist �ʱ�ȭ
            touchDist = new Vector2();
        }
    }

    //IPointerDownHandler �������̽� ����
    public void OnPointerDown(PointerEventData eventData)
    {
        //��ġ�� ���۵� �� ȣ��
        isPressed = true;
        pointerId = eventData.pointerId;
        previousPointer = eventData.position;
    }

    //IPointerUpHandler �������̽� ����
    public void OnPointerUp(PointerEventData eventData)
    {
        //��ġ�� ������ �� ȣ��
        isPressed = false;
    }
}
