using System;
using UnityEngine;
using MaidCafe.Components.Agent.StateMachine;

namespace MaidCafe.Components.Agent.Customer.States
{
    [Serializable]
    public class Eat : AgentStateBase
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Eating...");
        }
    }
}
