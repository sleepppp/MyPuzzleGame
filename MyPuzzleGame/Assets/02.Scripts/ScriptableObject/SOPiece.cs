using System.Collections.Generic;
using UnityEngine;

namespace Core.Data
{
    //============================================================================================
    [System.Serializable]
    public struct PieceInfo
    {
        public Sprite Sprite;
        public PieceType Type;
        public Color FXColor;
    }

    //============================================================================================
    // SOPiece ~
    // SO : ScriptableObject
    public class SOPiece : ScriptableObject
    {
        //============================================================================================
        //Field ~ 
        [SerializeField] PieceInfo[] m_pieceInfo;

        //============================================================================================
        //Property ~
        public PieceInfo[] pieceInfo { get { return m_pieceInfo; } }

        //============================================================================================
        //public Func ~
        public PieceInfo GetRandomPieceInfo()
        {
            return pieceInfo[Random.Range(0, pieceInfo.Length)];
        }

        public PieceInfo GetRandomPieceInfo(List<PieceType> ignoreList)
        {
            List<PieceInfo> list = new List<PieceInfo>();

            for (int i = 0; i < pieceInfo.Length; ++i)
            {
                bool bContinue = false;
                for(int j =0; j < ignoreList.Count; ++j)
                {
                    if(pieceInfo[i].Type == ignoreList[j])
                    {
                        bContinue = true;
                        break;
                    }
                }

                if (bContinue)
                    continue;

                list.Add(pieceInfo[i]);
            }

            return list[Random.Range(0, list.Count)];
        }
    }
}