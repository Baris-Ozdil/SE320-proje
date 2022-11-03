using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSİght : MonoBehaviour
{
    // AimLocation : x = 0 , y = -0.13 , z = 0.5324571 for Pistol
    // OldLocation : x = 0.1889456 , y = -0.1847655 , z = 0.5324571
    public Vector3 aimSight;
    public Vector3 defaultSight;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition,aimSight, 10 * Time.deltaTime);
        }
        if (Input.GetMouseButtonUp(1))
        {
            transform.localPosition = defaultSight;
        }
    }
}
