using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public float currentHealth;
    public float maxHealth = 100;
    private bool isShielded;

    public bool Shielded
    {
        get { return isShielded; }
        set { isShielded = value; }
    }

    private Animator _animator;

    private Image healthImage;

    private void Awake()
    {
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
        healthImage = GameObject.Find("HealthOrb").GetComponent<Image>();
    }

    public void TakeDamage(float amount)
    {
        if (!isShielded)
        {
            currentHealth -= amount;
        }

        healthImage.fillAmount = currentHealth / maxHealth;

        if (currentHealth<=0)
        {
            _animator.SetBool("Death",true);
        }
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthImage.fillAmount = currentHealth / maxHealth;
    }
}