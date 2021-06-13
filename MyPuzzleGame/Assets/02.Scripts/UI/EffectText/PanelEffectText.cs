using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.UI
{
    public class PanelEffectText : UIPanel
    {
        //============================================================================================
        //field~
        [SerializeField] int m_capacity;
        LinkedList<EffectText> m_sleepTextList = new LinkedList<EffectText>();
        LinkedList<EffectText> m_activeTextList = new LinkedList<EffectText>();
        GameObject m_effectTextPrefab;

        //============================================================================================
        //unity func~
        protected override void Awake()
        {
            base.Awake();

            Addressables.LoadAssetsAsync<GameObject>("EffectText", OnLoadEffectText);
        }

        //============================================================================================
        //my func~
        public void PlayEffectText(string text,Vector3 location,Color color)
        {
            EffectText effectText = PopSleepEffectText();
            effectText.gameObject.SetActive(true);
            effectText.StartTexting(text, location, color);
            m_activeTextList.AddLast(effectText);
        }

        EffectText PopSleepEffectText()
        {
            LinkedListNode<EffectText> effectText =  m_sleepTextList.Last;
            m_sleepTextList.RemoveLast();

            return effectText.Value;
        }

        //============================================================================================
        //callback func~

        void OnLoadEffectText(GameObject prefab)
        {
            m_effectTextPrefab = prefab;

            for(int i =0; i < m_capacity; ++i)
            {
                GameObject newObject = Instantiate(m_effectTextPrefab, transform);
                EffectText effectText = newObject.GetComponent<EffectText>();
                effectText.EventEndTexting += OnEventEndTexting;
                effectText.gameObject.SetActive(false);
                m_sleepTextList.AddLast(effectText);
            }
        }

        void OnEventEndTexting(EffectText effectText)
        {
            effectText.gameObject.SetActive(false);

            m_activeTextList.Remove(effectText);
            m_sleepTextList.AddLast(effectText);
        }

    }
}
