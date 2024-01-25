using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [Header("Enemy Settings")]
    [SerializeField] private float damageMax = 40f;
    [SerializeField] private float damageMin = 20f;
    [SerializeField] private float healthMax = 200f;
    [SerializeField] private float healthMin = 80f;

    public Transform[] spawnPoints;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private int wave;

    
    void Update()
    {
        //TODO: Game Manager 생성
        // if (GameManager.instance != null && GameManager.instance.isGameOver) return;

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

        for(int i = 0; i < spawnerCount; i++)
        {
            float enemyIntensity = Random.Range(0f, 1f);

            CreateEnemy(enemyIntensity);
        }
       
    }

    //적을 생성하고 추적할 대상을 할당
    private void CreateEnemy(float intensity)
    {
        //intensity를 기반으로 적의 능력치 결정
        float health = Mathf.Lerp(healthMin, healthMax, intensity);
        float damage = Mathf.Lerp(damageMax, damageMin, intensity);

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        enemy.GetComponent<EnemyHealth>().Setup(health, damage);

        enemies.Add(enemy); 

        //TODO: onDeath 이벤트 추가
    }
}
