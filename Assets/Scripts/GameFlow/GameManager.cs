using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private SceneLoader sceneLoader;
    
    [Header("Mission UI")]
    [SerializeField] private Canvas missionCanvas;
    private Animator missionAnim;
    private DialogueTrigger dialogueTrigger;
    public Text dialogueArea;
    private Queue<DialogueLine> lines = new Queue<DialogueLine>();
    public bool isDialogueActive = true;
    public float typingSpeed = 0.2f;

    public bool isGameOver = false;
    public bool didPlayerWin = false;
    private PlayerHealth player;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        if (instance == null )
            instance = this;
        else
            Destroy(gameObject);        
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        sceneLoader = GetComponent<SceneLoader>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        missionAnim = missionCanvas.GetComponentInChildren<Animator>();
        
        StartCoroutine("ShowMission");
        
    }

    IEnumerator ShowMission()
    {        
        missionAnim.Play("MissionAnim");
        float animTime = missionAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        yield return new WaitForSeconds(animTime + 0.5f);

        StartDialogue(dialogueTrigger.dialogue);   
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentHealth <= 0)
        {
            Debug.Log("Player Die");
            isGameOver = true;            
        }

        if (didPlayerWin && enemySpawner.enemies.Count <= 0)
            PlayerWin();

    }

    #region Mission

    public void StartDialogue(Dialogue dialogue)
    {
        //isDialogueActive = true;
        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currenLine = lines.Dequeue();

        StartCoroutine(TypeSentence(currenLine));

    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        
        dialogueArea.text = "";

        foreach (char letter in dialogueLine.line.ToCharArray())
        {

            // 줄바꿈 문자를 만나면 새로운 줄로 이동
            if (letter == '0')
            {
                dialogueArea.text += "\n";
                DisplayNextDialogueLine();
            }
            else if (letter == '1')
            {
                EndDialogue();
            }
            else
            {
                dialogueArea.text += letter;
            }

            yield return new WaitForSeconds(typingSpeed);
        }                          

    }

    private void EndDialogue()
    {
        missionAnim.Play("MissionEndAnim");
        
        isDialogueActive = false;
    }

    #endregion

    public void PlayerWin()
    {
        sceneLoader.ReloadGame();
    }


}
