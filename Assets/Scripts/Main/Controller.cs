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
    new SphereCollider collider;
    public static bool isCollide = false;

    private float SEInterval = 3f;

    [SerializeField] private ParticleSystem particle;

    void Start()
    {
        // 初期化時にマウスの位置を保存
        lastMousePosition = Input.mousePosition;
        collider = GetComponent<SphereCollider>();
        collider.enabled = false;
        particle.Stop();
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
                    transform.RotateAround(Kusakariki.transform.position, Vector3.up, -0.5f);
                }
                else if (SerialReceive.Flag_view == 2)
                {
                    //RotateAround(中心の場所,回転軸,回転角度)
                    transform.RotateAround(Kusakariki.transform.position, Vector3.up, 0.5f);
                }

                if (Input.GetKey(KeyCode.Space) || SerialReceive.Flag_button == 1)
                {
                    collider.enabled = true;
                    transform.position += transform.forward * moveSpeed * 1000;
                    // y軸の高さの固定
                    transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
                    particle.Play();

                    if (Time.time > SEInterval)
                    {
                        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                        soundManager.PlaySound(1);
                        SEInterval = Time.time + 2f;
                    }

                }
                else
                {
                    // collider.enabled = false;
                    particle.Stop();
                }
            }
            //マウス
            else if (!SerialHandler.Settingsflag)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    collider.enabled = true;
                    transform.position += transform.forward * moveSpeed * 1000;
                    // y軸の高さの固定
                    transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
                    particle.Play();

                    if (Time.time > SEInterval)
                    {
                        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                        soundManager.PlaySound(1);
                        SEInterval = Time.time + 2f;
                    }

                }
                else
                {
                    // collider.enabled = false;
                    particle.Stop();
                }

                // 十字キーでの進行方向変更（回転）
                float h = Input.GetAxis("Horizontal"); // 左右キーの取得
                transform.Rotate(0, rotationSpeed * h * 0.1f, 0);

                // マウスのX方向の移動距離を計算
                Vector3 currentMousePosition = Input.mousePosition;
                float mouseDeltaX = currentMousePosition.x - lastMousePosition.x;

                Kusakariki.transform.Rotate(0, mouseDeltaX * rotationSpeed * 0.005f, 0);

                // 現在のマウス位置を次のフレーム用に保存
                lastMousePosition = currentMousePosition;
            }
        }
    }
}
