using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using DG.Tweening;

namespace Core.UI
{
    public class PanelConsole : UIPanel
    {
        //============================================================================================
        //field ~
        List<RectTransform> m_nodeList = new List<RectTransform>();
        List<Text> m_textList = new List<Text>();
        Queue<Text> m_renderTextQueue = new Queue<Text>();

        RectTransform m_contentMaskTransform;

        GameObject m_consoleTextPrefab;

        //============================================================================================
        //unity func~
        protected override void Awake()
        {
            base.Awake();

            m_contentMaskTransform = transform.Find("ContentGroup/ContentMask") as RectTransform;

            Transform nodeGroupTransform = transform.Find("NodeGroup");
            for(int i =0; i < nodeGroupTransform.childCount;++i)
            {
                m_nodeList.Add(nodeGroupTransform.GetChild(i) as RectTransform);
            }

            Addressables.LoadAssetsAsync<GameObject>("ConsoleText",OnLoadConsoleTextPrefab);
        }

        //============================================================================================
        //my func~
        Text GetSleepText()
        {
            return m_textList.Find((Text text) => { return text.gameObject.activeInHierarchy == false; });
        }

        public void PushText(string text)
        {
            if(m_renderTextQueue.Count >= m_nodeList.Count - 2)
            {
                int index = 0;

                Text textCompont = GetSleepText();
                textCompont.gameObject.SetActive(true);
                textCompont.text = text;

                textCompont.transform.position = m_nodeList[m_nodeList.Count - 1].position;
                m_renderTextQueue.Enqueue(textCompont);

                foreach (Text item in m_renderTextQueue)
                {
                    RectTransform targetNode = m_nodeList[index];

                    item.transform.DOMove(targetNode.position, 1f);

                    ++index;
                }

                Text front = m_renderTextQueue.Peek();
                Core.Util.Timer.StartTimer(1f, () => { front.gameObject.SetActive(false); });

                m_renderTextQueue.Dequeue();
            }
            else
            {
                RectTransform targetNode = m_nodeList[m_renderTextQueue.Count + 1];
                Text textComponent = GetSleepText();
                textComponent.gameObject.SetActive(true);
                textComponent.transform.position = m_nodeList[m_nodeList.Count - 1].position;
                textComponent.transform.DOMove(targetNode.position, 1f);

                textComponent.text = text;

                m_renderTextQueue.Enqueue(textComponent);
            }
        }

        public void PopText()
        {
            int index = 0;
            foreach (Text item in m_renderTextQueue)
            {
                RectTransform targetNode = m_nodeList[index];
                item.transform.DOKill();
                //거리 = 속력 * 시간
                //시간 = 거리 / 속력
                item.transform.DOMove(targetNode.position, 
                    Vector3.Distance(item.transform.position, targetNode.transform.position) / 50f);
                ++index;
            }

            Text frontText = m_renderTextQueue.Peek();
            Core.Util.Timer.StartTimer(1f, () => { frontText.gameObject.SetActive(false); });
            m_renderTextQueue.Dequeue();
        }

        public void ClearText()
        {
            int removeCount = m_renderTextQueue.Count;
            for(int i =0; i < removeCount; ++i)
            {
                PopText();
            }
        }

        //============================================================================================
        //callback func~
        void OnLoadConsoleTextPrefab(GameObject prefab)
        {
            m_consoleTextPrefab = prefab;

            int capacity = m_nodeList.Count + 5;
            for(int i =0; i < capacity; ++i)
            {
                GameObject newObject = Instantiate(m_consoleTextPrefab, m_contentMaskTransform);
                newObject.SetActive(false);
                Text text = newObject.GetComponent<Text>();
                m_textList.Add(text);
            }
        }


    }
}
