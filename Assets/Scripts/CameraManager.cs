using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] virtualCams;

    [SerializeField] CinemachineVirtualCamera playerCam;
    private CinemachineVirtualCamera currentCam;
    [SerializeField] public CinemachineVirtualCamera deadCam;
    [SerializeField] private CinemachineDollyCart deadCamTrack;

    [Header("Touch Setting")]
    [SerializeField] private Transform player;
    public Vector2 lockAxis;
    public float camSensivity = 40f;
    private float xMove;
    private float yMove;
    private float xRotation;

    
    // Start is called before the first frame update
    void Start()
    {    
        currentCam = playerCam;

        //���� ī�޶� �켱���� ����
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
        xMove = lockAxis.x * camSensivity * Time.deltaTime;
        yMove = lockAxis.y * camSensivity * Time.deltaTime;
        xRotation -= yMove;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
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
            // deadCam�� dolly track�� ���� �����̰� ����
            deadCam.Follow = deadCamTrack.transform;           

            // dolly track �ʱ�ȭ
            deadCamTrack.m_Position = 0f;
        }
    }
}
