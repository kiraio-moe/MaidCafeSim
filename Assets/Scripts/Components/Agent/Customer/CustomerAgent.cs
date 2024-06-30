using UnityEngine;

namespace MaidCafe.Components.Agent.Customer
{
    [AddComponentMenu("Maid Cafe/Components/Agent/Customer")]
    public class CustomerAgent : AgentController
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
    }
}
