using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public float shakeDuration = 0.5f;      // 揺れる時間
    public float shakeMagnitude = 0.1f;      // 固定の揺れの強さ

    private Vector3 initialPosition;         // 元のカメラの位置

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // カメラを揺らす関数
    public void TriggerShake(float delayBeforeShake)
    {
        StartCoroutine(Shakes(delayBeforeShake));
    }

    private IEnumerator Shakes(float delayBeforeShake)
    {
        // 揺れる前に遅延を入れる
        yield return new WaitForSeconds(delayBeforeShake);
        // オーディオソースを再生
        audioSource.Play();

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            // 固定の揺れの強さを使用
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            elapsed += Time.deltaTime;

            yield return null;
        }

        // 揺れが終了したらカメラの位置を元に戻す
        transform.localPosition = initialPosition;
    }
}
