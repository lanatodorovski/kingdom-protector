using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float damadgeReceiveDuration = 1f;
    [SerializeField] private float deathDelay = 0.5f;
    [SerializeField] private bool isPlayer;

    private bool isAlive = true;
    private int currentHealth;

    private Slider slider;
    private CharacterAnimationHandler animationHandler;
    private void Awake()
    {
        currentHealth = maxHealth;
        animationHandler = GetComponent<CharacterAnimationHandler>();

        slider = GetComponentInChildren<Slider>();
        if (isPlayer)
        {
            slider = GameObject.FindGameObjectWithTag("Player").GetComponent<Slider>();
        }
        if(slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }
    }

    public void Hit(int damadge)
    {
        if (isAlive)
        {
            currentHealth -= damadge;
            Debug.Log(currentHealth);

            if(slider != null)slider.value = currentHealth;

            if (currentHealth <= 0)
            {
                StartCoroutine(Kill());
            }
            else
            {
                StartCoroutine(DealDamadge());
            }
        }
    }

    private IEnumerator Kill()
    {
        animationHandler.AnimateKilled();
        isAlive = false;
        GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSecondsRealtime(deathDelay);
        Destroy(gameObject);
    }
    private IEnumerator DealDamadge()
    {
        animationHandler.AnimateDamadged();
        yield return new WaitForSecondsRealtime(damadgeReceiveDuration);
    }
}
