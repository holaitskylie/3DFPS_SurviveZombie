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

        //적을 모두 물리친 경우 다음 스폰 진행
        if (enemies.Count <= 0)
            SpawnWave();

        //UI 갱신
        UpdateUI();

    }

    private void UpdateUI()
    {
        //TODO: UI Manager 생성
        //현재 웨이브와 남은 적 수 표시
        //UIManager.instance.UpdateWaveText(wave, enemies.Count);
    }

    //현재 웨이브에 맞춰 적 생성
    private void SpawnWave()
    {
        wave++;

        //현재 웨이브 * 1.5를 반올림한 수만큼 적 생성
        int spawnerCount = Mathf.RoundToInt(wave * 1.5f);

        //TODO: 일정 웨이브 증가 후 보스몹 스폰
        for (int i = 0; i < spawnerCount; i++)
        {
            float enemyIntensity = Random.Range(0f, 1f);

            CreateEnemy(enemyIntensity);
        }

        if(wave == endWave)
            CreateBoss();

    }

    //적을 생성하고 추적할 대상을 할당
    private void CreateEnemy(float intensity)
    {
        //intensity를 기반으로 적의 능력치 결정
        float health = Mathf.Lerp(healthMin, healthMax, intensity);
        float damage = Mathf.Lerp(damageMax, damageMin, intensity);

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        enemyHealth.Setup(health, damage);

        enemies.Add(enemy);

        // OnEnemyDeath 메서드를 델리게이트로 정의하여 추가
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