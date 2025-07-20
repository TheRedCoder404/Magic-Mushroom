using System.Collections;
using UnityEngine;

namespace Marten.Scripts.PlayerAttacks
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5, defaultDamage = 5, selfDestructTime = 5;
        
        private PlayerOrEnemy entityType;
        private GameObject sender;
        private float damage = 5, critChance = 0, critDamage = 1, lifeSteal = 0;

        private void Start()
        {
            StartCoroutine(SelfDestruct());
            damage = defaultDamage;
        }
        
        public Projectile SetEntityType(PlayerOrEnemy type)
        {
            entityType = type;
            return this;
        }
        public Projectile setDmg(float damageMultiplier = 1)
        {
            damage = damageMultiplier * defaultDamage;
            return this;
        }

        public Projectile setCritChance(float critChance)
        {
            this.critChance = critChance;
            return this;
        }

        public Projectile setCritDamage(float critChance)
        {
            this.critDamage = critChance;
            return this;
        }

        public Projectile setLifesteal(float lifeSteal)
        {
            this.lifeSteal = lifeSteal;
            return this;
        }

        public Projectile setSender(GameObject sender)
        {
            this.sender = sender;
            return this;
        }

        public PlayerOrEnemy GetEntityType()
        {
            return entityType;
        }

        public float GetSpeed()
        {
            return speed;
        }

        void Update()
        {
            transform.localPosition += transform.forward * (speed * Time.deltaTime);
        }
    
        private IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(selfDestructTime);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other is null) return;
            if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                if ((entityType == PlayerOrEnemy.Player && other.gameObject.CompareTag("Player")) || (entityType == PlayerOrEnemy.Enemy && other.gameObject.CompareTag("Enemy"))) return;

                if (Random.Range(0f, 1f) <= critChance)
                {
                    damageable.TakeDamage(damage * critDamage);
                }
                else
                {
                    damageable.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
    }
}
