using System.Collections;
using System;
using UnityEngine;

namespace Core.Util
{
    public class Timer
    {
        public static void StartTimer(float duration, Action notify)
        {
            UpdateManager.instance.StartRoutine(RoutineTimrer(duration, notify));
        }

        public static void StartTimer<T>(float duration, T notifyParam, Action<T> notify)
        {
            UpdateManager.instance.StartRoutine(RoutineTimrer<T>(duration, notifyParam, notify));
        }

        static IEnumerator RoutineTimrer(float duration,Action notify)
        {
            float time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;

                yield return null;
            }

            notify?.Invoke();
        }

        static IEnumerator RoutineTimrer<T>(float duration, T notifyParam, Action<T> notify)
        {
            float time = 0f;
            while(time < duration)
            {
                time += Time.deltaTime;

                yield return null;
            }

            notify?.Invoke(notifyParam);
        }
    }
}