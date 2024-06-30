using UnityEngine;
using MaidCafe.Components.Agent.Customer.States;
using MaidCafe.Components.Agent.StateMachine.States;

namespace MaidCafe.Components.Agent.Customer
{
    [AddComponentMenu("Maid Cafe/Components/Agent/Customer Animator")]
    public class CustomerAnimator : AgentAnimator
    {
        [SerializeField]
        Order m_Order;

        [SerializeField]
        Eat m_Eat;

        [SerializeField]
        Pay m_Pay;

        [SerializeField]
        Leave m_Leave;

        public Order Order => m_Order;

        void Start()
        {
            // Register State Commands
            StateMachine
                .AddCommand(Order.Name)
                .SetTargetState<Order>()
                .SetCondition(() => StateMachine.CurrentState is Walk && !AgentController.IsMoving);

            // StateMachine
            //     .AddCommand(Eat.Name)
            //     .SetTargetState<Eat>()
            //     .SetCondition(() => StateMachine.CurrentState is Walk && !AgentController.IsMoving);

            // StateMachine
            //     .AddCommand(Pay.Name)
            //     .SetTargetState<Pay>()
            //     .SetCondition(() => StateMachine.CurrentState is Walk && !AgentController.IsMoving);

            // StateMachine
            //     .AddCommand(Leave.Name)
            //     .SetTargetState<Leave>()
            //     .SetCondition(() => StateMachine.CurrentState is Walk && !AgentController.IsMoving);

            StateMachine.ExecuteCommand(Walk.Name); // Walk state as default
        }
    }
}
