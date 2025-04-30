using System;
using UnityEngine;

public class AnimationEventInvoker : MonoBehaviour
{
    public event Action OnRangeAttack;
    
    public void InvokeAttack()
    {
        OnRangeAttack?.Invoke();
        Debug.Log("Attack invoked!");
    }
}
