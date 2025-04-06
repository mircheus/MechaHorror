using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class DamagePopUpGenerator : MonoBehaviour
    {
        public static DamagePopUpGenerator current;
        public GameObject prefab;

        private void Awake()
        {
            current = this;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F10))
            {
                CreatePopUp(transform.position, Random.Range(300, 600).ToString(), Color.yellow);
            }
        }

        public void CreatePopUp(Vector3 position, string text, Color color)
        {
            var popup = Instantiate(prefab, position, Quaternion.identity);
            var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            temp.text = text;
            temp.faceColor = color;

            //Destroy Timer
            Destroy(popup, 1f);
        }

        public void CreatePopUpDefault(Vector3 position)
        {
            CreatePopUp(position, Random.Range(300, 600).ToString(), Color.yellow);
        }
    }
}
