using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSightM4A1 : MonoBehaviour
{
    // AimLocation : x = 0 , y = -0.26 , z = 1.2 for Pistol
    // OldLocation : x = 0.2720021 , y = -0.270513 , z = 0.9718211
    public Vector3 aimSightM4A1;
    public Vector3 defaultSightM4A1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, aimSightM4A1, 10 * Time.deltaTime);
            var rotationVector = transform.localRotation.eulerAngles;
            rotationVector.y = 180;
            transform.localRotation = Quaternion.Euler(rotationVector);
        }
        if (Input.GetMouseButtonUp(1))
        {
            transform.localPosition = defaultSightM4A1;
        }
    }
}
