using UnityEngine;

namespace MaidCafe.Components.Agent.StateMachine
{
    public interface IAgent
    {
        Animator Animator { get; }
        AgentController AgentController { get; }
    }
}
