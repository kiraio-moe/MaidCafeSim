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
        List<SplineContainer> m_OutSplines;

        public List<AgentController> AgentsPrefab => m_AgentsPrefab;

        public virtual void Spawn(AgentController agent, SplineContainer spline)
        {
            Vector3 initialKnotPosition = spline.Spline.ToArray()[0].Position;
            AgentController newAgent = Instantiate(agent, initialKnotPosition, Quaternion.identity);
            newAgent.GetComponent<SplineAnimate>().Container = spline;
            newAgent.OnEnd += () => Destroy(newAgent.gameObject);
        }

        public virtual void Update()
        {
        }
    }
}
