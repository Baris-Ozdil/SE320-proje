using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headshot : MonoBehaviour
{
    public GameObject head;
    public GameObject blood;

    // Update is called once per frame
    private void OnDisable()
    {
        head.SetActive(false); // disablellandıgında collider atanan headde disablellanır
        blood.SetActive(true);
    } 
    
}
