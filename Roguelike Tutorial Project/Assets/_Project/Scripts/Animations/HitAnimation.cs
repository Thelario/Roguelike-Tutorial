using System.Collections;
using UnityEngine;

namespace Game
{
    namespace Animations
    {
        [System.Serializable]
        public class HitAnimation
        {
            public Color agentColor = new Color(1f, 1f, 1f);
            [SerializeField] private Color _hitColor = new Color(1f, 0f, 0f);
            [SerializeField] private Color _invencibilityColor = new Color(0f, 1f, 0f);

            [SerializeField] private SpriteRenderer _agentRenderer;
            [SerializeField] private float _timeToWaitForColorChange = 0.05f;

            public IEnumerator Co_HitColorChange(bool makeInvencible, float invencibilityTime)
            {
                _agentRenderer.color = _hitColor;

                yield return new WaitForSeconds(_timeToWaitForColorChange);

                _agentRenderer.color = agentColor;

                if (makeInvencible)
                    yield return Co_SetInvencibilityColor(invencibilityTime);
            }

            private IEnumerator Co_SetInvencibilityColor(float invencibilitytime)
            {
                _agentRenderer.color = _invencibilityColor;

                yield return new WaitForSeconds(invencibilitytime);

                _agentRenderer.color = agentColor;
            }
        }
    }
}
