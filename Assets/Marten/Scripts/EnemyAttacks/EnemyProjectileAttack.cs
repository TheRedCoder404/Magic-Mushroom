using System;
using System.Collections;
using System.Collections.Generic;
using Marten.Scripts;
using Marten.Scripts.PlayerAttacks;
using UnityEngine;

public class EnemyProjectileAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private new SphereCollider collider;
    [SerializeField] private float defaultRange = 6f, attackDelay = 2f, startDelay = 1f;
    
    private List<Collider> targetsInRange = new List<Collider>();
    private bool attacking = false, allowedToAttack = false;
    private float projectileSpeed;

    private void Start()
    {
        ChangeRange(defaultRange);
        if (projectilePrefab is not null)
        {
            projectileSpeed = projectilePrefab.GetComponent<Projectile>().GetSpeed();
        }
        StartCoroutine(StartDelay());
    }

    public IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        allowedToAttack = true;
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
            .setSender(transform.parent.gameObject);
    }

    public bool CanAttack()
    {
        return targetsInRange.Count > 0 && allowedToAttack;
    }

    public void ChangeRange(float range)
    {
        collider.radius = range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other)) return;
        
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
    
    private bool IsPlayer(Collider other)
    {
        return other.gameObject.CompareTag("Player");
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
