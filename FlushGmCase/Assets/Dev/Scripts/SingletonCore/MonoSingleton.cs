using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    /// <summary>
    /// Singleton objeler icin GENERIC SINGLETON DESIGN PATTERN
    /// </summary>
    private static volatile T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                if(!FindObjectOfType(typeof(T)))
                {
                    instance = new GameObject($"Singleton{typeof(T)}").AddComponent<T>();
                }
                else
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                }
            }
            return instance;
        }
    }
}
