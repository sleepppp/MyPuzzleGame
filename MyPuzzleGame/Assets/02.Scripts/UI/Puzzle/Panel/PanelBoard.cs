using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;

using Core.Data;

namespace Core.UI
{
    public class PanelBoard : UIElementBase,IPointerDownHandler,IDragHandler, IPointerUpHandler
    {
        //============================================================================================
        //Fields
        [SerializeField] int m_rowCount;
        [SerializeField] int m_colCount;

        RectTransform m_boardTransform;
        PuzzleNode[,] m_nodeList;
        //============================================================================================
        //UnityFunc ~
        void Start()
        {
            InitializeComponent();

            Addressables.LoadAssetsAsync<GameObject>("PuzzlePiece", InitPiece);
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
                    m_nodeList[y, x].Init(new Indexer(x,y));
                    ++index;
                }
            }
        }

        void InitPiece(GameObject piecePrefab)
        {
            SOPiece soPiece = GameManager.instance.currentGameMode.soPiece;

            GridLayoutGroup layoutGroup = m_boardTransform.GetComponent<GridLayoutGroup>();
            Vector2 cellSize = layoutGroup.cellSize;
            Vector2 startPosition = Vector2.up * cellSize.y * m_rowCount;

            for (int y = 0; y < m_rowCount ; ++y)
            {
                for(int x = 0; x < m_colCount; ++x)
                {
                    GameObject newObject = Instantiate(piecePrefab, m_nodeList[y, x].transform);
                    (newObject.transform as RectTransform).anchoredPosition = Vector3.zero;
                    (newObject.transform as RectTransform).anchoredPosition +=
                        Vector2.up * cellSize.y * (m_rowCount - y) + startPosition;

                    newObject.GetComponent<PuzzlePiece>().pieceInfo = soPiece.GetRandomPieceInfo();
                }
            }
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
    }
}
