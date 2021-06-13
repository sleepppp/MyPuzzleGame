using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public class JobTest : MonoBehaviour
{
    struct TestJob : IJob
    {
        public NativeArray<int> m_arr;

        public void Execute()
        {
            for (int i =0; i < 10000; ++i)
            {
                m_arr[i] = i;
            }
        }
    }

    JobHandle m_handle;
    TestJob m_job;

    private void Start()
    {
        m_job = new TestJob();
        m_job.m_arr = new NativeArray<int>(10000, Allocator.Persistent);
        for(int i =0; i < m_job.m_arr.Length; ++i)
        {
            m_job.m_arr[i] = 0;
        }
        m_handle = m_job.Schedule();

        StartCoroutine(Routine());
    }

    //============================================================================================
    IEnumerator Routine()
    {
        while(m_handle.IsCompleted == false)
        {
            yield return null;
        }
        m_handle.Complete();

        Debug.Log("Frame : " + Time.frameCount);

        for(int i =0; i < m_job.m_arr.Length; ++i)
        {
            Debug.Log(m_job.m_arr[i]);
        }
    }
}
