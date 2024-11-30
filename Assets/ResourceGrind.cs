using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGrind : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Debug.Log("collision");
            ResourceObject resourceObject = collision.gameObject.GetComponent<ResourceObject>();
            resourceObject.HitAndGain();
        }
    }
}
