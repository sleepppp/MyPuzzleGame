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
        public void OnMatchPiece()
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

            //TODO 현재 UI 매니지먼트가 없어서 우선 이렇게 처리 개선 필요
            PanelPieceCollect panelPieceCollect = GameObject.Find("PanelPieceCollect").GetComponent<PanelPieceCollect>();
            PieceCollectContent content = panelPieceCollect.GetContent(m_pieceInfo.Type);

            transform.SetParent(panelPieceCollect.transform, true);

            if (content != null)
            {
                Vector3 targetLocation = content.pieceImage.transform.position;

                transform.DOMove(targetLocation, 0.5f).OnComplete(OnEndMoveToCollecter);
                transform.DOScale(content.pieceImage.transform.localScale,0.4f);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        //============================================================================================
        //call back func~
        void OnEndMoveToCollecter()
        {
            PanelPieceCollect panelPieceCollect = GameObject.Find("PanelPieceCollect").GetComponent<PanelPieceCollect>();
            panelPieceCollect.AddCollect(m_pieceInfo.Type);

            Destroy(gameObject);
        }
    }
}