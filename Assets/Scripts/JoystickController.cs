using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class JoystickController : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField] private CinemachineVirtualCamera fpsCam;
    [SerializeField] private float rotationHorizontal;
    [SerializeField] private float rotationVertical;
    [SerializeField] private float rotationSpeed = 35f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationVertical = joystick.Vertical;
        rotationHorizontal = joystick.Horizontal;

        fpsCam.transform.Rotate(-rotationVertical * rotationSpeed * Time.deltaTime, rotationHorizontal * rotationSpeed * Time.deltaTime, 0);
        
    }
}
