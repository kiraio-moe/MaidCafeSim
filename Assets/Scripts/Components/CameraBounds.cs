//using WSWhitehouse.TagSelector;
using UnityEngine;
using Cinemachine;

namespace MaidCafe.Components
{
    [AddComponentMenu("Maid Cafe/Components/Camera Bounds")]
    [RequireComponent(typeof(CompositeCollider2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class CameraBounds : MonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField]
        //[TagSelector]
        string m_InteractWith = "Player";

        [Header("Editor")]
        [SerializeField] Color m_BoundsColor = Color.red;

        CompositeCollider2D _compositeCollider2D;
        Rigidbody2D _rigidbody2D;
        BoxCollider2D _boxCollider2D;
        CinemachineConfiner[] _cinemachineConfiners;

        void Awake()
        {
            GetComponents();
        }

        void Start()
        {
            Initialize();
            AddColliderToConfiners();
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            GetComponents();
            Initialize();
            AddColliderToConfiners();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = m_BoundsColor;
            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + _boxCollider2D.offset, _boxCollider2D.size);
        }
#endif

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(m_InteractWith))
            {
                if (_cinemachineConfiners.Length > 0)
                {
                    foreach (CinemachineConfiner confiner in _cinemachineConfiners)
                        confiner.m_BoundingShape2D = _compositeCollider2D;
                }
            }
        }

        void GetComponents()
        {
            if (_compositeCollider2D == null)
                _compositeCollider2D = GetComponent<CompositeCollider2D>();
            if (_rigidbody2D == null)
                _rigidbody2D = GetComponent<Rigidbody2D>();
            if (_boxCollider2D == null)
                _boxCollider2D = GetComponent<BoxCollider2D>();

            _cinemachineConfiners = FindObjectsOfType<CinemachineConfiner>();
        }

        void Initialize()
        {
            // Rigidbody 2D
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _rigidbody2D.simulated = true;

            // Composite Collider 2D
            _compositeCollider2D.isTrigger = true;
            _compositeCollider2D.geometryType = CompositeCollider2D.GeometryType.Polygons;

            // Box Collider 2D
            _boxCollider2D.usedByComposite = true;
        }

        void AddColliderToConfiners()
        {
            if (_cinemachineConfiners.Length > 0)
            {
                foreach (CinemachineConfiner confiner in _cinemachineConfiners)
                {
                    confiner.m_ConfineMode = CinemachineConfiner.Mode.Confine2D;
                    confiner.m_ConfineScreenEdges = true;
                }
            }
        }

        /// <summary>
        /// Match bounds height with screen height.
        /// </summary>
        public float MatchCameraHeight()
        {
            float cameraHeight = 2f * Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * Camera.main.aspect;

            float desiredHeight = cameraHeight;
            float currentHeight = _boxCollider2D.size.y;

            float scale = desiredHeight / currentHeight;

            _boxCollider2D.size *= scale;
            _boxCollider2D.offset *= scale;

            return scale;
        }
    }
}
