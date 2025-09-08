using System;

namespace Levels.Tower
{
    public  class MainTowerModel
    {
        public System.Action OnDie;
        public System.Action<float> OnDamaged;

        public float Health { get; set; } = 50;

        public void GetDamage(float damage)
        {
            Health -= damage;
            OnDamaged?.Invoke(Health);
            if(Health <= 0)
            {
                Health = 0;
                OnDie?.Invoke();
            }
        }
    }
}
