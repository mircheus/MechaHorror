using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UniRX
{
    public class SimpleEventInvoker : MonoBehaviour
    {
        [SerializeField] private Button eventButton;
        [SerializeField] private Button R3Button;

        public event Action OnSimpleEvent;

        public readonly Subject<Unit> onSimpleR3Event = new();

        private void Awake()
        {
            eventButton.onClick.AddListener(OnButtonClicked);
            R3Button.onClick.AddListener(OnR3ButtonClicked);
        }

        private void OnButtonClicked()
        {
            OnSimpleEvent?.Invoke();
        }

        private void OnR3ButtonClicked()
        {
            onSimpleR3Event.OnNext(Unit.Default);
        }
    }
}
