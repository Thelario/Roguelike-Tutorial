using UnityEngine;

namespace Game
{
    namespace Core
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
