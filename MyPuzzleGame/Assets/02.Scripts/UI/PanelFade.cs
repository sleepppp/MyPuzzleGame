using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class PanelFade : UIPanel
    {
        Image m_fadeImage;

        protected override void Awake()
        {
            base.Awake();

            m_fadeImage = GetComponent<Image>();
            m_fadeImage.enabled = false;
            m_fadeImage.color = new Color(0f, 0f, 0f, 1f);
        }

        void FadeIn(float time)
        {
            m_fadeImage.enabled = true;
            StartCoroutine(CoroutineFade(time, 0f));
        }

        void FadeOut(float time)
        {
            StartCoroutine(CoroutineFade(time, 1f));
        }

        IEnumerator CoroutineFade(float time, float targetValue)
        {
            float currentTime = 0f;
            float alpha = m_fadeImage.color.a;
            Color color = m_fadeImage.color;

            bool isIn = targetValue > 0.1f ? false : true;

            while (currentTime < time)
            {
                currentTime += Time.deltaTime;
                alpha = isIn ? (1f - currentTime / time) : (currentTime / time);

                color = m_fadeImage.color;
                color.a = alpha;
                m_fadeImage.color = color;

                yield return null;
            }

            color.a = targetValue;
            m_fadeImage.color = color;
        }
    }
}
