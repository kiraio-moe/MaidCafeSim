using UnityEngine;
using UnityEngine.Splines;

namespace MaidCafe.Components.Environments
{
    [AddComponentMenu("Maid Cafe/Components/Environments/Chair")]
    [RequireComponent(typeof(SplineContainer))]
    public class Chair : MonoBehaviour
    {
        SplineContainer container;

        public SplineContainer Container
        {
            get => container;
            set => container = value;
        }

        void OnValidate()
        {
            if (Container == null)
                Container = GetComponent<SplineContainer>();
        }
    }
}
