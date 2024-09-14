using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    public GameObject[] DontDestroyObjects;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}