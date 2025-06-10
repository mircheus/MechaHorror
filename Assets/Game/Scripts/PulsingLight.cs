using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    public class PulsingLight : MonoBehaviour
    {
        [SerializeField] private Light lightComponent;
        [SerializeField] private float minIntensity = 0f;
        [SerializeField] private float maxIntensity = 2f;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Ease easeType = Ease.InOutSine;

        private Tween pulseTween;

        private void Start()
        {
            // lightComponent = GetComponent<Light>();

            // Start pulsing
            // StartPulsing();
        }

        public void Pulse()
        {
            pulseTween?.Kill();
            // Create a looping tween to pulse the intensity
            pulseTween = DOTween.To(
                    () => lightComponent.intensity,
                    x => lightComponent.intensity = x,
                    maxIntensity,
                    duration
                )
                .SetEase(easeType)
                .OnComplete(() =>
                {
                    // Reset the intensity back to min after pulsing
                    lightComponent.intensity = minIntensity;
                });
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