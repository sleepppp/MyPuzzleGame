using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using DG.Tweening;
using Core.Data;

using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

//TODO 현재 매치 검사를 타이머를 줘서 체크하고 있으므로 개선 필요

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
        GameObject m_piecePrefab;
        PuzzlePiece m_selectPiece;

        float m_cachedCellDistance; //월드상에서 셀의 실제 간격을 저장해 놓는 변수

        int m_movePieceCount;
        //============================================================================================
        //UnityFunc ~
        void Start()
        {
            InitializeComponent();

            Addressables.LoadAssetsAsync<GameObject>("PuzzlePiece", InitPiece);
        }

        void Update()
        {
            if (IsAnyPieceTranslateNow() || 
                GameManager.instance.currentGameMode.IsPlaying() == false)
                return;

            // {{ 비어있는 노드를 찾습니다 ~
            List<PuzzlePiece> movedPieceList = null;
            for(int x =0;x < m_colCount; ++x)
            {
                int createCount = 0;
                for(int y = m_rowCount -1; y >= 0; --y)
                {
                    Indexer index = new Indexer(x, y);
                    PuzzleNode node = GetNode(index);
                    
                    if (node.piece != null)
                        continue;

                    //위에 있는 노드로부터 피스를 찾아옵니다
                    PuzzlePiece targetPiece = null;
                    for(int tempY = y -1; tempY >= 0; --tempY)
                    {
                        Indexer tempIndex = new Indexer(x, tempY);
                        PuzzleNode tempNode = GetNode(tempIndex);

                        if (tempNode.piece == null)
                            continue;

                        targetPiece = tempNode.piece;
                        break;
                    }
                    //끌어 내릴 피스가 있다면... 나하고 역어준다
                    if(targetPiece != null)
                    {
                        DisConnectNodeAndPiece(targetPiece.node, targetPiece);
                        ConnectNodeAndPiece(node, targetPiece);
                        MovePieceToNode(targetPiece, 1f).SetEase(Ease.OutBounce);

                        if (movedPieceList == null)
                            movedPieceList = new List<PuzzlePiece>();

                        movedPieceList.Add(targetPiece);
                    }
                    //없다면 피스를 새로 생성해준다
                    else
                    {
                        if (movedPieceList == null)
                            movedPieceList = new List<PuzzlePiece>();

                        float height = 
                            Mathf.Abs(m_nodeList[0, 0].transform.position.y - m_nodeList[1, 0].transform.position.y);

                        GameObject newObject = Instantiate(m_piecePrefab, m_nodeList[y, x].transform);
                        PuzzlePiece piece = newObject.GetComponent<PuzzlePiece>();
                        ConnectNodeAndPiece(m_nodeList[y, x], piece);
                        piece.transform.position = m_nodeList[0, x].transform.position + Vector3.up * height +
                            Vector3.up * height * createCount;

                        piece.pieceInfo = GameManager.instance.currentGameMode.soPiece.GetRandomPieceInfo();

                        MovePieceToNode(piece, 1f).SetEase(Ease.OutBounce);
                        movedPieceList.Add(piece);
                        ++createCount;
                    }
                }
            }
            // }}

            if(movedPieceList != null)
                Core.Util.Timer.StartTimer(1f, movedPieceList, OnEventEndMovePiece);
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
            m_piecePrefab = piecePrefab;

            SOPiece soPiece = GameManager.instance.currentGameMode.soPiece;
            // GridLayoutGroup은 프레임이 돌기 전까지는 하위 자식들의 좌표를 계산하지 않으므로 연산을 강제해주어야한다. 
            GridLayoutGroup layoutGroup = m_boardTransform.GetComponent<GridLayoutGroup>();
            layoutGroup.CalculateLayoutInputHorizontal();
            layoutGroup.CalculateLayoutInputVertical();
            layoutGroup.SetLayoutHorizontal();
            layoutGroup.SetLayoutVertical();

            float height = Mathf.Abs(m_nodeList[0, 0].transform.position.y - m_nodeList[1, 0].transform.position.y);
            List<PuzzlePiece> pieceList = new List<PuzzlePiece>();
            for (int y = 0; y < m_rowCount ; ++y)
            {
                for(int x = 0; x < m_colCount; ++x)
                {
                    GameObject newObject = Instantiate(piecePrefab, m_nodeList[y, x].transform);
                    PuzzlePiece piece = newObject.GetComponent<PuzzlePiece>();
                    ConnectNodeAndPiece(m_nodeList[y, x], piece);
                    piece.transform.position = m_nodeList[0, x].transform.position + Vector3.up * height + 
                        Vector3.up * height * (m_rowCount - y);

                    piece.pieceInfo = soPiece.GetRandomPieceInfo();

                    pieceList.Add(piece);
                }
            }

            //TODO 임시로 연출처리. 추후에 GameState 추가되면 처리 필요
            Core.Util.Timer.StartTimer(1f, pieceList, (List<PuzzlePiece> list) => 
            {
                for(int i =0; i < list.Count; ++i)
                {
                    MovePieceToNode(list[i], 1f).SetEase(Ease.OutBounce);
                }
            });

            m_cachedCellDistance = height;

            GameManager.instance.currentGameMode.Play();
        }

        //============================================================================================
        //Evnet Systems Func ~
        public void OnPointerDown(PointerEventData eventData)
        {
            if (CanDragPiece() == false)
                return;

            if (eventData.pointerEnter == null)
                return;

            PuzzlePiece piece = eventData.pointerEnter.GetComponent<PuzzlePiece>();
            if (piece == null)
                return;

            m_selectPiece = piece;

            Vector3 position = TransformScreenPointToWorldPoint(eventData.position, m_selectPiece.rectTrasnform);
            m_selectPiece.transform.position = position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (CanDragPiece() == false)
                return;

            if (m_selectPiece == null)
                return;

            Vector3 position = TransformScreenPointToWorldPoint(eventData.position, m_selectPiece.rectTrasnform);
  
            Vector3 dir = position -  m_selectPiece.node.transform.position;

            float distance = dir.magnitude;
            float maxDistance= m_cachedCellDistance * 0.5f;
            if(distance >= maxDistance)
            {
                position = m_selectPiece.node.transform.position + dir.normalized * maxDistance;                
            }

            m_selectPiece.transform.position = position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (m_selectPiece == null)
                return;

            if (CanDragPiece() == false)
            {
                m_selectPiece = null;
                return;
            }

            Vector3 dir = m_selectPiece.transform.position - m_selectPiece.node.transform.position;
            //dir.Normalize();

            //너무 적게 땡겼으면 원래로 돌아가야합니다
            if(dir.magnitude < m_cachedCellDistance * 0.2f)
            {
                MovePieceToNode(m_selectPiece,0.3f);
                m_selectPiece = null;
                return;
            }

            Indexer indexDir = Vector3DirToIndexDir(dir);
            indexDir.Normalize();

            if(CanMove(m_selectPiece,indexDir))
            {
                Indexer nextIndex = m_selectPiece.node.indexer + indexDir;
                PuzzlePiece nextPiece = GetNode(nextIndex).piece;

                SwapPieces(m_selectPiece, nextPiece);
            }
            //만약 땡긴 방향으로 움직일 수 없다면 원래 자리로 돌아갑니다.
            else
            {
                m_selectPiece.rectTrasnform.DOAnchorPos(Vector2.zero, 0.3f).OnComplete(OnEventEndMovePiece);
                ++m_movePieceCount;
            }

            m_selectPiece = null;
        }

        //============================================================================================
        //My Func~

        TweenerCore<Vector2, Vector2, VectorOptions> MovePieceToNode(PuzzlePiece piece,float duration)
        {
            ++m_movePieceCount;
            return piece.rectTrasnform.DOAnchorPos(Vector2.zero, duration).OnComplete(OnEventEndMovePiece);
        }

        //노드와 피스를 상호참조 시켜줍니다.
        public void ConnectNodeAndPiece(PuzzleNode node, PuzzlePiece piece)
        {
            node.piece = piece;
            piece.node = node;

            piece.transform.SetParent(node.transform, true);
        }

        public void DisConnectNodeAndPiece(PuzzleNode node,PuzzlePiece piece)
        {
            node.piece = null;
            piece.node = null;
            piece.transform.SetParent(transform, true);
        }

        //방향벡터를 인덱스로 변환합니다.
        Indexer Vector3DirToIndexDir(Vector3 dir)
        {
            Indexer result = new Indexer(dir);
            //인덱스는 아래로 증감이므로 뒤집어 준다
            result.y *= -1;
            return result;
        }
        //피스가 해당 방향으로 이동 가능한지 판별합니다
        bool CanMove(PuzzlePiece piece,Indexer dirIndex)
        {
            Indexer resultIndex = piece.node.indexer + dirIndex;
            return IsInBoundBoard(resultIndex) && m_nodeList[resultIndex.y,resultIndex.x].nodeType != NodeType.Empty;
        }
        //해당 인덱스가 보드 안에 포함되는지 판별합니다
        bool IsInBoundBoard(Indexer index)
        {
            if (index.x < 0 || index.x >= m_colCount) return false;
            if (index.y < 0 || index.y >= m_rowCount) return false;

            return true;
        }

        bool CanDragPiece()
        {
            return m_movePieceCount == 0;
        }

        bool IsAnyPieceTranslateNow()
        {
            return m_movePieceCount != 0;
        }

        void SwapPieces(PuzzlePiece pieceA, PuzzlePiece pieceB,bool checkMatch = true)
        {
            if (pieceA == null || pieceB == null)
                return;

            PuzzleNode nodeA = pieceA.node;
            PuzzleNode nodeB = pieceB.node;

            ConnectNodeAndPiece(nodeA, pieceB);
            ConnectNodeAndPiece(nodeB, pieceA);

            MovePieceToNode(pieceA, 0.3f);
            MovePieceToNode(pieceB, 0.3f);

            if(checkMatch)
            {
                List<PuzzlePiece> list = new List<PuzzlePiece>() { pieceA, pieceB };

                Core.Util.Timer.StartTimer(0.3f, list, OnEventEndSwap);
            }
        }

        PuzzleNode GetNode(Indexer index)
        {
            if (IsInBoundBoard(index) == false)
                return null;

            return m_nodeList[index.y, index.x];
        }

        PuzzlePiece GetPiece(Indexer index)
        {
            PuzzleNode node = GetNode(index);
            if (node == null)
                return null;
            return node.piece;
        }

        List<PuzzlePiece> CheckMatch(Indexer startIndex)
        {
            PieceType targetType = GetPiece(startIndex).pieceInfo.Type;

            List<PuzzlePiece> result = new List<PuzzlePiece>();

            Indexer[] arrDir = new Indexer[4];
            arrDir[0] = Indexer.left;
            arrDir[1] = Indexer.right;
            arrDir[2] = Indexer.top;
            arrDir[3] = Indexer.bottom;
            // {{ 4 방향 매치 검사~
            for(int i =0; i < arrDir.Length; ++i)
            {
                List<PuzzlePiece> line = new List<PuzzlePiece>();

                int count = arrDir[i].x == 0 ? m_rowCount : m_colCount;

                int matchCount = 0;
                for(int j = 1; j < count; ++j)
                {
                    Indexer nextIndex = startIndex + Indexer.Mul(arrDir[i], j);

                    if (IsInBoundBoard(nextIndex) == false)
                        break;

                    PuzzleNode nextNode = GetNode(nextIndex);
                    PuzzlePiece nextPiece = nextNode.piece;
                    if (nextPiece == null)
                        break;
                    if (nextPiece.pieceInfo.Type != targetType)
                        break;

                    line.Add(nextPiece);
                    ++matchCount;
                }

                if(matchCount >= 2)
                {
                    MergeMatchList(result, line);
                }
            }
            // }} 

            // {{ 가운데 4방향 검사
            for (int i = 0; i < 4; i += 2)
            {
                Indexer[] directions = new Indexer[2];
                directions[0] = arrDir[i];
                directions[1] = arrDir[i + 1];

                int matchCount = 0;
                List<PuzzlePiece> line = new List<PuzzlePiece>();
                for (int j = 0; j < directions.Length; ++j)
                {
                    Indexer index = Indexer.Add(startIndex, directions[j]);
                    PuzzleNode node = GetNode(index);
                    if (node != null && node.piece != null && node.piece.pieceInfo.Type == targetType)
                    {
                        ++matchCount;
                        line.Add(node.piece);
                    }
                }

                if (matchCount >= 2)
                {
                    MergeMatchList(result, line);
                }
            }
            // }} 

            //시작 노드 추가
            if(result.Count != 0)
            {
                PuzzlePiece startPiece = GetPiece(startIndex);
                if (result.Contains(startPiece) == false)
                    result.Add(startPiece);
            }

            return result;
        }

        void MergeMatchList(List<PuzzlePiece> target, List<PuzzlePiece> temp)
        {
            for(int i =0; i < temp.Count; ++i)
            {
                if (target.Contains(temp[i]))
                    continue;

                target.Add(temp[i]);
            }
        }

        PuzzleNode GetTopNode(int colIndex)
        {
            if (GetNode(new Indexer(colIndex, 0)).nodeType == NodeType.Fill)
                return GetNode(new Indexer(colIndex, 0));

            for(int y = 1; y < m_rowCount; ++y)
            {
                PuzzleNode node = GetNode(new Indexer(colIndex, y));
                if (node.nodeType == NodeType.Empty)
                {
                    PuzzleNode nextNode = GetNode(new Indexer(colIndex, y + 1));
                    if (nextNode.nodeType == NodeType.Fill)
                        return node;
                }
            }

            return null;
        }

        //============================================================================================
        //call by delegate~

        void OnEventEndMovePiece()
        {
            --m_movePieceCount;
        }

        void OnEventEndMovePiece(List<PuzzlePiece> movedPieceList)
        {
            List<PuzzlePiece> matchList = new List<PuzzlePiece>();

            for(int i =0; i < movedPieceList.Count; ++i)
            {
                List<PuzzlePiece> tempList = CheckMatch(movedPieceList[i].node.indexer);
                MergeMatchList(matchList, tempList);
            }

            if (matchList.Count != 0)
            {
                for (int i = 0; i < matchList.Count; ++i)
                {
                    matchList[i].DestroyPiece();
                }
            }
        }

        void OnEventEndSwap(List<PuzzlePiece> movedPieceList)
        {
            List<PuzzlePiece> matchList = new List<PuzzlePiece>();

            for(int i =0; i < movedPieceList.Count; ++i)
            {
                List<PuzzlePiece> tempList = CheckMatch(movedPieceList[i].node.indexer);
                MergeMatchList(matchList, tempList);
            }

            //만약 스왑되었는데 매치 된게 없다면 다시 돌아가야 한다
            if(matchList.Count == 0)
            {
                SwapPieces(movedPieceList[0], movedPieceList[1],false);
            }
            else
            {
                for(int i =0; i < matchList.Count; ++i)
                {
                    matchList[i].DestroyPiece();   
                }
            }
        }
    }
}
