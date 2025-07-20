namespace Marten.Scripts
{
    public interface IPlayerAttack
    {
        public void DoAttack();
        public bool CanAttack();
        public void UpdateRange();
        public void UpdateDamage();
        public void UpdateAttackSpeed();
        public void UpdateCritChance();
        public void UpdateCritDamage();
        public void UpdateLifesteal();
    }
}
