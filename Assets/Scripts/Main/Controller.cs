using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.001f;
    [SerializeField] private GameObject mainCamera;
    public SerialReceive serialReceive;
    public GameObject M5Stack;
    public float distanceFromCamera = 5f; // カメラからの距離
    private Vector3 lastMousePosition;

    [SerializeField] private GameObject Kusakariki;
    int angle = 90;
    new SphereCollider collider;
    public static bool isCollide = false;

    void Start()
    {
        // 初期化時にマウスの位置を保存
        lastMousePosition = Input.mousePosition;
        collider = GetComponent<SphereCollider>();
        collider.enabled = false;
    }

    void Update()
    {
        if (ShowStartText.flag)
        {
            // Flagを入手するためのコード
            SerialHandler SerialHandler; //呼ぶスクリプトにあだなつける
            GameObject M5Stack = GameObject.Find("M5stack_Event"); //Playerっていうオブジェクトを探す
            SerialHandler = M5Stack.GetComponent<SerialHandler>(); //付いているスクリプトを取得

            // ジャイロを入手するためのコード
            SerialReceive SerialReceive; //呼ぶスクリプトにあだなつける
            SerialReceive = M5Stack.GetComponent<SerialReceive>(); //付いているスクリプトを取得

            float rotationSpeed = 10f; // 回転速度

            // M5Stack
            if (SerialHandler.Settingsflag)
            {
                if (SerialReceive.Flag_view == 1)
                {
                    //RotateAround(中心の場所,回転軸,回転角度)
                    transform.RotateAround(Kusakariki.transform.position, Vector3.up, -angle);
                    SerialReceive.Flag_view = 3;
                }
                else if (SerialReceive.Flag_view == 2)
                {
                    //RotateAround(中心の場所,回転軸,回転角度)
                    transform.RotateAround(Kusakariki.transform.position, Vector3.up, angle);
                    SerialReceive.Flag_view = 3;
                }
            }
            //マウス
            else if (!SerialHandler.Settingsflag)
            {
                // コライダーの有効無効を切り替える
                if (Input.GetKey(KeyCode.Space))
                {
                    collider.enabled = true;
                }
                else
                {
                    collider.enabled = false;
                }

                // 十字キーでの進行方向変更（回転）
                float h = Input.GetAxis("Horizontal"); // 左右キーの取得
                transform.Rotate(0, rotationSpeed * h * 0.1f, 0);

                // マウスのX方向の移動距離を計算
                Vector3 currentMousePosition = Input.mousePosition;
                float mouseDeltaX = currentMousePosition.x - lastMousePosition.x;

                Kusakariki.transform.Rotate(0, mouseDeltaX * rotationSpeed * 0.005f, 0);

                // マウスの移動距離に応じて前進
                if (Mathf.Abs(mouseDeltaX) > 0)
                {
                    transform.position += transform.forward * Mathf.Abs(mouseDeltaX) * moveSpeed * 5;
                    // 高さは固定
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                }

                // 現在のマウス位置を次のフレーム用に保存
                lastMousePosition = currentMousePosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CloneEnemy") == true && other.gameObject.CompareTag("Enemy") == false)
        {
            isCollide = true;
        }
    }
}
