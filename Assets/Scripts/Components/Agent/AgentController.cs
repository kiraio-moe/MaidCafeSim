// using UnityEngine;
// using UnityEngine.Events;
// using UnityEngine.Splines;

// namespace MaidCafe.Components.Agent
// {
// 	[RequireComponent(typeof(SplineAnimate))]
// 	public abstract class AgentController : MonoBehaviour
// 	{
// 		[Header("Movement")]
// 		[SerializeField]
// 		float m_MoveSpeed = 2f;
// 		
// 		[Header("Events")]
// 		[Tooltip("Events executed when the Agent start moving.")]
// 		[SerializeField]
// 		UnityEvent m_OnStart;
// 		
// 		[Tooltip("Events executed everytime the Agent passed the knot (point) of the Spline.")]
// 		[SerializeField]
// 		UnityEvent m_OnUpdate;
// 		
// 		[Tooltip("Events executed when the Agent finished moving along the Spline.")]
// 		[SerializeField]
// 		UnityEvent m_OnEnd;
// 		
// 		SplineAnimate splineAnimate;
// 		
// 		public float MoveSpeed
// 		{
// 			get => m_MoveSpeed;
// 			set => m_MoveSpeed = value;
// 		}
// 		
// 		public UnityEvent OnStart
// 		{
// 			get => m_OnStart;
// 			set => m_OnStart = value;
// 		}
// 		
// 		public UnityEvent OnUpdate
// 		{
// 			get => m_OnUpdate;
// 			set => m_OnUpdate = value;
// 		}
// 		
// 		public UnityEvent OnEnd
// 		{
// 			get => m_OnEnd;
// 			set => m_OnEnd = value;
// 		}
// 		
// 		public SplineAnimate SplineAnimate { get; private set; }
// 		
// 		void Reset()
// 		{
// 			splineAnimate = GetComponent<SplineAnimate>();
// 			splineAnimate.Alignment = SplineAnimate.AlignmentMode.SplineObject;
// 			splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
// 			splineAnimate.Easing = SplineAnimate.EasingMode.EaseOut;
// 			splineAnimate.Loop = SplineAnimate.LoopMode.Once;
// 			splineAnimate.MaxSpeed = m_MoveSpeed;
// 			splineAnimate.PlayOnAwake = false;
// 		}
// 		
// 		void Awake()
// 		{
// 			splineAnimate = GetComponent<SplineAnimate>();
// 			splineAnimate.PlayOnAwake = false;
// 		}
// 		
// 		void OnEnable()
// 		{
// 			splineAnimate.Updated += OnSplineUpdate;
// 			splineAnimate.Completed += () => m_OnEnd?.Invoke();
// 		}
// 		
// 		public void StartMove(bool reverse = false)
// 		{
// 			splineAnimate.StartOffset = reverse ? 1 : 0;
// 			m_OnStart?.Invoke();
// 			splineAnimate.Play();
// 		}
// 		
// 		public void ChangeSplineContainer(SplineContainer container)
// 		{
// 			splineAnimate.Container = container;
// 		}
// 		
// 		public float SetMoveSpeed(float speed)
// 		{
// 			return m_MoveSpeed = speed;
// 		}
// 		
// 		void OnSplineUpdate(Vector3 position, Quaternion rotation)
// 		{
// 			m_OnUpdate?.Invoke();
// 		}
// 	}
// }

using UnityEngine;
using UnityEngine.Splines;

namespace MaidCafe.Components.Agent
{
	[RequireComponent(typeof(SplineAnimate))]
	public abstract class AgentController : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField]
		float m_MoveSpeed = 2f;

		public delegate void StartAction();
		public delegate void UpdateAction(Vector3 position, Quaternion rotation);
		public delegate void EndAction();

		public event StartAction OnStart;
		public event UpdateAction OnUpdate;
		public event EndAction OnEnd;

		SplineAnimate splineAnimate;

		public float MoveSpeed
		{
			get => m_MoveSpeed;
			set => m_MoveSpeed = value;
		}

		public SplineAnimate SplineAnimate
		{
			get => splineAnimate;
		}

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
			splineAnimate.PlayOnAwake = false;
		}

		void OnEnable()
		{
			splineAnimate.Updated += OnSplineUpdate;
			splineAnimate.Completed += OnCompleted;
		}

		void OnDisable()
		{
			splineAnimate.Updated -= OnSplineUpdate;
			splineAnimate.Completed -= OnCompleted;
		}

		public void StartMove(bool reverse = false)
		{
			splineAnimate.StartOffset = reverse ? 1 : 0;
			OnStart?.Invoke();
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
			OnUpdate?.Invoke(position, rotation);
		}

		private void OnCompleted()
		{
			OnEnd?.Invoke();
		}
	}
}
