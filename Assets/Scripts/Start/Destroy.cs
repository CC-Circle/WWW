using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // 同じ名前のオブジェクトがあれば削除
        GameObject[] obj = GameObject.FindGameObjectsWithTag(gameObject.tag);
        if (obj.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}