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

        //가상 카메라 우선순위 설정
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
            // deadCam을 dolly track에 따라 움직이게 설정
            deadCam.Follow = deadCamTrack.transform;           

            // dolly track 초기화
            deadCamTrack.m_Position = 0f;
        }
    }
}
