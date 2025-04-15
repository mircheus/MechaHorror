using UnityEngine;
using VFavorites.Libs;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.Destroy();
        // Check if the object has a Target component
        Debug.Log("Object destroyed: " + other.gameObject.name + " at position: " + other.transform.position);
    }
}
