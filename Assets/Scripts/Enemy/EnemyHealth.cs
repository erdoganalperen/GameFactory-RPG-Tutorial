using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float currentHealth;
    private Animator _animator;
    public float maxHealth = 100f;
    [SerializeField] private Image enemyHealthBar;
    private SphereCollider targetCollider;

    private void Awake()
    {
        targetCollider = GetComponent<SphereCollider>();
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        enemyHealthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth > 0)
        {
            _animator.SetTrigger("Hit");
        }

        if (currentHealth <= 0)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            Canvas canvas = enemyHealthBar.gameObject.GetComponentInParent<Canvas>();
            if (targetCollider.gameObject.activeInHierarchy)
            {
                targetCollider.gameObject.SetActive(false);
            }
            if (canvas.gameObject.activeInHierarchy)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
}