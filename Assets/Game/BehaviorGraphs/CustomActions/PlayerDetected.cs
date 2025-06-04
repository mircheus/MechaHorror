using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/PlayerDetected")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "PlayerDetected", message: "[Enemy] detected [Player]", category: "Events", id: "6891e1ade4c5416e437b54448c97c89a")]
public partial class PlayerDetected : EventChannelBase
{
    public delegate void PlayerDetectedEventHandler(GameObject Enemy, GameObject Player);
    public event PlayerDetectedEventHandler Event; 

    public void SendEventMessage(GameObject Enemy, GameObject Player)
    {
        Event?.Invoke(Enemy, Player);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<GameObject> EnemyBlackboardVariable = messageData[0] as BlackboardVariable<GameObject>;
        var Enemy = EnemyBlackboardVariable != null ? EnemyBlackboardVariable.Value : default(GameObject);

        BlackboardVariable<GameObject> PlayerBlackboardVariable = messageData[1] as BlackboardVariable<GameObject>;
        var Player = PlayerBlackboardVariable != null ? PlayerBlackboardVariable.Value : default(GameObject);

        Event?.Invoke(Enemy, Player);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        PlayerDetectedEventHandler del = (Enemy, Player) =>
        {
            BlackboardVariable<GameObject> var0 = vars[0] as BlackboardVariable<GameObject>;
            if(var0 != null)
                var0.Value = Enemy;

            BlackboardVariable<GameObject> var1 = vars[1] as BlackboardVariable<GameObject>;
            if(var1 != null)
                var1.Value = Player;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as PlayerDetectedEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as PlayerDetectedEventHandler;
    }
}

