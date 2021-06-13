using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Core.UI
{
    public class EffectText : UIElementBase
    {
        public event System.Action<EffectText> EventEndTexting;

        //============================================================================================
        //field
        TextMeshProUGUI m_textMesh;

        //============================================================================================
        //property
        public TextMeshProUGUI textMesh { get { return m_textMesh; } }

        //============================================================================================
        //unity func~
        protected override void Awake()
        {
            base.Awake();

            m_textMesh = GetComponent<TextMeshProUGUI>();
        }

        //============================================================================================
        //my func~
        public void StartTexting(string text, Vector3 location , Color color)
        {
            transform.position = location;
            m_textMesh.text = text;
            m_textMesh.color = color;
            transform.DOMoveY(location.y + 5f, 1f).OnComplete(() => EventEndTexting?.Invoke(this));
        }
    }
}
