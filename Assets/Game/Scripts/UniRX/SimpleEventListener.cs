using System;
using UniRx;
using UnityEngine;

namespace Game.Scripts.UniRX
{
    public class SimpleEventListener : MonoBehaviour
    {
        [SerializeField] private SimpleEventInvoker simpleEventInvoker;
        
        private CompositeDisposable _disposable = new CompositeDisposable();

        private void OnEnable()
        {
            simpleEventInvoker.OnSimpleEvent += HandleSimpleEvent;

            simpleEventInvoker
                .onSimpleR3Event
                .Subscribe(_ => OnSimpleR3Event()) // Subscribe to the R3 event
                .AddTo(_disposable); // This will ensure the subscription is disposed of when the component is destroyed
        }

        private void OnDisable()
        {
            simpleEventInvoker.OnSimpleEvent -= HandleSimpleEvent;
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        private void HandleSimpleEvent()
        {
            Debug.Log("SimpleEvent triggered");
        }

        private void OnSimpleR3Event()
        {
            Debug.Log("UniRX SimpleR3Event triggered");
        }
    }
}