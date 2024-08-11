using System;
using UnityEngine;

namespace DefaultNamespace.Health
{
    public class Health : MonoBehaviour
    {
        private float CurrentHealth;
        public float MaxHealth = 100;

        private void Start()
        {
            CurrentHealth = MaxHealth;
        }
        
        public void SetMaxHealth(float health)
        {
            MaxHealth = health;
            CurrentHealth = MaxHealth;
        }
        
        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}