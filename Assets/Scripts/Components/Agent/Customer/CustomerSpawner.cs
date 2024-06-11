using System.Collections;
using MaidCafe.Components.Environments;
using UnityEngine;

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
                    int groupCount = Mathf.RoundToInt(
                        Random.Range(RandomizeAgentIndex.x, RandomizeAgentIndex.y)
                    );

                    for (int i = 0; i < groupCount; i++)
                    {
                        Chair chair = table.transform.GetChild(i).GetComponent<Chair>();
                        int agentIndex = Random.Range(0, AgentsPrefab.Length); // Spawn random agent variety
                        Spawn(AgentsPrefab[agentIndex], chair.Container);
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
