using UnityEngine;
using UnityEngine.Events;
using MaidCafe.Components.Agent.StateMachine;
using MaidCafe.Components.Agent.StateMachine.States;
using OceanFSM;

namespace MaidCafe.Components.Agent
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(SpriteRenderer))]
	public abstract class AgentAnimator : MonoBehaviour, IAgent
	{
		[Header("States")]
		[SerializeField]
		Idle m_Idle;
		
		[SerializeField]
		Walk m_Walk;
		
		[Header("Events")]
        [SerializeField]
        UnityEvent<string> m_OnStateChanged;
        
		Animator animator;
		AgentController agentController;
		IAutonomousMachine<IAgent> stateMachine;
		
		public Animator Animator => animator;
		public AgentController AgentController => agentController;
		public UnityEvent<string> OnStateChanged
		{
			get => m_OnStateChanged;
			set => m_OnStateChanged = value;
		}
		public IAutonomousMachine<IAgent> StateMachine => stateMachine;
		public Idle Idle => m_Idle;
		public Walk Walk => m_Walk;
		
		void Awake()
		{
			animator = GetComponent<Animator>();
			agentController = GetComponentInParent<AgentController>();
			
			stateMachine = new AutonomousBuilder<IAgent>(this)
                .SetInitialState(nameof(Idle))
                .AddState(Idle)
                .AddState(Walk)
                .Build();
                
            stateMachine.AddCommand(Walk.Name)
            	.SetTargetState<Walk>()
            	.SetCondition(() => stateMachine.CurrentState is Idle && !agentController.SplineAnimate.IsPlaying)
            	.OnSuccess(() => Debug.Log("Agent is walking..."));
//             	.OnFailure(() => Debug.LogError("Failed to set Agent state: Walk"));

            stateMachine.ExecuteCommand(Walk.Name);
		}
		
		void OnEnable()
		{
			stateMachine.Start();
            stateMachine.StateChanged += StateChanged;
		}
		
		void OnDisable()
		{
			stateMachine.Stop();
			stateMachine.StateChanged -= StateChanged;
		}
		
		void Start()
		{
//             agentController.OnStart.AddListener(() => Debug.Log("TEST"));
		}
		
		void StateChanged(State<IAgent> state)
		{
			m_OnStateChanged?.Invoke(state.GetType().Name);
			Debug.Log($"State: {stateMachine.CurrentState.GetType().Name}");
		}
		
		void Update()
		{
			stateMachine.Update(Time.deltaTime);
		}
	}
}
