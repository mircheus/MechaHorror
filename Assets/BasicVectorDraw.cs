using UnityEditor;
using UnityEngine;

public class BasicVectorDraw : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        float thickness = 8f;
        int multiplier = 4;
        
        var transform1 = transform;
        var position = transform1.position;
        Gizmos.color = Color.red;
        Handles.DrawLine(position, position + transform1.right * multiplier, thickness);
        Handles.color = Color.green;
        Handles.DrawLine(position, position + transform1.up * multiplier, thickness);
        Handles.color = Color.blue;
        Handles.DrawLine(position, position + transform1.forward * multiplier, thickness);
        // Gizmos.color = Color.white;
        // Gizmos.DrawLine(Vector3.right, Vector3.up);
        // Gizmos.DrawLine(Vector3.right, Vector3.forward);
        // Gizmos.DrawLine(Vector3.up, Vector3.forward);
    }
}
