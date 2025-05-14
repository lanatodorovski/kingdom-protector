using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    private List<GameObject> objectsInRange = new List<GameObject>();
    [HideInInspector]
    public GameObject targetedGameobject = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies") 
            && !collision.gameObject.GetComponent<DetectArea>())
        {            
            objectsInRange.Add(collision.gameObject);
            if (targetedGameobject == null)
            {
                targetedGameobject = objectsInRange[objectsInRange.Count - 1];
            }
            //Debug.Log("enemy has entered tower teritory");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            objectsInRange.Remove(collision.gameObject);
            if (collision.gameObject == targetedGameobject && objectsInRange.Count > 0)
            {
                targetedGameobject = objectsInRange[0];
                //Debug.Log("Enemy has left the teritory");
            }

        }
    }
}
