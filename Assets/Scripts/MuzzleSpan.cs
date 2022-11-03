using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleSpan : MonoBehaviour
{
    public float MuzzleLifeSpan = 0.5f;
    private float LifeSpan;
    // Start is called before the first frame update
    void Start()
    {
        LifeSpan = MuzzleLifeSpan;
        
    }

    // Update is called once per frame
    void Update()
    {
        LifeSpan -= Time.deltaTime;
        if(LifeSpan <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
