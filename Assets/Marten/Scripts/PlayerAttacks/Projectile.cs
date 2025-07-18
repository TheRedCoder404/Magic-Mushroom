using System.Collections;
using UnityEngine;

namespace Marten.Scripts.PlayerAttacks
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed, dmg, selfDestructTime;
        
        private PlayerOrEnemy entityType;

        private void Start()
        {
            StartCoroutine(SelfDestruct());
        }
        
        public void SetEntityType(PlayerOrEnemy type)
        {
            entityType = type;
        }

        public PlayerOrEnemy GetEntityType()
        {
            return entityType;
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
            if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                if ((entityType == PlayerOrEnemy.Player && other.gameObject.CompareTag("Player")) || (entityType == PlayerOrEnemy.Enemy && other.gameObject.CompareTag("Enemy"))) return;
                damageable.TakeDamage(dmg);
                Destroy(gameObject);
            }
        }
    }
}
