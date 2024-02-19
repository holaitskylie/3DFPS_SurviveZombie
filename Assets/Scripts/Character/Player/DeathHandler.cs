using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas joystickCanvas;
    [SerializeField] private Canvas healthCanvas;
    
    void Start()
    {
        gameOverCanvas.enabled = false;
        joystickCanvas.enabled = true;
        healthCanvas.enabled = true;

        //플레이어 사망시 HandleDeath 메서드 실행
        PlayerHealth player = FindObjectOfType<PlayerHealth>();
        player.onDeath += () => HandleDeath();
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        joystickCanvas.enabled = false;
        healthCanvas.enabled = false;
        //Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
