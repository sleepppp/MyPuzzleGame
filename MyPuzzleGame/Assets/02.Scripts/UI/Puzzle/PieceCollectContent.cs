using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core.Data;
using DG.Tweening;

namespace Core.UI
{
    public class PieceCollectContent : UIElementBase
    {
        //============================================================================================
        //field ~
        TextMeshProUGUI m_textMesh;
        Image m_pieceImage;
        PieceInfo m_pieceInfo;

        int m_collectCount;
        int m_currentCollectCount;

        //============================================================================================
        //property ~
        public Image pieceImage { get { return m_pieceImage; } }

        //============================================================================================
        //my func~
        public void Init(PieceInfo pieceInfo)
        {
            m_pieceInfo = pieceInfo;

            m_pieceImage = GetComponentInChildren<Image>();
            m_textMesh = GetComponentInChildren<TextMeshProUGUI>();

            m_pieceImage.sprite = pieceInfo.Sprite;
            m_textMesh.text = "00";

            UpdateManager.instance.StartRoutine(RoutineUpdate());
        }

        public void AddCollect()
        {
            ++m_collectCount;
            m_collectCount = Mathf.Clamp(m_collectCount, 0, 99);
        }

        IEnumerator RoutineUpdate()
        {
            float timer = 0f;
            while(true)
            {
                if(m_collectCount > m_currentCollectCount)
                {
                    m_textMesh.color = Color.yellow;

                    timer += Time.deltaTime;
                    if(timer >= 0.1f)
                    {
                        ++m_currentCollectCount;

                        if (m_currentCollectCount < 10)
                            m_textMesh.text = "0" + m_currentCollectCount.ToString();
                        else
                            m_textMesh.text = m_currentCollectCount.ToString();

                        timer = 0f;
                    }
                }
                else
                {
                    m_textMesh.color = Color.white;
                }

                yield return null;
            }
        }
    }
}
