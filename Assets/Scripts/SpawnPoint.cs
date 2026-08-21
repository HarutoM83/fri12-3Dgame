using UnityEngine;
using R3;               // R3 core
using R3.Triggers;
using Unity.VisualScripting;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform spawnpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Spawn();
    }

    void Spawn()
    {
        GameObject enemyobj = Instantiate(enemy);
        enemy.transform.position = spawnpoint.position;
    }
}
