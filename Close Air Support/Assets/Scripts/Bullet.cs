using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTimer = 0.0f;

    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }
}
