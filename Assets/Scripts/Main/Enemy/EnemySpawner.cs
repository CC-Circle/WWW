using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// 敵のプレハブ。Inspectorで設定する必要があります。
    /// </summary>
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private Transform parentObject;


    /// <summary>
    /// 敵の生成位置を格納するベクトル。初期値は(0, 0, 0)です。
    /// </summary>
    private Vector3 spawnPosition = new Vector3(0, 0, 0);

    /// <summary>
    /// 敵の生成間隔（秒単位）。Inspectorで設定する必要があります。デフォルトは5.0秒です。
    /// </summary>
    [SerializeField] private float spawnInterval = 5.0f;

    /// <summary>
    /// 敵が目的地に到達するまでの時間（秒単位）。Inspectorで設定する必要があります。デフォルトは3.0秒です。
    /// </summary>
    [SerializeField] private float arrivalTime = 3.0f;

    /// <summary>
    /// スクリプト開始時に呼び出される初期化メソッドです。
    /// </summary>
    void Start()
    {
        StartCoroutine(EnemySpowner());
    }

    void Update()
    {
        Timer TimerScript = GameObject.Find("UIManager").GetComponent<Timer>();
        if (TimerScript.countTime < 20)
        {
            spawnInterval = 3.0f;
        }
        else if (TimerScript.countTime < 10)
        {
            spawnInterval = 1.0f;
        }
    }

    /// <summary>
    /// 敵の生成位置を設定します。
    /// </summary>
    /// <param name="spawnPosition">新しい生成位置</param>
    private void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }

    /// <summary>
    /// 現在の敵の生成位置を取得します。
    /// </summary>
    /// <returns>現在の生成位置</returns>
    private Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }

    /// <summary>
    /// ランダムな生成位置を設定します。XとZの座標がほぼ同じにならないようにします。
    /// </summary>
    private void SetRomdomSpawnPosition()
    {
        float x = Random.Range(-500.0f, 500.0f);
        float z;
        do
        {
            z = Random.Range(-500.0f, 500.0f);
        } while (Mathf.Approximately(x, z));


        SetSpawnPosition(new Vector3(x, 0.5f, z));
    }

    /// <summary>
    /// 敵を生成し、指定した位置に移動させます。
    /// </summary>
    private void SpownEnemy()
    {
        SetRomdomSpawnPosition();
        if (enemyPrefab != null)
        {
            // enemyPrefabのうちランダムで選択
            GameObject enemyObj = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], GetSpawnPosition(), Quaternion.identity, parentObject);
            // GameObject enemyObj = Instantiate(enemyPrefab[0], GetSpawnPosition(), Quaternion.identity, parentObject);
            if (enemyObj.name == "Kamemushi(Clone)")
            {
                // y座標を3fに設定
                enemyObj.transform.position = new Vector3(enemyObj.transform.position.x, 18.0f, enemyObj.transform.position.z);
            }
            // cloneオブジェクト用のを設定
            enemyObj.tag = "CloneEnemy";
            // レイヤーの設定
            /* 参照下のEnemyのレイヤーはCenterに設定されている（クローンされたオブジェクトは参照下のオブジェクトに向かって移動するため）
            このレイヤーはメインカメラには見えない．（ミニマップおよびゲーム画面に表示されないように）
            クローンされたオブジェクトはメインやカメラやミニマップに表示されて欲しいので生成後にレイヤーを変更する．
            */
            int LayerIgnoreRaycast = LayerMask.NameToLayer("Enemy");
            enemyObj.layer = LayerIgnoreRaycast;

            Enemy enemyScript = enemyObj.GetComponent<Enemy>();
            if (enemyObj.name == "Kamemushi(Clone)")
            {
                enemyScript.StartMoveEnemy(new Vector3(0, 18.0f, 0), arrivalTime);
            }
            else
            {
                enemyScript.StartMoveEnemy(new Vector3(0, 0.5f, 0), arrivalTime);
            }
        }
        else
        {
            Debug.LogWarning("Enemy Prefab is not assigned in the inspector");
        }
    }


    /// <summary>
    /// 一定の間隔で敵を生成し続けるコルーチンです。
    /// </summary>
    /// <returns>IEnumerator コルーチンの実行に必要なオブジェクト</returns>
    public IEnumerator EnemySpowner()
    {
        while (true)
        {
            SpownEnemy();

            // 一定の間隔を待つ
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
