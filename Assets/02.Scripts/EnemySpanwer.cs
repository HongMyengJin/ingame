using Unity.FPS.AI;
using Unity.FPS.UI;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    private float _timer = 0.0f;

    public EnemyDataSO EnemyDataSO;

    [SerializeField]
    private int _maxEnemy = 10;

    private EnemyManager _enemyManager;
    private void Awake()
    {
        _enemyManager = FindObjectOfType<EnemyManager>();
    }
    public void SpawnEnemy()
    {
        Instantiate(EnemyDataSO.Prefab, transform.position, Quaternion.identity);
        _timer = Time.time;
    }

    private void Update()
    {
        if (Time.time - _timer > EnemyDataSO.SpawnTime && _enemyManager.GetEnemyN() < _maxEnemy)
            SpawnEnemy();
    }
}
