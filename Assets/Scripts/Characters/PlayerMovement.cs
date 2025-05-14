using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Rigidbody2D rb;
    private AttackArea attackArea;

    Vector2 movementDirection;
    bool isMoving;
    private CharacterAnimationHandler animationHandler;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        attackArea = gameObject.GetComponentInChildren<AttackArea>();
        animationHandler = gameObject.GetComponent<CharacterAnimationHandler>();
    }
    void OnMove(InputValue value)
    {
        movementDirection = value.Get<Vector2>();
        rb.velocity = movementDirection * movementSpeed;
        isMoving = movementDirection.sqrMagnitude > 0;
        if(isMoving) attackArea.RotateDirection((int) Mathf.Round(movementDirection.y));

        animationHandler.AnimateMovement(movementDirection);
        animationHandler.FlipGOHandler(movementDirection);
    }

}
