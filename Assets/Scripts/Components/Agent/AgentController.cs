using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

namespace MaidCafe.Components.Agent
{
	[RequireComponent(typeof(SplineAnimate))]
	public abstract class AgentController : MonoBehaviour
	{
		[Header("Movement")]
		[SerializeField]
		float m_MoveSpeed = 2f;
		
		[Header("Events")]
		[Tooltip("Invoke events at the start of the Spline.")]
		[SerializeField]
		UnityEvent m_OnStart;
		
		[Tooltip("Invoke events at every knot of the Spline.")]
		[SerializeField]
		UnityEvent m_OnUpdate;
		
		[Tooltip("Invoke events at the end of the Spline.")]
		[SerializeField]
		UnityEvent m_OnEnd;
		
		public float MoveSpeed
		{
			get => m_MoveSpeed;
			set => m_MoveSpeed = value;
		}
		
		public UnityEvent OnStart
		{
			get => m_OnStart;
			set => m_OnStart = value;
		}
		
		public UnityEvent OnUpdate
		{
			get => m_OnUpdate;
			set => m_OnUpdate = value;
		}
		
		public UnityEvent OnEnd
		{
			get => m_OnEnd;
			set => m_OnEnd = value;
		}
		
		SplineAnimate splineAnimate;
		
		void Reset()
		{
			splineAnimate = GetComponent<SplineAnimate>();
			splineAnimate.Alignment = SplineAnimate.AlignmentMode.SplineObject;
			splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
			splineAnimate.Easing = SplineAnimate.EasingMode.EaseOut;
			splineAnimate.Loop = SplineAnimate.LoopMode.Once;
			splineAnimate.MaxSpeed = m_MoveSpeed;
			splineAnimate.PlayOnAwake = false;
		}
		
		void Awake()
		{
			splineAnimate = GetComponent<SplineAnimate>();
		}
		
		void OnEnable()
		{
			if (splineAnimate.PlayOnAwake)
				m_OnStart?.Invoke();
			splineAnimate.Updated += OnSplineUpdate;
			splineAnimate.Completed += () => m_OnEnd?.Invoke();
		}
		
		public void StartMove()
		{
			m_OnStart?.Invoke();
			splineAnimate.Play();
		}
		
		public void StartMoveBackward()
		{
			m_OnStart?.Invoke();
			splineAnimate.StartOffset = 1;
			splineAnimate.Play();
		}
		
		public void ChangeSplineContainer(SplineContainer container)
		{
			splineAnimate.Container = container;
		}
		
		public float SetMoveSpeed(float speed)
		{
			return m_MoveSpeed = speed;
		}
		
		void OnSplineUpdate(Vector3 position, Quaternion rotation)
		{
			m_OnUpdate?.Invoke();
		}
	}
}
