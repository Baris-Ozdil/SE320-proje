using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dalekSpawner : MonoBehaviour
{
    public GameObject Dalek;
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
            
                Vector3 pos = new Vector3(gameObject.transform.position.x , gameObject.transform.position.y , gameObject.transform.position.z) ;
                Instantiate(Dalek, pos, transform.rotation);
            
            StartCoroutine(spawnTimer());
        }

    }

    IEnumerator spawnTimer()
    {
        spawn = false;
        yield return new WaitForSeconds(45f);
        spawn = true;
    }
}
