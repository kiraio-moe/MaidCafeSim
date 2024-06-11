using UnityEngine;
using UnityEngine.Splines;

namespace MaidCafe.Components.Agent
{
    public abstract class AgentController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        float m_MoveSpeed = 2f;

        public delegate void StartAction();
        public delegate void UpdateAction(float deltaTime);
        public delegate void EndAction();

        /// <summary>
        /// Invoke events when Agent start moving.
        /// </summary>
        public event StartAction OnStart;

        /// <summary>
        /// Invoke events on every frame when Agent is currently moving.
        /// </summary>
        public event UpdateAction OnUpdate;

        /// <summary>
        /// Invoke events when Agent stop moving.
        /// </summary>
        public event EndAction OnEnd;

        SplineContainer splineContainer;
        Spline currentSpline;
        float normalizedTime;
        bool isMoving;
        bool isReversed;
        Vector3 direction,
            tangent;

        public float MoveSpeed
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

        public void StartMove(bool reverse = false)
        {
            IsMoving = true;
            // isReversed = reverse;
            currentSpline = SplineContainer.Splines[0];

            OnStart?.Invoke();
            OnUpdate += Move;
        }

        void Move(float deltaTime)
        {
            if (isMoving)
            {
                // Move the GameObject along the spline
                normalizedTime += Mathf.Clamp01(
                    m_MoveSpeed * deltaTime / currentSpline.GetLength() * (isReversed ? -1 : 1)
                );
                Vector3 currentPosition = SplineContainer.EvaluatePosition(
                    currentSpline,
                    normalizedTime
                );

                transform.SetLocalPositionAndRotation(currentPosition, Quaternion.identity);
                // Optional: Make the GameObject face along the spline direction
                Vector3 nextPosition = SplineContainer.EvaluatePosition(normalizedTime + 0.05f);
                direction = nextPosition - currentPosition;
                // transform.rotation = Quaternion.LookRotation(direction, transform.up);
                tangent = SplineContainer.EvaluateTangent(normalizedTime);
                // transform.rotation = Quaternion.LookRotation(tangent);

                if (normalizedTime >= 1)
                {
                    isMoving = false; // Stop at the end of the spline
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
        /// Change the Spline to follow.
        /// </summary>
        /// <param name="spline"></param>
        public void ChangeSpline(Spline spline)
        {
            currentSpline = spline;
        }

        /// <summary>
        /// Change the Spline Container.
        /// </summary>
        /// <param name="container"></param>
        public void ChangeSplineContainer(SplineContainer container)
        {
            SplineContainer = container;
        }
    }
}
