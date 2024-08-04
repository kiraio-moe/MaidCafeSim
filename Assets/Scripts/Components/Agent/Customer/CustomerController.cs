using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MaidCafe.Components.Agent.Customer
{
    [AddComponentMenu("Maid Cafe/Components/Agent/Customer Controller")]
    public class CustomerController : AgentController
    {
        CustomerAnimator customerAnimator;

        public CustomerAnimator CustomerAnimator
        {
            get => customerAnimator;
            set => customerAnimator = value;
        }

        void Reset()
        {
            if (CustomerAnimator != null)
                return;
            CustomerAnimator = new GameObject(
                "Sprite",
                typeof(CustomerAnimator)
            ).GetComponent<CustomerAnimator>();
            CustomerAnimator.transform.SetParent(transform);
        }

        void Awake()
        {
            if (CustomerAnimator == null)
                CustomerAnimator = GetComponentInChildren<CustomerAnimator>();
        }

        void OnEnable()
        {
            OnEnd += CustomerActivity;
        }

        protected override void Start()
        {
            base.Start();
            CustomerAnimator.StateMachine.ExecuteCommand(CustomerAnimator.Walk.Name);
        }

        async void CustomerActivity()
        {
            // just a test
            await UniTask.WaitForSeconds(Random.Range(1, 3));
            CustomerAnimator.StateMachine.ExecuteCommand(CustomerAnimator.Order.Name);
            await UniTask.WaitForSeconds(2);
            CustomerAnimator.StateMachine.ExecuteCommand(CustomerAnimator.Eat.Name);
            await UniTask.WaitForSeconds(2);
            CustomerAnimator.StateMachine.ExecuteCommand(CustomerAnimator.Pay.Name);
            await UniTask.WaitForSeconds(2);
            CustomerAnimator.StateMachine.ExecuteCommand(CustomerAnimator.Leave.Name);

            // IsMovingReversed = true;
            // CustomerAnimator.StateMachine.ExecuteCommand(CustomerAnimator.Walk.Name);
            // await UniTask.WaitUntil(() => !IsMoving);
            OnEnd -= CustomerActivity;
        }
    }
}
