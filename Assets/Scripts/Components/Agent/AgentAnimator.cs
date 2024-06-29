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
        IAutonomousMachine<IAgent> stateMachine;

        public Animator Animator => animator;
        public AgentController AgentController => agentController;

        public delegate void OnStateChangedAction();
        public event OnStateChangedAction OnStateChanged;

        public IAutonomousMachine<IAgent> StateMachine
        {
            get => stateMachine;
            private set => stateMachine = value;
        }
        public Idle Idle => m_Idle;
        public Walk Walk => m_Walk;

        void Awake()
        {
            animator = GetComponent<Animator>();
            agentController = GetComponentInParent<AgentController>();

            // Set initial state
            StateMachine = new AutonomousBuilder<IAgent>(this)
                .SetInitialState(nameof(Idle))
                .AddState(Idle)
                .AddState(Walk)
                .Build();

            // Register State Commands
            StateMachine
                .AddCommand(Idle.Name)
                .SetTargetState<Idle>()
                .SetCondition(() => !AgentController.IsMoving);
            StateMachine
                .AddCommand(Walk.Name)
                .SetTargetState<Walk>()
                .SetCondition(() => StateMachine.CurrentState is Idle && !AgentController.IsMoving);

            StateMachine.ExecuteCommand(Idle.Name);
            Debug.Log($"Init State: {StateMachine.CurrentState.GetType().Name}");
        }

        void OnEnable()
        {
            StateMachine.Start();
            StateMachine.StateChanged += StateChanged;
        }

        void OnDisable()
        {
            StateMachine.Stop();
            StateMachine.StateChanged -= StateChanged;
        }

        void StateChanged(State<IAgent> state)
        {
            OnStateChanged?.Invoke();
            Debug.Log($"Change State: {StateMachine.CurrentState.GetType().Name}");
        }

        void Update()
        {
            StateMachine.Update(Time.deltaTime);
        }
    }
}
