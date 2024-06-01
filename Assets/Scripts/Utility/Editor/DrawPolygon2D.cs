#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MaidCafe.Utility
{
    internal static class DrawPolygon2D
    {
        internal static void Draw(Transform gameObj, bool showLine, Color lineColor)
        {
            if (!showLine)
                return;

            List<Vector2[]> pointsList = new List<Vector2[]>();
            PolygonCollider2D[] thisColliders = gameObj.GetComponents<PolygonCollider2D>();

            foreach (PolygonCollider2D c in thisColliders)
            {
                Vector2[] pointWithOffset = new Vector2[c.points.Length];

                for (int i = 0; i < c.points.Length; i++)
                    pointWithOffset[i] = c.points[i] + c.offset;

                pointsList.Add(pointWithOffset);
            }

            Gizmos.matrix = gameObj.localToWorldMatrix; // Apply scale and rotation transformation

            for (int j = 0; j < pointsList.Count; j++)
            {
                Vector2[] points = pointsList[j];
                Gizmos.color = lineColor;

                for (int i = 0; i < points.Length - 1; i++)
                    Gizmos.DrawLine(new Vector3(points[i].x, points[i].y), new Vector3(points[i + 1].x, points[i + 1].y));

                Gizmos.DrawLine(new Vector3(points[points.Length - 1].x, points[points.Length - 1].y), new Vector3(points[0].x, points[0].y));
            }
        }
    }
}
#endif
