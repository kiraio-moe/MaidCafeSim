using System;

namespace MaidCafe.Components.Agent.StateMachine.States
{
    [Serializable]
    public class Walk : AgentStateBase
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Runner.AgentController.StartMove();
        }
    }
}
