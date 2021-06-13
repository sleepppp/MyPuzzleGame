using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    }
}