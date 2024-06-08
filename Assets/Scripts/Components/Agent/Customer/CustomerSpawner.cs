using System.Collections;
using MaidCafe.Components.Environments;
using UnityEngine;
using UnityEngine.Splines;

namespace MaidCafe.Components.Agent.Customer
{
    [AddComponentMenu("Maid Cafe/Components/Agent/Customer Spawner")]
    public class CustomerSpawner : AgentSpawner
    {
        Table[] tables;
        private float delay = 2f;
        private bool canSpawn = true;

        void Start()
        {
            tables = FindObjectsByType<Table>(FindObjectsSortMode.None);
        }

        public override void Update()
        {
            if (canSpawn)
                StartCoroutine(TableCheck(delay));
        }

        IEnumerator TableCheck(float delay)
        {
            canSpawn = false;

            foreach (Table table in tables)
            {
                if (!table.IsOccupied) //Table empty, then spawn
                {
                    int groupCount = Random.Range(4, 4);
                    for (int i = 0; i < groupCount; i++)
                    {
                        SplineContainer chair = table
                            .transform.GetChild(i)
                            .GetComponent<SplineContainer>();
                        int agentIndex =
                            AgentsPrefab.Count == 1 ? 0 : Random.Range(0, AgentsPrefab.Count); // Spawn random agent variety
                        Spawn(AgentsPrefab[agentIndex], chair);
                        //Set the spline to the chair
                    }
                    table.IsOccupied = true;
                    break;
                }
            }
            yield return new WaitForSeconds(delay);
            canSpawn = true;
        }
    }
}
