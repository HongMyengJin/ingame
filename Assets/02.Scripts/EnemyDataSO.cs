using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public int MaxNum;
    public float SpawnTime = 2.0f;
    public GameObject Prefab;
}
