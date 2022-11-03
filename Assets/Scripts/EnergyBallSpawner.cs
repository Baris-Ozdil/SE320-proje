using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallSpawner : MonoBehaviour
{
    public GameObject EnergBall;
    bool spawn = true;
    // Start is called before the first frame update
    void Start()
    {
       // Vector3 posG = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (spawn)
        {
            for (int i =0; i < 10;  i++)
            {
                Vector3 pos = new Vector3( gameObject.transform.position.x +  Random.Range(-50, 50), gameObject.transform.position.y + Random.Range(0, 1), gameObject.transform.position.z + Random.Range(-50, 50));
                Instantiate(EnergBall, pos, transform.rotation);
            }
            StartCoroutine(spawnTimer());
        }

    }

    IEnumerator spawnTimer()
    {
        spawn = false;
        yield return new WaitForSeconds(20f);
        spawn = true;
    }
}
