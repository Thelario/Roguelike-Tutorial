using UnityEngine;
using Game.Characters.Shooting;
using Game.Animations;
using Game.Managers;

namespace Game
{
    namespace Characters
    {
        namespace Enemies
        {
            public class Enemy : MonoBehaviour, IDamageable
            {
                [SerializeField] private int _health;

                [Header("HitAnimation")]
                [SerializeField] private HitAnimation _hitAnimation;

                public void TakeDamage(int damage)
                {
                    SoundManager.I.PlaySound(SoundType.enemyHit);
                    _health -= damage;
                    if (_health <= 0)
                        Die();

                    StartCoroutine(_hitAnimation.Co_HitColorChange(false, 0f));
                }

                private void Die()
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
