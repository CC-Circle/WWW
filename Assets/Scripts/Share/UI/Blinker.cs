using UnityEngine;
using TMPro;

public class Blinker : MonoBehaviour
{
    [SerializeField] private float blinkSpeed = 1.0f; // 点滅の速度
    [SerializeField] private TextMeshProUGUI textMeshPro; // 点滅させるテキスト
    private float timeElapsed; // 経過時間

    private void Start()
    {
        // テキストコンポーネントを取得
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshProUGUI component not found!");
            }
        }
    }

    private void Update()
    {
        // テキストの色のアルファ値を更新
        textMeshPro.color = GetBlinkingColorAlpha(textMeshPro.color);
    }

    /// <summary>
    /// 点滅する色のアルファ値を計算します。
    /// </summary>
    /// <param name="color">元の色</param>
    /// <returns>点滅した色</returns>
    private Color GetBlinkingColorAlpha(Color color)
    {
        timeElapsed += Time.deltaTime * blinkSpeed; // 経過時間を更新
        color.a = Mathf.Abs(Mathf.Sin(timeElapsed)); // アルファ値をサイン関数で変化させる
        return color;
    }
}
