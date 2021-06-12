using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Data;
using DG.Tweening;

namespace Core.UI
{
    public class PuzzlePiece : UIElementBase
    {
        //============================================================================================
        //Fields~
        Image m_image;
        PuzzleNode m_node;
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

        public PuzzleNode node { get { return m_node; } set { m_node = value; } }

        //============================================================================================
        //unity Func~
        protected override void Awake()
        {
            base.Awake();

            if(m_image == null)
                m_image = GetComponent<Image>();
        }

    }
}