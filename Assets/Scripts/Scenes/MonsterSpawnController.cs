using Assets.Scripts.Enemies;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    [SerializeField] GameObject monsterObject;
    [SerializeField] int monsterCount;
    [SerializeField] Vector3 monsterSpawnCenter = new Vector3(2.8f, 21.047f, -1.2f);
    [SerializeField] float monsterSpawnRadius;
    [SerializeField] bool spawned = false;
    [SerializeField] bool powerSuplied = false;
    void Start()
    {
        if (!spawned)
        {
            spawned = true;
            monsterSpawnCenter = new Vector3(monsterSpawnCenter.x + monsterSpawnRadius, monsterSpawnCenter.y, monsterSpawnCenter.z + monsterSpawnRadius);
            for (int i = 0; i < monsterCount; i++)
            {
                SpawnmMonster(i);
            }
        }
    }
    void SpawnmMonster(int index)
    {
        Vector3 spawnPosition = GetSpawnPosition();
        GameObject obj = Instantiate(monsterObject, spawnPosition, monsterObject.transform.rotation);
        obj.GetComponent<BaseEnemy>().canHeal = index == 0;
        obj.GetComponent<BaseEnemy>().hasPower = index == 1;
    }

    private Vector3 GetSpawnPosition() =>
        new Vector3(Random.Range(-monsterSpawnCenter.x, monsterSpawnCenter.x), 23f, Random.Range(-monsterSpawnCenter.z, monsterSpawnCenter.z));// * monsterSpawnRadius;

    void Update()
    {
        
    }
}