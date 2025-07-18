using UnityEngine;

namespace Marten.Scripts
{
    public interface IDamageable
    {
        public void TakeDamage(float damage, GameObject source = null);
    }
}
