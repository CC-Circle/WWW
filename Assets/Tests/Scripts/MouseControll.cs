using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControll : MonoBehaviour
{
    private Vector3 lastMousePosition;
    // Start is called before the first frame update
    void Start()
    {
        // 初期化時にマウスの位置を保存
        lastMousePosition = Input.mousePosition;

    }

    // Update is called once per frame
    void Update()
    {
        // マウスの移動量を取得
        float deltaX = Input.mousePosition.x - lastMousePosition.x;
        float deltaY = Input.mousePosition.y - lastMousePosition.y;

        // マウスの位置を保存
        Debug.Log("deltaX: " + deltaX + " deltaY: " + deltaY);


    }
}
