using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public abstract class UIElementBase : MonoBehaviour
    {
        //============================================================================================
        //Fields ~ 
        protected Canvas m_canvas;
        protected RectTransform m_rectTransform;

        //============================================================================================
        public RectTransform rectTrasnform
        {
            get
            {
                if (m_rectTransform == null)
                    m_rectTransform = transform as RectTransform;
                return m_rectTransform;
            }
        }
        //============================================================================================
        //Unity Func~
        protected virtual void Awake()
        {
            m_canvas = GetComponentInParent<Canvas>();
        }
        //============================================================================================
        protected Vector3 TransformScreenPointToWorldPoint(Vector2 screenPoint,RectTransform targetTransform)
        {
            Vector3 result;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(targetTransform,
                screenPoint, m_canvas.worldCamera, out result);
            return result;
        }
    }
}