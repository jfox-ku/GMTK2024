using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        
        private static T _instance;

        public static T Instance
        {
            get =>
                _instance;
            private set =>
                _instance = value;
        }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            
            Instance = this as T;
        }
    }
}