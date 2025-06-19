using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject attackArea;
    [SerializeField] private float timeToAttack = 0.25f;
    [SerializeField] private float cooldownTime = 0.25f;
 
    private bool isAttacking  = false;
    private bool isCooled = true;
    private CharacterAnimationHandler animationHandler;

    private void Awake()
    {
        attackArea.SetActive(isAttacking);
        animationHandler = GetComponent<CharacterAnimationHandler>();
    }
    private void OnFire()
    {
        if (isCooled)
        {
            Fire();
        }
    }
    void Fire()
    {
        if (!isAttacking)
        {
            animationHandler.AnimateAttack();
            SetAttack(true);
            StartCoroutine(StopAttack());
        }
    }
    public void SetAttack(bool toggle)
    {
            isAttacking = toggle;
            attackArea.SetActive(isAttacking);
    }

    private IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(timeToAttack);
        SetAttack(false);
        StartCoroutine(CooldownPeriod());
    }

    private IEnumerator CooldownPeriod()
    {
        isCooled = false;
        yield return new WaitForSeconds(cooldownTime);
        isCooled = true;
    }

    //private void Animate()
    //{
    //    animator.SetBool("IsAttacking", true);
       
    //}

}
