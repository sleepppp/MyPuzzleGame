using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.UI
{
    public class PanelEffectText : UIElementBase
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
        //callback func~

        void OnLoadEffectText(GameObject prefab)
        {
            m_effectTextPrefab = prefab;

            for(int i =0; i < m_capacity; ++i)
            {
                GameObject newObject = Instantiate(m_effectTextPrefab, transform);
                EffectText effectText = newObject.GetComponent<EffectText>();

                m_sleepTextList.AddLast(effectText);
            }
        }

    }
}
