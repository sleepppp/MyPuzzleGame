using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Core.Data;
using Core.UI;

namespace Core.Editor
{
    public class SOPieceEditor
    {
        //============================================================================================
        //static Func ~
        [MenuItem("Assets/Create/Puzzle/PieceInfo")]
        static void CreateSOPiece()
        {
            if (AssetDatabase.IsValidFolder("Assets/Data") == false)
            {
                AssetDatabase.CreateFolder("Assets", "Data");
            }

            SOPiece so = ScriptableObject.CreateInstance<SOPiece>();
            AssetDatabase.CreateAsset(so, "Assets/Data/PieceInfo.asset");
        }
    }
}