using System;
using System.Collections;
using System.Collections.Generic;
using Marten.Scripts;
using Marten.Scripts.PlayerAttacks;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour, IAttack
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private new SphereCollider collider;
    [SerializeField] private float defaultRange, attackDelay;
    
    private List<Collider> targetsInRange = new List<Collider>();
    private bool attacking = false;

    private void Start()
    {
        ChangeRange(defaultRange);
    }

    public void DoAttack()
    {
        Collider target = GetClosestTarget();
        if (target is null) return;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        Instantiate(projectilePrefab, transform.position, rotation).GetComponent<Projectile>()
            .SetEntityType(gameObject.CompareTag("Player") ? PlayerOrEnemy.Player : PlayerOrEnemy.Enemy);
    }

    public bool CanAttack()
    {
        return targetsInRange.Count > 0;
    }

    public void ChangeRange(float range)
    {
        collider.radius = range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Player")) return;
        
        if (!targetsInRange.Contains(other) && other.gameObject != gameObject)
        {
            targetsInRange.Add(other);
            if (!attacking)
            {
                attacking = true;
                StartCoroutine(Attack());    
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targetsInRange.Remove(other);
    }

    private IEnumerator Attack()
    {
        if (CanAttack())
        {
            DoAttack();
            yield return new WaitForSeconds(attackDelay);
            StartCoroutine(Attack());
        }
        else
        {
            attacking = false;
        }
    }
    
    private Collider GetClosestTarget()
    {
        Collider closest = null;
        float minDist = float.MaxValue;
        foreach (var target in targetsInRange)
        {
            if (target is null) continue;
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = target;
            }
        }
        return closest;
    }
}
