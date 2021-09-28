using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDamage : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float radius = .5f;
    public float damageCount = 10f;

    private EnemyHealth _enemyHealth;
    protected bool collided;
    internal virtual void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        foreach (Collider hit in hits)
        {
            _enemyHealth = hit.gameObject.GetComponent<EnemyHealth>();
            collided = true;
        }

        if (collided)
        {
            _enemyHealth.TakeDamage(damageCount);
            enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
