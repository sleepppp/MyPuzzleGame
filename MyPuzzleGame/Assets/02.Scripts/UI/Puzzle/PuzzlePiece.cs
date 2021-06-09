using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class PuzzlePiece : UIElementBase
    {
        //============================================================================================
        Image m_image;
        //============================================================================================
        protected override void Awake()
        {
            base.Awake();

            m_image = GetComponent<Image>();
        }
    }
}