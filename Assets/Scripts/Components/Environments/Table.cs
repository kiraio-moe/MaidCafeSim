using UnityEngine;
using UnityEngine.Splines;

namespace MaidCafe.Components.Environments
{
    [AddComponentMenu("Maid Cafe/Components/Environments/Table")]
    public class Table : MonoBehaviour
    {
        bool isOccupied;
        // SplineContainer[] chairs;

        public bool IsOccupied { get; set; }
        // public SplineContainer[] Chairs => chairs;
    }
}
