using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;

namespace Core
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [SerializeField]GameMode m_currentGameMode;

        public GameMode currentGameMode { get { return m_currentGameMode; } }
    }
}