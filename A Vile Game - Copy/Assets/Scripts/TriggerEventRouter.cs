using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TriggerEvent(GameObject trigger, GameObject other);

public class TriggerEventRouter : MonoBehaviour
{
    public TriggerEvent callback;

    void OnTriggerEnter(Collider other)
    {
        if (callback != null)
        {
            callback(this.gameObject, other.gameObject);
        }
    }
}
