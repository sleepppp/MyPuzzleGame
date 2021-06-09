
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace Core.UI
{
    public class PuzzleNode : UIElementBase
    {
        //============================================================================================
        //Fields~
        Image m_image;
        NodeType m_nodeType;

        PuzzlePiece m_piece;
        //============================================================================================
        //Property~
        public NodeType getNodeType { get { return m_nodeType; } }
        public PuzzlePiece getPiece { get { return m_piece; } }
        //============================================================================================
        //Unity Func ~
        protected override void Awake()
        {
            base.Awake();

            m_image = GetComponent<Image>();

            m_nodeType = m_image.enabled ? NodeType.Fill : NodeType.Empty;
        }
    }
}