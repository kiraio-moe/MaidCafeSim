using UnityEngine;
using UnityEngine.Splines;

namespace MaidCafe.Components.Environments
{
    [AddComponentMenu("Maid Cafe/Components/Environments/Chair")]
    [RequireComponent(typeof(SpriteRenderer), typeof(SplineContainer))]
    public class Chair : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        int[] m_StopCheckpointsIndex;

        SplineContainer container;

        public SplineContainer Container
        {
            get => container;
            set => container = value;
        }

        void Awake()
        {
            Container = GetComponent<SplineContainer>();
        }
    }
}
