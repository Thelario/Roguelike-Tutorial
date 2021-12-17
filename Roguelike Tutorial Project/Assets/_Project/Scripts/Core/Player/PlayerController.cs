using Game.Core.Shooting;
using UnityEngine;

namespace Game
{
    namespace Core
    {
        namespace Player
        {
            public class PlayerController : MonoBehaviour
            {
                [SerializeField] private float _moveSpeed;
                [SerializeField] private float _fireRate;
                [SerializeField] private Transform _shootPoint;
                [SerializeField] private GameObject _bulletPrefab;

                private float _horizontal;
                private float _vertical;
                private float _shootCounter;

                private Rigidbody2D _rb;
                private Transform _t;

                private void Awake()
                {
                    _rb = GetComponent<Rigidbody2D>();
                    _t = GetComponent<Transform>();
                }

                private void Start()
                {
                    _shootCounter = 0f;
                }

                private void Update()
                {
                    GetMoveInput();

                    RotatePlayer();

                    CheckShoot();
                }

                private void FixedUpdate()
                {
                    ChangePlayerVelocity();
                }

                private void GetMoveInput()
                {
                    _horizontal = Input.GetAxisRaw("Horizontal");
                    _vertical = Input.GetAxisRaw("Vertical");
                }

                private void ChangePlayerVelocity()
                {
                    _rb.velocity = new Vector2(_horizontal, _vertical) * Time.fixedDeltaTime * _moveSpeed;
                }

                private void RotatePlayer()
                {
                    Vector2 dirToMouse = GetDirToMouse();
                    Vector2 player = _t.up;
                    float angle = Vector2.SignedAngle(player, dirToMouse);
                    _t.Rotate(0f, 0f, angle);
                }

                private Vector2 GetDirToMouse()
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    return mousePos - _t.position;
                }

                private void CheckShoot()
                {
                    _shootCounter -= Time.deltaTime;
                    if (_shootCounter <= 0f)
                        _shootCounter = 0f;

                    if (Input.GetMouseButton(0))
                    {
                        if (_shootCounter <= 0f)
                            Shoot();
                    }
                }

                private void Shoot()
                {
                    _shootCounter = _fireRate;
                    GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
                    bullet.GetComponent<Bullet>().SetDir(GetDirToMouse());
                }
            }
        }
    }
}
