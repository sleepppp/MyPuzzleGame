
using UnityEngine;
using UnityEngine.UI;
using Core;
using Core.Data;

namespace Core.UI
{
    public class PuzzleNode : UIElementBase
    {
        //============================================================================================
        //Fields~
        Image m_image;
        NodeType m_nodeType;
        Indexer m_indexer;
        PuzzlePiece m_piece;
        //============================================================================================
        //Property~
        public NodeType nodeType { get { return m_nodeType; } }
        public PuzzlePiece piece { get { return m_piece; } set { m_piece = value; } }
        public Indexer indexer { get { return m_indexer; } }

        //============================================================================================
        //public Func~
        public void Init(Indexer indexer)
        {
            m_image = GetComponent<Image>();
            m_nodeType = m_image.enabled ? NodeType.Fill : NodeType.Empty;

            m_indexer = indexer;
        }
    }
}