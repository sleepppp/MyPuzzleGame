using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class UIElementBase : MonoBehaviour
    {
        //============================================================================================
        Canvas m_canvas;
        //============================================================================================
        public virtual void Start()
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