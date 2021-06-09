using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public class PanelBoard : UIElementBase,IPointerDownHandler,IDragHandler, IPointerUpHandler
    {
        //============================================================================================
        [SerializeField] int m_rowCount;
        [SerializeField] int m_colCount;
        //============================================================================================
        RectTransform m_boardTransform;
        PuzzleNode[,] m_nodeList;
        //============================================================================================
        //UnityFunc ~
        void Start()
        {
            InitializeComponent();
            CreatePiece();
        }

        //============================================================================================
        //Init ~
        void InitializeComponent()
        {
            m_boardTransform = transform.Find("Board") as RectTransform;

            m_nodeList = new PuzzleNode[m_rowCount, m_colCount];
            int index = 0;
            for (int y = 0; y < m_rowCount; ++y)
            {
                for (int x = 0; x < m_colCount; ++x)
                {
                    m_nodeList[y, x] = m_boardTransform.GetChild(index).GetComponent<PuzzleNode>();

                    ++index;
                }
            }
        }

        void CreatePiece()
        {
            
        }

        //============================================================================================
        //Evnet Systems Func ~
        public void OnPointerDown(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
        //============================================================================================
    }
}
