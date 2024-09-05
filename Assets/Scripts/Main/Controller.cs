using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.001f;
    [SerializeField] private GameObject mainCamera;
    public SerialReceive serialReceive;
    private int leftRightCount = 0;
    private bool isSwinging = false; // 振り回し動作のフラグ
    private float swingDirection = 0; // 現在の振り回し方向
    private float swingAmount = 30f; // 振り回しの角度
    private float swingSpeed = 50f; // 振り回しの速度
    private float swingTime = 0; // 振り回しの経過時間
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
        bool isRotating = false; // 視点操作が実行されているかどうかを判定するフラグ

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
            float moveAmount = 1f * Time.deltaTime;

            // 現在の位置を取得
            Vector3 currentPosition = transform.position;


            // M5Stack
            if (SerialHandler.Settingsflag)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    //RotateAround(中心の場所,回転軸,回転角度)
                    transform.RotateAround(Kusakariki.transform.position, Vector3.up, angle);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    //RotateAround(中心の場所,回転軸,回転角度)å
                    transform.RotateAround(Kusakariki.transform.position, Vector3.up, -angle);
                }
                // 視点操作が実行されていない場合のみ振り回し操作を行う
                // if (!isRotating)
                // {
                //     if (serialReceive.Flag == 1 || serialReceive.Flag == 2)
                //     {
                //         // 振り回し動作を開始する
                //         if (!isSwinging)
                //         {
                //             isSwinging = true;
                //             swingDirection = serialReceive.Flag == 1 ? -1 : 1; // 振り回しの方向を設定
                //             swingTime = 0; // 経過時間のリセット
                //         }

                //         // 振り回し動作を実行する
                //         swingTime += Time.deltaTime * swingSpeed;
                //         float angle = Mathf.Sin(swingTime) * swingAmount; // 振り回しの角度を計算
                //         transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + angle * swingDirection, 0);

                //         // 振り回しが終了したらフラグをリセット
                //         if (swingTime > Mathf.PI * 2) // 振り回しが一周したら終了
                //         {
                //             isSwinging = false;
                //             swingTime = 0;
                //             leftRightCount++;
                //         }
                //     }
                // }

                // // 左右に一回ずつ振ったら前進
                // if (leftRightCount >= 2)
                // {
                //     transform.position += transform.forward * 10; // プレイヤーの向いている方向に前進
                //     leftRightCount = 0; // カウントをリセット
                // }
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
