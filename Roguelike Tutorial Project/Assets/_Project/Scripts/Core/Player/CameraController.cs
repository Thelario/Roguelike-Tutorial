using UnityEngine;

namespace Game
{
    namespace Core
    {
        namespace Player
        {
            public class CameraController : MonoBehaviour
            {
                [SerializeField] private float _smoothSpeed = 5f;
                [SerializeField] private float _zOffset = -10f;

                [SerializeField] private Transform _player;

                private Transform _t;

                private void Awake()
                {
                    _t = GetComponent<Transform>();
                }

                private void Update()
                {
                    CalculateCamPos();
                }

                private void CalculateCamPos()
                {
                    Vector2 playerPos = _player.position;
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 dir = mousePos - playerPos;
                    Vector2 newPos = playerPos + dir / 2;
                    Vector2 smoothPos = Vector2.Lerp(_t.position, newPos, _smoothSpeed * Time.deltaTime);

                    _t.position = new Vector3(smoothPos.x, smoothPos.y, _zOffset);
                }
            }
        }
    }
}
