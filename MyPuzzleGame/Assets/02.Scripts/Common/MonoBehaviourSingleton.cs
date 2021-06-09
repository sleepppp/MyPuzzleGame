using UnityEngine;

namespace Core
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;

        public static T instance
        {
            get
            {
                if(_instance == null)
                {
                    T[] arrInstance = FindObjectsOfType<T>();
                    if(arrInstance.Length > 1)
                    {
                        throw new System.Exception("To many singleton");
                    }
                    else if(arrInstance.Length == 1)
                    {
                        _instance = arrInstance[0];
                    }
                    else
                    {
                        GameObject newObject = new GameObject(typeof(T).Name);
                        _instance = newObject.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }
    }
}