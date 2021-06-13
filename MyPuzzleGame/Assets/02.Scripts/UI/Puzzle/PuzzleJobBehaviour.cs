using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Core.Data;

namespace Core.UI
{
    //============================================================================================
    //Job ~
    struct ShuffleJob : IJob
    {
        public NativeArray<PieceType> m_pieceTypeList;
        public NativeArray<PieceType> m_result;
        public int m_rowCount;
        public int m_colCount;

        public void Execute()
        {
            for(int i =0; i < m_result.Length; ++i)
            {
                Indexer index = GetIndexer(i);

            }
        }

        Indexer GetIndexer(int index)
        {
            Indexer result = new Indexer();
            result.y = index % m_colCount;
            result.x = index - result.y;
            return result;
        }
    }

    //============================================================================================
    //Monobehavour~
    public class PuzzleJobBehaviour : MonoBehaviour
    {
        //============================================================================================
        //field~
        ShuffleJob m_job;
        JobHandle m_halder;

        //============================================================================================
        //my func~
        public void StartJob(PuzzleNode[,] nodeList,int rowCount, int colCount,SOPiece soPiece)
        {
            m_job = new ShuffleJob();

            // {{ Init job data~
            m_job.m_rowCount = rowCount;
            m_job.m_colCount = colCount;
            m_job.m_pieceTypeList = new NativeArray<PieceType>(soPiece.pieceInfo.Length,Allocator.Persistent);

            for(int i =0; i < soPiece.pieceInfo.Length; ++i)
            {
                m_job.m_pieceTypeList[i] = soPiece.pieceInfo[i].Type;
            }

            m_job.m_result = new NativeArray<PieceType>(rowCount * colCount, Allocator.Persistent);

            for(int y=0; y < rowCount; ++y)
            {
                for(int x=0;x < colCount; ++x)
                {
                    int index = y * colCount + x;
                    m_job.m_result[index] = nodeList[y, x].piece.pieceInfo.Type;
                }
            }

            // }}
            m_halder = m_job.Schedule();

            StartCoroutine(CoroutineWaitJobComplete());
        }

        IEnumerator CoroutineWaitJobComplete()
        {
            while(m_halder.IsCompleted == false)
            {
                yield return null;
            }
            m_halder.Complete();
        }
    }
}
