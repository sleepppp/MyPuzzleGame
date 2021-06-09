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
    }

    //============================================================================================
    // SOPiece ~
    public class SOPiece : ScriptableObject
    {
        //============================================================================================
        //static Func ~
        [MenuItem("Assets/Create/Puzzle/PieceInfo")]
        static void CreateSOPiece()
        {
            if(AssetDatabase.IsValidFolder("Assets/Data") == false)
            {
                AssetDatabase.CreateFolder("Assets", "Data");
            }

            SOPiece so = CreateInstance<SOPiece>();
            AssetDatabase.CreateAsset(so, "Assets/Data/PieceInfo.asset");
        }

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