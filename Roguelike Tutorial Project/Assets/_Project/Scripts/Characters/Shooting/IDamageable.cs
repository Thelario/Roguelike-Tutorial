using UnityEngine;

namespace Game
{
    namespace Characters
    {
        namespace Shooting
        {
            public interface IDamageable
            {
                public void TakeDamage(int damage);
            }
        }
    }
}
