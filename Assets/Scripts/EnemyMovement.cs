using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
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
    [SerializeField]private int nthPathPoint = 0;
    Vector3 pointPosition;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        attackArea = gameObject.GetComponentInChildren<AttackArea>();
        animationHandler = gameObject.GetComponent<CharacterAnimationHandler>();
    }
    private void Update()
    {
            Move();
    }
    private void Move()
    {
        if (Mathf.Sign(pointPosition.x - transform.position.x) != Mathf.Sign( movementDirection.x)
            && Mathf.Sign(pointPosition.y - transform.position.y) != Mathf.Sign(movementDirection.y) 
            || nthPathPoint == 0)
        {
            if (nthPathPoint == path.GetPathPointCount())
            {
                Destroy(gameObject);
                return;
            }
            if(nthPathPoint == 0 || nthPathPoint ==  path.GetPathPointCount() - 1)
            {
                pointPosition = path.GetPathPointAt(nthPathPoint).transform.position;
            }
            else
            {
                pointPosition = path.GetRandomPositionFromPoint(nthPathPoint, 1.5f);
            }
            movementDirection = (pointPosition - transform.position).normalized;
            nthPathPoint++;
            rb.velocity = movementDirection * movementSpeed;

            animationHandler.AnimateMovement(movementDirection);

            Flip();

        }
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
