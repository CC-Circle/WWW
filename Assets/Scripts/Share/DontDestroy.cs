using UnityEngine;

public class SingletonSample : MonoBehaviour
{

    public GameObject[] DontDestroyObjects;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}