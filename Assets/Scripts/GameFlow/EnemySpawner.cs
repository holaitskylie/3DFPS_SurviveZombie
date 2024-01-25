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
        //TODO: Game Manager ����
        // if (GameManager.instance != null && GameManager.instance.isGameOver) return;

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

        for(int i = 0; i < spawnerCount; i++)
        {
            float enemyIntensity = Random.Range(0f, 1f);

            CreateEnemy(enemyIntensity);
        }
       
    }

    //���� �����ϰ� ������ ����� �Ҵ�
    private void CreateEnemy(float intensity)
    {
        //intensity�� ������� ���� �ɷ�ġ ����
        float health = Mathf.Lerp(healthMin, healthMax, intensity);
        float damage = Mathf.Lerp(damageMax, damageMin, intensity);

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        enemy.GetComponent<EnemyHealth>().Setup(health, damage);

        enemies.Add(enemy); 

        //TODO: onDeath �̺�Ʈ �߰�
    }
}
