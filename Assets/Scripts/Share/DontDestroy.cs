using UnityEngine;

public class SingletonSample : MonoBehaviour
{

    public static SingletonSample instance;
    public GameObject[] DontDestroyObjects;

    void Awake()
    {
        CheckInstance();
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}