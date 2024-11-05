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
        gameObject.transform.position = (Vector3) Vector2.MoveTowards(gameObject.transform.position, targetObject.transform.position, movementSpeed /10);
        if(targetObject == null)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        targetObject = target;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided projectile");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies")
            && collision.gameObject.GetComponent<HealthScript>() != null)
        {
            HealthScript health = collision.gameObject.GetComponent<HealthScript>();
            health.Hit(projectileDamadge);
            Destroy(gameObject);
        }
    }
}
