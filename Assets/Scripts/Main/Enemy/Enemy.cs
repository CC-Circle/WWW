using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// スクリプトが開始された時に呼び出される初期化メソッドです。
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// 毎フレーム呼び出される更新メソッドです。
    /// </summary>
    void Update()
    {

    }

    public void StartMoveEnemy(Vector3 targetPosition, float time)
    {
        StartCoroutine(MoveEnemy(targetPosition, time));
    }

    /// <summary>
    /// 敵を目標位置まで移動させるコルーチンです。
    /// </summary>
    /// <param name="targetPosition">移動先の目標位置</param>
    /// <param name="time">目標位置に到達するまでの時間（秒単位）</param>
    /// <returns>IEnumerator コルーチンの実行に必要なオブジェクト</returns>
    private IEnumerator MoveEnemy(Vector3 targetPosition, float time)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            // 移動位置を計算し設定
            transform.position = Vector3.MoveTowards(startPosition, targetPosition, (elapsedTime / time) * Vector3.Distance(startPosition, targetPosition));
            // 常に(0,0,0)を向く
            transform.LookAt(Vector3.zero);

            // 経過時間を更新
            elapsedTime += Time.deltaTime;

            // 目標位置に到達したか確認
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition; // 目標位置に正確に設定
                yield break; // コルーチンを終了
            }

            yield return null;
        }

        // 最終的に目標位置に到達
        transform.position = targetPosition;
    }
}
