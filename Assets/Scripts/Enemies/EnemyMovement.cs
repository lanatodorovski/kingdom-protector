using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D rb;
    private AttackArea attackArea;

    Vector2 movementDirection;

    private EnemyPathScript path;
    private CharacterAnimationHandler animationHandler;
    [Header("Read Only")]
    [SerializeField]private int nthPathPoint = 0;
    Vector3 pointPosition;

    HealthScript healthScript;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        attackArea = gameObject.GetComponentInChildren<AttackArea>();
        animationHandler = gameObject.GetComponent<CharacterAnimationHandler>();
        healthScript = gameObject.GetComponent<HealthScript>();
    }
    private void Update()
    {
        Move();
        if (healthScript != null && !healthScript.GetIsAlive()) StopMove();
    }
    private void Move()
    {
        if (Mathf.Sign(pointPosition.x - transform.position.x) != Mathf.Sign( movementDirection.x)
            || Mathf.Sign(pointPosition.y - transform.position.y) != Mathf.Sign(movementDirection.y) 
            || nthPathPoint == 0)
        {
            if (nthPathPoint == path.GetPathPointCount())
            {
                //FindAnyObjectByType<PopulationHandler>().EliminatePopulationBy(gameObject);
                int eliminationCount = gameObject.GetComponent<EnemyPopulationHandler>().GetEliminationCount();
                FindAnyObjectByType<PopulationHandler>().RemovePopulation(eliminationCount);
                Destroy(gameObject);
                return;
            }
            if(nthPathPoint == 0 || nthPathPoint ==  path.GetPathPointCount() - 1)
            {
                pointPosition = path.GetPathPointAt(nthPathPoint).transform.position;
            }
            else
            {
                pointPosition = path.GetRandomPositionFromPoint(nthPathPoint);
            }
            movementDirection = (pointPosition - transform.position).normalized;
            nthPathPoint++;
            rb.velocity = movementDirection * movementSpeed;

            animationHandler.AnimateMovement(movementDirection);
            Flip();

        }
    }
    private void StopMove()
    {
        rb.velocity = Vector3.zero;
    }
    private void Flip()
    {
        animationHandler.FlipGOHandler(movementDirection, true);
        attackArea.RotateDirection((int)Mathf.Round(movementDirection.y));
    }
    public void SetPath(EnemyPathScript path)
    {
        this.path = path;
    }
}
