using Game.Characters.Shooting;
using Game.Managers;
using UnityEngine;

namespace Game
{
    namespace Characters
    {
        namespace Player
        {
            public class PlayerController : MonoBehaviour
            {
                [SerializeField] private float _moveSpeed;
                [SerializeField] private float _fireRate;
                [SerializeField] private Transform _shootPoint;
                [SerializeField] private CameraController _cam;
                [SerializeField] private GameObject _bulletParticles;

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
                    if (_horizontal != 0f || _vertical != 0f)
                        SoundManager.I.PlaySound(SoundType.playerWalk);

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
                    GameObject bullet = BulletPoolManager.I.RequestBullet();
                    bullet.transform.position = _shootPoint.position;
                    bullet.GetComponent<Bullet>().SetDir(GetDirToMouse());
                    _cam.AnimateCamera();
                    SoundManager.I.PlaySound(SoundType.playerShoot);
                    Destroy(Instantiate(_bulletParticles, _shootPoint.position, _shootPoint.rotation), 0.5f);
                }
            }
        }
    }
}
