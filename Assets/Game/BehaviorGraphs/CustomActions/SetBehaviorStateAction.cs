using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Behavior State", story: "Set [BehaviorState] to [value]", category: "Action", id: "7f3e914a5fa4f88468da30f4cd0fc439")]
public partial class SetBehaviorStateAction : Action
{
    [SerializeReference] public BlackboardVariable<BehaviorState> BehaviorState;
    [SerializeReference] public BlackboardVariable<BehaviorState> Value;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

