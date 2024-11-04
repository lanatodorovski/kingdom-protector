using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int damadgeAmount = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<HealthScript>() != null 
            && collision.gameObject.GetComponentInChildren<AttackArea>() != this
            && !collision.gameObject.layer.Equals(gameObject.transform.parent.gameObject.layer))
        {
        Debug.Log(collision.gameObject.name + " " + gameObject.transform.parent.gameObject.layer);
        Debug.Log(!collision.gameObject.layer.Equals(gameObject.GetComponentInParent<Transform>().gameObject.layer));
            HealthScript healthScript = collision.GetComponent<HealthScript>();
            healthScript.Hit(damadgeAmount);            
        }
    }    

    //only changing y axis (x axis is already handled in PlayerMovement)
    public void RotateDirection(int horizontalDirection)
    {
            int rotation = horizontalDirection * 90;
            transform.localEulerAngles = new Vector3(0, 0, rotation);
    }
}
