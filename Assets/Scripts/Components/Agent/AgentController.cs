using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines;

namespace MaidCafe.Components.Agent
{
    public abstract class AgentController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        float m_MoveSpeed = 1f;

        protected delegate void StartAction();
        protected delegate void UpdateAction(float deltaTime);
        protected delegate void EndAction();

        /// <summary>
        /// Invoked events when the Agent start moving.
        /// </summary>
        protected event StartAction OnStart;

        /// <summary>
        /// Invoked events on every frame when the Agent is currently moving.
        /// </summary>
        protected event UpdateAction OnUpdate;

        /// <summary>
        /// Invoked events when the Agent stop moving.
        /// </summary>
        protected event EndAction OnEnd;

        SplineContainer splineContainer;
        Spline currentSpline;
        float normalizedTime;
        bool isMoving,
            isMovingReversed;

        // Vector3 direction,
        //     tangent;

        protected float MoveSpeed
        {
            get => m_MoveSpeed;
            set => m_MoveSpeed = value;
        }
        public SplineContainer SplineContainer
        {
            get => splineContainer;
            set => splineContainer = value;
        }
        public bool IsMoving
        {
            get => isMoving;
            set => isMoving = value;
        }
        public bool IsMovingReversed
        {
            get => isMovingReversed;
            set => isMovingReversed = value;
        }

        protected virtual void Start()
        {
            ChangeSpline(SplineContainer.Splines[0]); // set initial position
        }

        public void StartMove()
        {
            IsMoving = true;
            OnStart?.Invoke();
            OnUpdate += Move;
        }

        void Move(float deltaTime)
        {
            if (IsMoving)
            {
                // Calculate the delta for normalized time, considering the direction
                float deltaNormalizedTime = m_MoveSpeed * deltaTime / currentSpline.GetLength();
                normalizedTime += IsMovingReversed ? -deltaNormalizedTime : deltaNormalizedTime;

                // Clamp normalizedTime to ensure it stays within [0, 1] range
                normalizedTime = Mathf.Clamp01(normalizedTime);

                // Evaluate the current position on the spline
                Vector3 currentPosition = SplineContainer.EvaluatePosition(
                    currentSpline,
                    normalizedTime
                );

                // Update the position and rotation of the GameObject
                transform.SetLocalPositionAndRotation(currentPosition, Quaternion.identity);

                // Optional: Make the GameObject face along the spline direction
                // Vector3 nextPosition = SplineContainer.EvaluatePosition(normalizedTime + 0.05f);
                // Vector3 direction = nextPosition - currentPosition;
                // transform.rotation = Quaternion.LookRotation(direction, transform.up);
                // Vector3 tangent = SplineContainer.EvaluateTangent(normalizedTime);
                // transform.rotation = Quaternion.LookRotation(tangent);

                // Check if the movement has reached the end or the start of the spline
                if (
                    (IsMovingReversed && normalizedTime <= 0)
                    || (!IsMovingReversed && normalizedTime >= 1)
                )
                {
                    isMoving = false; // Stop at the end or start of the spline
                    Debug.Log("Invoke End event");
                    OnEnd?.Invoke();
                    OnUpdate -= Move;
                }
            }
        }

        void Update()
        {
            // Move(isMoving);
            OnUpdate?.Invoke(Time.deltaTime);
        }

        /// <summary>
        /// Change the Spline path to follow.
        /// </summary>
        /// <param name="spline"></param>
        protected void ChangeSpline(Spline spline)
        {
            currentSpline = spline;
        }

        /// <summary>
        /// Change the Spline Container.
        /// </summary>
        /// <param name="container"></param>
        protected void ChangeSplineContainer(SplineContainer container)
        {
            SplineContainer = container;
        }
    }
}
