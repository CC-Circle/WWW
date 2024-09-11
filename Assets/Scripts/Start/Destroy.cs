using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 同じタグを持つオブジェクトが存在する場合に、重複しているオブジェクトを削除するクラス.
/// </summary>
public class Destroy : MonoBehaviour
{
    /// <summary>
    /// オブジェクトが初めてシーンにロードされる際に呼び出されるメソッド.
    /// 重複するタグを持つオブジェクトが存在する場合、そのオブジェクトを削除します.
    /// </summary>
    void Awake()
    {
        // 同じタグを持つすべてのオブジェクトを取得
        GameObject[] obj = GameObject.FindGameObjectsWithTag(gameObject.tag);

        // 取得したオブジェクトの数が1を超えている場合（重複している場合）、このオブジェクトを削除
        if (obj.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
