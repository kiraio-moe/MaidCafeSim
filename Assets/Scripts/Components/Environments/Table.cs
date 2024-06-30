using UnityEngine;

namespace MaidCafe.Components.Environments
{
    [AddComponentMenu("Maid Cafe/Components/Environments/Table")]
    public class Table : MonoBehaviour
    {
        bool isOccupied;

        public bool IsOccupied
        {
            get => isOccupied;
            set => isOccupied = value;
        }

        void Reset()
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject chair = new($"Chair_{i}");
                chair.transform.SetParent(transform, false);
                chair.AddComponent(typeof(Chair));
            }
        }
    }
}
