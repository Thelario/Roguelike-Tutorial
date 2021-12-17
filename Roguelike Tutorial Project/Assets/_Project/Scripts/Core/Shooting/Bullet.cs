using UnityEngine;

namespace Game
{
    namespace Core
    {
        namespace Shooting
        {
            public class Bullet : MonoBehaviour
            {
                [SerializeField] private float _bulletMoveSpeed;

                private Vector2 _dir = Vector2.zero;
                private Rigidbody2D _rb;

                private void Awake()
                {
                    _rb = GetComponent<Rigidbody2D>();
                }

                private void Update()
                {
                    Rotate();
                }

                private void FixedUpdate()
                {
                    Move();
                }

                private void OnTriggerEnter2D(Collider2D collision)
                {
                    Destroy(gameObject);
                }

                private void Move()
                {
                    if (_dir == Vector2.zero)
                        return;

                    _rb.velocity = _bulletMoveSpeed * Time.deltaTime * new Vector2(_dir.normalized.x, _dir.normalized.y);
                }

                private void Rotate()
                {
                    if (_dir == Vector2.zero)
                        return;

                    transform.up = _dir;
                }

                public void SetDir(Vector2 dir) { _dir = dir; }
            }
        }
    }
}
