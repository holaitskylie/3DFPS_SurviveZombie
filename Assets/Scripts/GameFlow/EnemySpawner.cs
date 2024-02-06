using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossPrefab;

    [Header("Enemy Settings")]
    [SerializeField] private float damageMax = 40f;
    [SerializeField] private float damageMin = 20f;
    [SerializeField] private float healthMax = 200f;
    [SerializeField] private float healthMin = 80f;

    public Transform[] spawnPoints;
    public List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private int wave;
    [SerializeField] private int endWave = 2;


    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isDialogueActive)
            return;

        if (GameManager.instance.isGameOver || GameManager.instance.didPlayerWin) 
            return;

        //���� ��� ����ģ ��� ���� ���� ����
        if (enemies.Count <= 0)
            SpawnWave();

        //UI ����
        UpdateUI();

    }

    private void UpdateUI()
    {
        //TODO: UI Manager ����
        //���� ���̺�� ���� �� �� ǥ��
        //UIManager.instance.UpdateWaveText(wave, enemies.Count);
    }

    //���� ���̺꿡 ���� �� ����
    private void SpawnWave()
    {
        wave++;

        //���� ���̺� * 1.5�� �ݿø��� ����ŭ �� ����
        int spawnerCount = Mathf.RoundToInt(wave * 1.5f);

        //TODO: ���� ���̺� ���� �� ������ ����
        for (int i = 0; i < spawnerCount; i++)
        {
            float enemyIntensity = Random.Range(0f, 1f);

            CreateEnemy(enemyIntensity);
        }

        if(wave == endWave)
            CreateBoss();

    }

    //���� �����ϰ� ������ ����� �Ҵ�
    private void CreateEnemy(float intensity)
    {
        //intensity�� ������� ���� �ɷ�ġ ����
        float health = Mathf.Lerp(healthMin, healthMax, intensity);
        float damage = Mathf.Lerp(damageMax, damageMin, intensity);

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        enemyHealth.Setup(health, damage);

        enemies.Add(enemy);

        // OnEnemyDeath �޼��带 ��������Ʈ�� �����Ͽ� �߰�
        enemyHealth.onDeath += () => OnEnemyDeath(enemy);

    }

    private void CreateBoss()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        EnemyHealth bossHealth = boss.GetComponent<EnemyHealth>();

        float health = healthMax + 50f;
        float damage = damageMax + 10f;
       
        bossHealth.Setup(health, damage);

        enemies.Add(boss);
        bossHealth.onDeath += () => OnEnemyDeath(boss);
        bossHealth.onDeath += () => StartEndScene();
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject, 5f);
    }

    private void StartEndScene()
    {
        GameManager.instance.didPlayerWin = true;
    }
}