using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PervaneDönme : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] // speedi gösterir ama private olarak tutar yani değistirilemez!
    float speed = 200;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
