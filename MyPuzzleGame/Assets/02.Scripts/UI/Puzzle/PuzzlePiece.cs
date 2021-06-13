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

        //============================================================================================
        //my func~
        public void DestroyPiece()
        {
            Vector3 fxLocation = transform.position;
            fxLocation.z = m_canvas.planeDistance - 10f;
            GameObject particleObject = EffectManager.instance.PlayEffect("PuzzlePieceDestroyFX", fxLocation);
            if(particleObject != null)
            {
                //TODO 현재는 마땅한 이펙트가 없어서 해당 파티클로 처리하나 추후에 변경 요망
                ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
                ParticleSystemRenderer renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
                renderer.material.color = m_pieceInfo.FXColor;
            }
            m_node.piece = null;
            Destroy(gameObject);
        }

    }
}