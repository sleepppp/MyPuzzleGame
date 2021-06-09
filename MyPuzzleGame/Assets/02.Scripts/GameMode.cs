using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Core.Data;

namespace Core
{
    public class GameMode : MonoBehaviour
    {
        [SerializeField] PanelBoard m_board;
        [SerializeField] SOPiece m_soPiece;

        public PanelBoard board
        {
            get
            {
                if (m_board == null)
                    m_board = FindObjectOfType<PanelBoard>();

                return m_board;
            }
        }

        public SOPiece soPiece { get { return m_soPiece; } }
    }
}