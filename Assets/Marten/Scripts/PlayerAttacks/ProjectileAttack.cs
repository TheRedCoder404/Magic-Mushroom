using System;
using System.Collections;
using System.Collections.Generic;
using Marten.Scripts;
using Marten.Scripts.PlayerAttacks;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour, IPlayerAttack
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private new SphereCollider collider;
    [SerializeField] private float defaultRange, defaultAttackDelay;
    
    private List<Collider> targetsInRange = new List<Collider>();
    private bool attacking = false;
    private float projectileSpeed;
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        ChangeRange(defaultRange);
        if (projectilePrefab is not null)
        {
            projectileSpeed = projectilePrefab.GetComponent<Projectile>().GetSpeed();
        }
    }

    public void DoAttack()
    {
        Collider target = GetClosestTarget();
        if (target is null) return;

        Vector3 targetPosition = target.transform.position;
        Vector3 targetVelocity = Vector3.zero;

        if (target.attachedRigidbody is not null)
        {
            targetVelocity = target.attachedRigidbody.linearVelocity;
        }
        
        Vector3 toTarget = targetPosition - transform.position;
        float distance = toTarget.magnitude;
        
        float timeToHit = distance / projectileSpeed;
        
        Vector3 futurePosition = targetPosition + targetVelocity * timeToHit;
        
        Vector3 direction = (futurePosition - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        Instantiate(projectilePrefab, transform.position, rotation).GetComponent<Projectile>()
            .SetEntityType(transform.parent.gameObject.CompareTag("Player") ? PlayerOrEnemy.Player : PlayerOrEnemy.Enemy)
            .setDmg(playerStats.damage)
            .setCritChance(playerStats.critChance)
            .setCritDamage(playerStats.critDamage)
            .setLifesteal(playerStats.lifesteal)
            .setSender(transform.parent.gameObject);
    }

    public bool CanAttack()
    {
        return targetsInRange.Count > 0;
    }

    public void ChangeRange(float range)
    {
        collider.radius = range;
    }

    public void UpdateRange()
    {
        ChangeRange(defaultRange * playerStats.range);
    }

    public void UpdateDamage()
    {
        // nothing to update, handle by itself
    }

    public void UpdateAttackSpeed()
    {
        // nothing to update, handle by itself
    }

    public void UpdateCritChance()
    {
        // nothing to update, handle by itself
    }

    public void UpdateCritDamage()
    {
        // nothing to update, handle by itself
    }

    public void UpdateLifesteal()
    {
        // nothing to update, handle by itself
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsEnemy(other)) return;
        
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
    
    private bool IsEnemy(Collider other)
    {
        return other.gameObject.CompareTag("Enemy");
    }

    private IEnumerator Attack()
    {
        if (CanAttack())
        {
            DoAttack();
            yield return new WaitForSeconds(defaultAttackDelay * (1 / playerStats.attackSpeed));
            StartCoroutine(Attack());
        }
        else
        {
            attacking = false;
        }
    }
    
    private Collider GetClosestTarget()
    {
        targetsInRange.RemoveAll(c => !c || !c.gameObject);
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
