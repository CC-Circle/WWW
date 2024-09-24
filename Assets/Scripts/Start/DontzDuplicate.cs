using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontzDuplicate : MonoBehaviour
{
    void Awake()
    {
        // "DontDestroy"という名前のオブジェクトをすべて取得
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroy");

        // 複数存在する場合、最初のオブジェクトを除いてすべて削除
        if (objs.Length > 1)
        {
            for (int i = 1; i < objs.Length; i++)
            {
                Destroy(objs[i]);
            }
        }
    }
}
