using System;
using UnityEngine;
using MaidCafe.Components.Agent.StateMachine;

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
