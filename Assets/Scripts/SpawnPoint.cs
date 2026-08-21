using UnityEngine;
using R3;               // R3 core
using R3.Triggers;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform spawnpoint;

    private void OnTriggerEnter(Collider other)
    {
        Spawn();
    }

    void Spawn()
    {
        GameObject enemyobj = Instantiate(enemy);
        enemy.transform.position = spawnpoint.position;
    }
}
