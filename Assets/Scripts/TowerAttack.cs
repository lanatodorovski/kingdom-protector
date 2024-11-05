using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectileObject;
    [SerializeField] private float projectileCooldown = 1f;
    
    private TargetEnemy targetEnemy;
    private float cooldownTimer = 0f;

    private void Awake()
    {
        targetEnemy = GetComponentInChildren<TargetEnemy>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (targetEnemy.targetedGameobject != null
            && cooldownTimer > projectileCooldown)
        {
            GameObject newProjectile = Instantiate(projectileObject, gameObject.transform.position, Quaternion.identity);
            newProjectile.GetComponent<ProjectileScript>().SetTarget(targetEnemy.targetedGameobject);
            cooldownTimer = 0f;
        }
    }

}
