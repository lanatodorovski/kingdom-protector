using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private int projectileDamadge;
    [SerializeField] private float destroyAfterSeconds;
    private GameObject targetObject;


    void FixedUpdate()
    {
        if(targetObject == null || !targetObject.GetComponent<HealthScript>().GetIsAlive())
        {
            Destroy(gameObject);
            return;
        }
        gameObject.transform.position = (Vector3) Vector2.MoveTowards(gameObject.transform.position, targetObject.transform.position, movementSpeed /10);
    }

    private void RotateTowardsTarget()
    {
        Vector2 direction = targetObject.transform.position - gameObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation= rotation;
    }
    public void SetTarget(GameObject target)
    {
        targetObject = target;
        RotateTowardsTarget();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies")
            && collision.gameObject.GetComponent<HealthScript>() != null)
        {
            HealthScript health = collision.gameObject.GetComponent<HealthScript>();
            health.Hit(projectileDamadge);
            Destroy(gameObject);
        }
    }
}
