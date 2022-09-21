using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    public float rotationSpeed = 0f;
    void Update()
    {
        transform.Rotate(0f, rotationSpeed, 0f, Space.Self);
    }
}