using MaidCafe.Components.Agent.Customer.States;
using UnityEngine;

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
        public Eat Eat => m_Eat;
        public Pay Pay => m_Pay;
        public Leave Leave => m_Leave;

        protected override void Awake()
        {
            base.Awake();

            RegisterState(Order, Eat, Pay, Leave);
            BuildStateMachine();
            StateMachine.AddCommand(Order.Name).SetTargetState<Order>();
            StateMachine.AddCommand(Eat.Name).SetTargetState<Eat>();
            StateMachine.AddCommand(Pay.Name).SetTargetState<Pay>();
            StateMachine.AddCommand(Leave.Name).SetTargetState<Leave>();
        }
    }
}
