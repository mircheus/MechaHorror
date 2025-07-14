using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UniRX
{
    public class ParamsEventInvoker : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI buttonText;

        public readonly Subject<int> paramsR3Event = new();
        private int _counter = 0;

        private void Awake()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _counter++;
            buttonText.text = 
        }
    }
}
