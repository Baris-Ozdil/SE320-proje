using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapi : MonoBehaviour
{
    private bool acilacak = false;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(acilacak)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.003f);
            
            if(startPos.z - this.transform.position.z > this.transform.localScale.z)
            {
                acilacak = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.other.name.Contains("Player"))
        {
            acilacak = true;
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("oldu");
        if(other.name.Contains("Player"))
        {
            acilacak = true;
        }
    }

}
