using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneNumber;
    public void ReloadGame()
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Helicopter"))
        {
            StartCoroutine("LoadGameScene");
        }
    }

    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("main");
    }
}
