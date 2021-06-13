using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class UIPanel : UIElementBase
    {
        protected UIPanelGroup m_myGroup;

        public UIPanelGroup myGroup { get { return m_myGroup; } set { m_myGroup = value; } }
    }
}
