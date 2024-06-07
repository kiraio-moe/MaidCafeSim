using System;
using UnityEngine;
using OceanFSM;

namespace MaidCafe.Components.Agent.StateMachine
{
	[Serializable]
	public abstract class AgentStateBase : State<IAgent>
	{
		[SerializeField]
		[Tooltip("The Animation name of the state. If not explicitly stated, will use the Type name of the state.")]
		string m_AnimationStateName;
		
		[SerializeField, Range(0f, 1f)]
		[Tooltip("The transition duration when changing state.")]
		float m_TransitionDuration = 0.15f;
		
		int animationStateHash;
		
		/// <summary>
		/// The Animation name tied with the state.
		/// <summary>
		public string AnimationStateName
		{
			get => m_AnimationStateName;
			set => m_AnimationStateName = value;
		}
		
		/// <summary>
		/// The transition duration when changing state.
		/// </summary>
		public float TransitionDuration
		{
			get => m_TransitionDuration;
			set => m_TransitionDuration = value;
		}
		
		public string Name => GetType().Name;

		public override void OnInitialize()
        {
            animationStateHash = Animator.StringToHash(AnimationStateName);
			m_AnimationStateName ??= GetType().Name;
        }
        
        public override void OnEnter()
        {
            Runner.Animator.CrossFade(animationStateHash, TransitionDuration, 0);
        }
        
        public override void OnExit()
        {
        
        }
        
        public override void OnUpdate(float deltaTime)
        {
        
        }
	}
}
