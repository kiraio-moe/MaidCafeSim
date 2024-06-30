using UnityEngine;
using UnityEngine.Splines;
using Zelude;

namespace MaidCafe.Components.Agent
{
    public abstract class AgentSpawner : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        AgentController[] m_AgentsPrefab;

        [SerializeField, MinMaxSlider(1, 4)]
        Vector2 m_RandomizeAgentIndex = new(1, 4);

        public AgentController[] AgentsPrefab => m_AgentsPrefab;
        public Vector2 RandomizeAgentIndex => m_RandomizeAgentIndex;

        void OnValidate()
        {
            m_RandomizeAgentIndex = new(
                Mathf.RoundToInt(m_RandomizeAgentIndex.x),
                Mathf.RoundToInt(m_RandomizeAgentIndex.y)
            );
        }

        /// <summary>
        /// Spawn agent to follow a Spline path.
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="spline"></param>
        /// <returns>The instantiated Agent.</returns>
        public virtual AgentController Spawn(AgentController agent, SplineContainer spline)
        {
            Vector3 initialKnotPosition = spline.Spline.ToArray()[0].Position;
            AgentController newAgent = Instantiate(agent, initialKnotPosition, Quaternion.identity);
            // newAgent.GetComponent<SplineAnimate>().Container = spline;
            newAgent.SplineContainer = spline;
            return newAgent;
        }

        public virtual void Update() { }
    }
}
