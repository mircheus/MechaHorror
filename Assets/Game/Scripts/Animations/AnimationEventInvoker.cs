using System;
using UnityEngine;

public class AnimationEventInvoker : MonoBehaviour
{
    public event Action<AttackType> OnRangeAttack;

    public void InvokeAttack(AttackType attackType)
    {
        OnRangeAttack?.Invoke(attackType);
    }
}

public enum AttackType
{
    RangeAttack, 
    ShootAttack,
    BallKickAttack
}
