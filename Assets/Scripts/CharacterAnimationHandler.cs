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
            Vector3 newScaleVector = new Vector3(turnDirection, transform.localScale.y, transform.localScale.z);
            transform.localScale = newScaleVector;

            if(gameObject.GetComponentInChildren<Canvas>() != null)
            {
                GameObject canvas = gameObject.GetComponentInChildren<Canvas>().gameObject;
                Vector3 canvasLocalScale = canvas.transform.localScale;
                canvas.transform.localScale = new Vector3(Mathf.Abs(canvasLocalScale.x) * turnDirection, canvasLocalScale.y, canvasLocalScale.z);
            }
        }
    }
}
