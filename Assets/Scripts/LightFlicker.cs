using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light theLight;
    [SerializeField]
    float minTimeBeforeLightFlickers;

    [SerializeField]
    float maxTimeBeforeLightFlickers;
    // Start is called before the first frame update
    void Start()
    {
        theLight = GetComponent<Light>();

        StartCoroutine(MakeLightFlicker());
    }
    IEnumerator MakeLightFlicker()
    {
        while (true)
        { //tekrar etmesi icin
            yield return new WaitForSeconds(Random.Range(minTimeBeforeLightFlickers, maxTimeBeforeLightFlickers));
            theLight.enabled = !theLight.enabled; // on-off switch icin mesela flashlight offsa on onsa off yapar
        }
    }
    // Update is called once per frame

}
