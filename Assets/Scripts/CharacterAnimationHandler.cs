using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationHandler : MonoBehaviour
{
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    public void AnimateMovement(Vector2 movementDirection)
    {
        myAnimator.SetFloat("Speed", movementDirection.sqrMagnitude);
        if (myAnimator.GetFloat("Speed") > 0)
        {
            myAnimator.SetFloat("Horizontal", movementDirection.x);
            myAnimator.SetFloat("Vertical", movementDirection.y);


        }
    }
    public void  AnimateAttack()
    {
        myAnimator.SetTrigger("Attack");
    }
    public void AnimateKilled()
    {
        myAnimator.SetTrigger("Killed");
    }
    public void AnimateDamadged()
    {
        myAnimator.SetTrigger("Damadged");
    }
    public void FlipGOHandler(Vector2 movementDirection, bool isEnemy = false)
    {
        if (myAnimator.GetFloat("Speed") > 0)
        {
            int turnDirection= (movementDirection.x < 0 && (movementDirection.y == 0 || isEnemy)) ? -1 : 1;
            transform.localScale = new Vector3(turnDirection, transform.localScale.y, transform.localScale.z);
        }
    }

    public void FlipFlipGOHandler(int turnDirection)
    {
        transform.localScale = new Vector3(turnDirection, transform.localScale.y, transform.localScale.z);
    }
}
