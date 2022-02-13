using UnityEngine;
using Game.Characters.Shooting;

namespace Game.Destructibles
{
    public class Box : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _health;

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
