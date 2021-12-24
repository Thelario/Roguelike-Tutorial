using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    namespace Managers
    {
        public class BulletPoolManager : Singleton<BulletPoolManager>
        {
            [SerializeField] private int _initialNumberOfBullets;
            [SerializeField] private Transform _bulletsParent;
            [SerializeField] private GameObject _bulletPrefab;

            private List<GameObject> _bulletPool = new List<GameObject>();

            private void Start()
            {
                _bulletPool = GenerateBullets(_initialNumberOfBullets);
            }

            private List<GameObject> GenerateBullets(int amountOfBullets)
            {
                for (int i = 0; i < amountOfBullets; i++)
                {
                    GameObject bullet = Instantiate(_bulletPrefab, _bulletsParent);
                    _bulletPool.Add(bullet);
                    bullet.SetActive(false);
                }

                return _bulletPool;
            }

            public GameObject RequestBullet()
            {
                foreach (GameObject bullet in _bulletPool)
                {
                    if (!bullet.activeInHierarchy)
                    {
                        bullet.SetActive(true);
                        return bullet;
                    }
                }

                _bulletPool = GenerateBullets(1);
                return RequestBullet();
            }
        }
    }
}
