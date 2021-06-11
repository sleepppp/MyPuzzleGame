using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Data;

namespace Core.UI
{
    public class PuzzlePiece : UIElementBase
    {
        //============================================================================================
        //Fields~
        Image m_image;

        PieceInfo m_pieceInfo;
        //============================================================================================
        //Property ~
        public PieceInfo pieceInfo
        {
            get { return m_pieceInfo; }
            set
            {
                m_pieceInfo = value;

                if (m_image == null)
                    m_image = GetComponent<Image>();

                m_image.sprite = m_pieceInfo.Sprite;
            }
        }

        //============================================================================================
        protected override void Awake()
        {
            base.Awake();

            if(m_image == null)
                m_image = GetComponent<Image>();
        }
    }
}