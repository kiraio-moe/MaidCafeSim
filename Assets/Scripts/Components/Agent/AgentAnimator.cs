using MaidCafe.Components.Agent.StateMachine;
using MaidCafe.Components.Agent.StateMachine.States;
using OceanFSM;
using UnityEngine;

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

        Animator animator;
        AgentController agentController;
        AutonomousBuilder<IAgent> builder;
        IAutonomousMachine<IAgent> stateMachine;

        public Animator Animator => animator;
        public AgentController AgentController => agentController;

        public delegate void OnStateChangedAction();
        public event OnStateChangedAction OnStateChanged;

        public IAutonomousMachine<IAgent> StateMachine
        {
            get => stateMachine;
            set => stateMachine = value;
        }
        public Idle Idle => m_Idle;
        public Walk Walk => m_Walk;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            agentController = GetComponentInParent<AgentController>();

            // Set initial state
            builder = new AutonomousBuilder<IAgent>(this)
                .SetInitialState(nameof(Idle))
                .AddStates(Idle, Walk);
        }

        protected void BuildStateMachine()
        {
            StateMachine = builder.Build();

            // Register State Machine Commands
            StateMachine
                .AddCommand(Idle.Name)
                .SetTargetState<Idle>()
                .SetCondition(() => !AgentController.IsMoving);
            StateMachine
                .AddCommand(Walk.Name)
                .SetTargetState<Walk>()
                .SetCondition(() => StateMachine.CurrentState is Idle && !AgentController.IsMoving);

            StateMachine.ExecuteCommand(Idle.Name);
            Debug.Log($"Initial state: {StateMachine.CurrentState.GetType().Name}");
        }

        void OnEnable()
        {
            StateMachine?.Start();
            StateMachine.StateChanged += StateChanged;
        }

        void OnDisable()
        {
            StateMachine?.Stop();
            StateMachine.StateChanged -= StateChanged;
        }

        void Update()
        {
            StateMachine?.Update(Time.deltaTime);
        }

        /// <summary>
        /// Invoke OnStateChanged event.
        /// </summary>
        /// <param name="state"></param>
        void StateChanged(State<IAgent> state)
        {
            Debug.Log($"State changed to: {state.GetType().Name}");
            OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Register a <paramref name="state"/> to the State Machine.
        /// </summary>
        /// <param name="state"></param>
        /// <returns>State Machine Autonomous Builder</returns>
        protected AutonomousBuilder<IAgent> RegisterState(State<IAgent> state)
        {
            return builder?.AddState(state);
        }

        /// <summary>
        /// Register <paramref name="states"/> to the State Machine.
        /// </summary>
        /// <param name="states"></param>
        /// <returns>State Machine Autonomous Builder</returns>
        protected AutonomousBuilder<IAgent> RegisterState(params State<IAgent>[] states)
        {
            return builder?.AddStates(states);
        }
    }
}
