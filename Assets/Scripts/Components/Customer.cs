using UnityEngine;
using MaidCafe.Components.Agent;

namespace MaidCafe.Components
{
    [AddComponentMenu("Maid Cafe/Components/Customer")]
	public class Customer : AgentController
	{
		void Reset()
		{
// 			base.Reset();
			GameObject agentAnimator = new GameObject("Sprite", typeof(AgentAnimator));
			agentAnimator.transform.SetParent(transform);
		}
	}
}
