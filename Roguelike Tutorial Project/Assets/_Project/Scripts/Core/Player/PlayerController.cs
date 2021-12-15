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

                private float _horizontal;
                private float _vertical;

                private Rigidbody2D _rb;
                private Transform _t;

                private void Awake()
                {
                    _rb = GetComponent<Rigidbody2D>();
                    _t = GetComponent<Transform>();
                }

                private void Update()
                {
                    GetMoveInput();

                    RotatePlayer();
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
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 dirToMouse = mousePos - _t.position;
                    Vector2 player = _t.up;
                    float angle = Vector2.SignedAngle(player, dirToMouse);
                    _t.Rotate(0f, 0f, angle);
                }
            }
        }
    }
}
