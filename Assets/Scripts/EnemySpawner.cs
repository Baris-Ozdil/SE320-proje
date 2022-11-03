using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn; // basically our zombie
    public Transform spawnParent; //More than 1 transforms
    BoxCollider trigger;
    private void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") // whether player entered or not
        {
            SpawnEnemy();
            trigger.enabled = false; // box colliderı disabller eger player gecerse ki birdaha spawnlamasın 
        }
    }
    void SpawnEnemy()
    {
        foreach (Transform sp in spawnParent) // for each var sp in spawnPoints which is our every spawn points for zombies
        {
            Instantiate(enemyToSpawn, sp.position, sp.rotation); // we want to instantiate at sp position and sp's rotation
        }
    }

 
}
