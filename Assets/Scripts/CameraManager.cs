using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] virtualCams;

    [SerializeField] public CinemachineVirtualCamera deadCam;
    [SerializeField] private CinemachineDollyCart deadCamTrack;  

    [SerializeField] CinemachineVirtualCamera startCam;
    private CinemachineVirtualCamera currentCam;
    
    // Start is called before the first frame update
    void Start()
    {    
        currentCam = startCam;

        //���� ī�޶� �켱���� ����
        for(int i = 0; i< virtualCams.Length; i++)
        {
            if (virtualCams[i] == currentCam)
                virtualCams[i].Priority = 20;
            else
                virtualCams[i].Priority = 10;
        }
        
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
