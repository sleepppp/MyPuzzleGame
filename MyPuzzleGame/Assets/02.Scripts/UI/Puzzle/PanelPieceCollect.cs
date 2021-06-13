using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;

namespace Core.UI
{
    public class PanelPieceCollect : UIPanel
    {
        //============================================================================================
        //field ~
        Dictionary<PieceType, PieceCollectContent> m_contentList = new Dictionary<PieceType, PieceCollectContent>();

        //============================================================================================
        //unity func~
        protected override void Awake()
        {
            base.Awake();

            SOPiece soPiece = GameManager.instance.currentGameMode.soPiece;

            PieceCollectContent[] contents = GetComponentsInChildren<PieceCollectContent>();

            int contentIndex = 0;
            for (int i = 0; i < soPiece.pieceInfo.Length; ++i)
            {
                if (soPiece.pieceInfo[i].Type == PieceType.Coin) continue;

                contents[contentIndex].Init(soPiece.pieceInfo[i]);  

                m_contentList.Add(soPiece.pieceInfo[i].Type, contents[contentIndex]);
                ++contentIndex;
            }
        }

        //============================================================================================
        //my func~
        public PieceCollectContent GetContent(PieceType type)
        {
            return m_contentList.GetValue(type);
        }

        public void AddCollect(PieceType type)
        {
            PieceCollectContent content = GetContent(type);
            if(content)
                content.AddCollect();
        }

        //============================================================================================
        //callback func~

    }


}