using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class EffectText : UIElementBase
    {
        TextMesh m_textMesh;

        protected override void Awake()
        {
            base.Awake();

            m_textMesh = GetComponent<TextMesh>();
        }
    }
}
