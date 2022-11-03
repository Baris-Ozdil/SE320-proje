using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparancy : MonoBehaviour
{
    public GameObject obje;
    public float alpha = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        obje = gameObject;
        transcparancy(obje.GetComponent<Renderer>().material, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //change alpha
    private void transcparancy(Material material, float alpha) {
        Color oldcolar = material.color;
        Color newcolar = new Color(oldcolar.r, oldcolar.g, oldcolar.b, alpha);
        material.SetColor("_Color", newcolar);
    }
}
