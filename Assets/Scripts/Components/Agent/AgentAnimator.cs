using UnityEngine;

namespace MaidCafe.Components.Agent
{
	[AddComponentMenu("Maid Cafe/Components/Agent Animator")]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(SpriteRenderer))]
	public class AgentAnimator : MonoBehaviour
	{
		[Header("Animator Parameters")]
		[SerializeField]
		string m_IdleParameter = "OnIdle";
		
		[SerializeField]
		string m_WalkParameter = "OnWalk";
		
		[SerializeField]
		string m_EatParameter = "OnEat";
		
		Animator animator;
		
		void Awake()
		{
			animator = GetComponent<Animator>();
		}
	}
}
