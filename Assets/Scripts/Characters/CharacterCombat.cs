using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterCombat : MonoBehaviour
{
    [SerializeField] private GameObject attackArea;
    [SerializeField] private float timeToAttack = 0.25f;
 
    private bool isAttacking  = false;
    private CharacterAnimationHandler animationHandler;

    protected virtual void Awake()
    {        
        animationHandler = GetComponent<CharacterAnimationHandler>();
        attackArea.SetActive(isAttacking);
    }
    private void Update()
    {
        Fire();
    }
    protected virtual void SetAttack(bool toggle)
    {
        isAttacking = toggle;
        attackArea.SetActive(isAttacking);
    }
    
    protected virtual IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(timeToAttack);
        SetAttack(false);
    }
    protected void Fire()
    {
        if (!isAttacking)
        {
            animationHandler.AnimateAttack();
            SetAttack(true);
            StartCoroutine(StopAttack());
        }
    }
}
