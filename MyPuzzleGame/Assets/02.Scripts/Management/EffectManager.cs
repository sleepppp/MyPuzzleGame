using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;
using UnityEngine.AddressableAssets;

namespace Core
{
    public class EffectManager : MonoBehaviourSingleton<EffectManager>
    {
        //============================================================================================
        //fieds ~
        Dictionary<string, List<GameObject>> m_effectList = new Dictionary<string, List<GameObject>>();
        Dictionary<string, EffectData> m_effectData = new Dictionary<string, EffectData>();
        //============================================================================================  
        //unity func~
        private void Awake()
        {
            Dictionary<int, EffectData> effectData = DataManager.instance.gameData.EffectData;

            foreach(KeyValuePair<int,EffectData> item in effectData)
            {
                m_effectData.Add(item.Value.PrefabKey, item.Value);
                Addressables.LoadAssetsAsync<GameObject>(item.Value.PrefabKey, OnLoadEffectPrefab);
            }
        }

        //============================================================================================
        //callback func~
        void OnLoadEffectPrefab(GameObject prefab)
        {
            EffectData data = m_effectData.GetValue(prefab.name);

            List<GameObject> effectContainer = new List<GameObject>(data.Capacity);
            m_effectList.Add(prefab.name, effectContainer);

            for(int i =0; i < data.Capacity; ++i)
            {
                GameObject newObject = Instantiate(prefab, transform);
                newObject.SetActive(false);
                effectContainer.Add(newObject);
            }
        }

        //============================================================================================
        //my Func~
        public GameObject PlayEffect(string key,Vector3 location)
        {
            List<GameObject> effectList = m_effectList.GetValue(key);
            if (effectList == null)
                throw new System.Exception("There's no effect : " + key);

            for(int i =0; i < effectList.Count; ++i)
            {
                if(effectList[i].activeInHierarchy == false)
                {
                    effectList[i].SetActive(true);
                    effectList[i].transform.position = location;
                    return effectList[i];
                }
            }

            Debug.Log("EffectManager : all particle is already active : " + key);

            return null;
        }
    }
}