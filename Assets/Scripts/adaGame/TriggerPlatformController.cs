using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatformController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;

        if (gameObject.CompareTag("Sphere"))
        {
            GameObject parent = transform.parent.gameObject;
            Invoke("IncrementSteps", 0.8F);
            Destroy(parent, 0.8F);
            
        }
    }

    private void IncrementSteps()
    {
        AdaGameUIManager.current.steps += 1;
    }
}
