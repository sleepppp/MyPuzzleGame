
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class PuzzleNode : UIElementBase
    {
        Image m_image;
        NodeType m_nodeType;

        public NodeType getNodeType { get { return m_nodeType; } }

        public override void Start()
        {
            base.Start();

            m_image = GetComponent<Image>();

            m_nodeType = m_image.enabled ? NodeType.Fill : NodeType.Empty;
        }
    }
}