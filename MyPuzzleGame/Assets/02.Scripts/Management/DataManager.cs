using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;

namespace Core
{
    public class DataManager : MonoBehaviourSingleton<DataManager>
    {
        GameData m_gameData;

        public GameData gameData
        {
            get
            {
                if (m_gameData == null)
                    m_gameData = new GameData();

                return m_gameData;
            }
        }
    }
}
