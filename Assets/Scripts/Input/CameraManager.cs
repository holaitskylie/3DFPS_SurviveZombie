using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] virtualCams;

    private CinemachineVirtualCamera currentCam;
    [SerializeField] CinemachineVirtualCamera playerCam; //게임 진행 시 사용
    [SerializeField] public CinemachineVirtualCamera deadCam; //플레이어 사망 연출 시 사용
    [SerializeField] private CinemachineDollyCart deadCamTrack;

    [Header("Touch Camera Setting")]
    [SerializeField] private FixedTouchField fixedTouchField;
    [SerializeField] private Transform player;
    public Vector2 lockAxis; //카메라 회전 축 제어
    private float xMove; //수평 회전량
    private float yMove; //수직 회전량
    [SerializeField] private float xRotation; //수직 회전 각도
    public float camSensivity = 40f;
    
    void Start()
    {    
        currentCam = playerCam;

        //가상 카메라 우선순위 설정
        for(int i = 0; i< virtualCams.Length; i++)
        {
            if (virtualCams[i] == currentCam)
                virtualCams[i].Priority = 20;
            else
                virtualCams[i].Priority = 10;
        }
        
    }

    private void Update()
    {
        //UI Panel에서의 터치 입력 이동 변화를 대입
        lockAxis = fixedTouchField.touchDist;

        //수평 회전 축 갱신
        xMove = lockAxis.x * camSensivity * Time.deltaTime;

        //수직 회전 축 갱신
        yMove = lockAxis.y * camSensivity * Time.deltaTime;

        //카메라 수직 회전 제한
        xRotation -= yMove;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //카메라 로컬 회전 설정(x축 회전)
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //플레이어를 수평 회전 입력값만큼 y축 회전
        player.Rotate(Vector3.up * xMove);
    }

    public void SwitchCamera(CinemachineVirtualCamera newCam)
    {
        currentCam = newCam;
        currentCam.Priority = 20;

        for(int i = 0; i< virtualCams.Length; i++)
        {
            if (virtualCams[i] != currentCam)
                virtualCams[i].Priority = 10;
        }

        if(currentCam == deadCam)
        {
            // deadCam을 dolly track에 따라 움직이게 설정
            deadCam.Follow = deadCamTrack.transform;           

            // dolly track 초기화
            deadCamTrack.m_Position = 0f;
        }
    }
}
