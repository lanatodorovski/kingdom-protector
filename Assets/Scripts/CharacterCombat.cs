using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCombat : MonoBehaviour
{
    [SerializeField] private GameObject attackArea;
    [SerializeField] private float timeToAttack = 0.25f;
 
    private bool isAttacking  = false;
    private Animator animator;
    private CharacterAnimationHandler animationHandler;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationHandler = GetComponent<CharacterAnimationHandler>();
    }
    private void Update()
    {
        if (!isAttacking)
        {
            //animationHandler.AnimateAttack();
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
    }

}
