using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class UpdateManager : MonoBehaviourSingleton<UpdateManager>
    {
        List<IEnumerator> m_updator = new List<IEnumerator>();

        private void Update()
        {
            for (int i = 0; i < m_updator.Count; ++i)
            {
                if (m_updator[i].MoveNext() == false)
                {
                    m_updator.RemoveAt(i);
                    --i;
                }
            }
        }

        public IEnumerator StartRoutine(IEnumerator routine)
        {
            m_updator.Add(routine);
            return routine;
        }

        public void RemoveRoutine(IEnumerator routine)
        {
            for (int i = 0; i < m_updator.Count; ++i)
            {
                if (m_updator[i] == routine)
                {
                    m_updator.RemoveAt(i);
                    break;
                }
            }
        }

        public bool IsValid(IEnumerator handler)
        {
            for (int i = 0; i < m_updator.Count; ++i)
            {
                if (m_updator[i] == handler)
                {
                    return true;
                }
            }
            return false;
        }
    }
}