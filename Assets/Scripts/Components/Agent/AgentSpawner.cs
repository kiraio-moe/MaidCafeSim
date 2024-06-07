using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace MaidCafe.Components.Agent
{
    public abstract class AgentSpawner : MonoBehaviour
    {
        [Header("Sprites Variety")]
        [SerializeField]
        List<AgentController> m_AgentsPrefab;

        [Header("Splines List")]
        [SerializeField]
        List<SplineContainer> m_InSplines;
        
        [SerializeField]
        List<SplineContainer> m_OutSplines;
        
        [SerializeField, Range(-1, 5)]
        float m_SpawnDelay = 2f;
        
        float spawnTime;
        
        public virtual int Spawn(AgentController agent)
        {
            int inIndex = m_InSplines.Count == 1 ? 0 : Random.Range(0, m_InSplines.Count);
            AgentController newAgent = Instantiate(agent, m_InSplines[inIndex].transform.position, Quaternion.identity);
            newAgent.GetComponent<SplineAnimate>().Container = m_InSplines[inIndex];
//             newAgent.StartMove();
            newAgent.OnEnd += () => Destroy(newAgent.gameObject);
            return inIndex;
        }
        
        public virtual void Update()
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= m_SpawnDelay && m_SpawnDelay != -1)
            {
                int agentIndex = m_AgentsPrefab.Count == 1 ? 0 : Random.Range(0, m_AgentsPrefab.Count);
                Spawn(m_AgentsPrefab[agentIndex]);
                spawnTime = 0;
            }
        }
    }
}
