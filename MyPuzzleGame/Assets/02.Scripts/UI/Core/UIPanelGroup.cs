using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class UIPanelGroup : UIElementBase
    {
        //============================================================================================
        //field ~
        Dictionary<string, UIPanel> m_panelList = new Dictionary<string, UIPanel>();

        //============================================================================================
        //unity func~
        protected override void Awake()
        {
            base.Awake();

            UIPanel[] panelList = GetComponentsInChildren<UIPanel>();
            for(int i =0; i < panelList.Length; ++i)
            {
                panelList[i].myGroup = this;
                m_panelList.Add(panelList[i].gameObject.name, panelList[i]);
            }
        }

        //============================================================================================
        //my func~
        public T GetPanel<T>(string key) where T : UIPanel
        {
            UIPanel panel = m_panelList.GetValue(key);
            if (panel == null)
                return null;

            return panel as T;
        }
    }
}
