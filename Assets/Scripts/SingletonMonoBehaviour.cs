using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    [SerializeField] bool notDestroyOnLoad = true;
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            if (instance == null)
            {
                Debug.Log("Instance is Nothing!");
            }
            return instance;
        }

    }

    public void Awake()
    {

        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        if (notDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }

}