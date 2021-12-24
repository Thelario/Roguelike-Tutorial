using UnityEngine;

namespace Game
{
    namespace Managers
    {
        public class Singleton<T> : MonoBehaviour where T : Component
        {
            [SerializeField] private bool _dontDestroyOnLoad;

            private static T _instance;
            public static T I
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                        if (_instance == null)
                        {
                            GameObject obj = new GameObject();
                            obj.name = typeof(T).Name;
                            _instance = obj.AddComponent<T>();
                        }
                    }

                    return _instance;
                }
            }

            protected virtual void Awake()
            {
                if (_instance == null)
                {
                    _instance = this as T;

                    if (_dontDestroyOnLoad)
                        DontDestroyOnLoad(gameObject);
                }
                else if (_instance != this as T)
                {
                    Destroy(gameObject);
                }
                else
                {
                    if (_dontDestroyOnLoad)
                        DontDestroyOnLoad(gameObject);
                }
            }
        }
    }
}
