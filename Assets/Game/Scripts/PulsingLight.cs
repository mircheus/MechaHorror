using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(Light))]
    public class PulsingLight : MonoBehaviour
    {
        [SerializeField] private float minIntensity = 0f;
        [SerializeField] private float maxIntensity = 2f;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Ease easeType = Ease.InOutSine;

        private Light lightComponent;
        private Tween pulseTween;

        private void Start()
        {
            lightComponent = GetComponent<Light>();

            // Start pulsing
            StartPulsing();
        }

        private void StartPulsing()
        {
            // Create a looping tween to pulse the intensity
            pulseTween = DOTween.To(
                    () => lightComponent.intensity,
                    x => lightComponent.intensity = x,
                    maxIntensity,
                    duration
                )
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(easeType);
        }

        private void OnDisable()
        {
            // Kill tween when the object is disabled to avoid memory leaks
            pulseTween?.Kill();
        }
    }
}