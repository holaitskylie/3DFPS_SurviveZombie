using UnityEngine;
using System.Collections;

public class FlyCamera : MonoBehaviour
{

	/*
	EXTENDED FLYCAM
		Desi Quintans (CowfaceGames.com), 17 August 2012.
		Based on FlyThrough.js by Slin (http://wiki.unity3d.com/index.php/FlyThrough), 17 May 2011.
 
	LICENSE
		Free as in speech, and free as in beer.
 
	FEATURES
		WASD/Arrows:    Movement
		          Q:    Climb
		          E:    Drop
                      Shift:    Move faster
                    Control:    Move slower
                        End:    Toggle cursor locking to screen (you can also press Ctrl+P to toggle play mode on and off).
	*/

	public float cameraSensitivity = 90; //마우스 움직임에 대한 감도
	public float climbSpeed = 4; //카메라의 상승 속도
	public float normalMoveSpeed = 10; //일반 이동 속도
	public float slowMoveFactor = 0.25f; //느린 이동 속도
	public float fastMoveFactor = 3; //빠른 이동 속도

	//카메라 회전 값 저장
	private float rotationX = 0.0f;
	private float rotationY = 0.0f;

	void Start ()
	{
        //화면 커서를 잠근다
        //게임 화면 내에서 마우스가 보이지 않고, 일종의 가상 화면 내에서만 움직이게 된다
        //Screen.lockCursor = true;

        // 화면 커서를 잠금 설정
        Cursor.lockState = CursorLockMode.Locked;
        // 마우스 커서를 숨김
        Cursor.visible = false;
    }


	void Update ()
	{
		//카메라 회전 제어
		//마우스 X 및 Y 축 움직임에 따른 카메라 회전값 저장
		rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
		rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

		//카메라의 세로 방향 회전 제한(-90 ~ 90도 사이로 제한)
		rotationY = Mathf.Clamp (rotationY, -90, 90);

		//카메라의 로컬 회전
		transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up); //마우스 X축 움직임에 따라 Y축(Vector3.up) 회전
		transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
		{
			transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
			transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl))
		{
			transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
			transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
		}
		else
		{
			transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
			transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
		}


		if (Input.GetKey (KeyCode.Q)) {transform.position += transform.up * climbSpeed * Time.deltaTime;}
		if (Input.GetKey (KeyCode.E)) {transform.position -= transform.up * climbSpeed * Time.deltaTime;}

		if (Input.GetKeyDown (KeyCode.End))
		{
            //Screen.lockCursor = (Screen.lockCursor == false) ? true : false;

            // 현재 마우스 커서 상태를 가져옴
            CursorLockMode currentLockMode = Cursor.lockState;

            // 마우스 커서가 현재 보이는지 여부를 가져옴
            bool isCursorVisible = Cursor.visible;

            // 마우스 커서 상태를 토글
            Cursor.lockState = (currentLockMode == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;

            // 마우스 커서 가시성을 토글
            Cursor.visible = !isCursorVisible;
        }
	}
}