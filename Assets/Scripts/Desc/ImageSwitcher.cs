using UnityEngine;

/// <summary>
/// 画像の切り替えを制御するスクリプト。
/// 指定された2つの画像オブジェクトのうち、開始時は最初の画像を表示し、
/// 一定時間（指定した時間）が経過すると2つ目の画像を表示します。
/// </summary>
public class ImageSwitcher : MonoBehaviour
{
    // 画像オブジェクトを格納する配列
    [SerializeField] private GameObject[] imageObjects;
    // 画像を切り替える時間（秒）
    [SerializeField] private float switchDelay = 3f;
    // 経過時間を計測するための変数
    private float elapsedTime = 0;

    /// <summary>
    /// ゲーム開始時に最初の画像を表示し、2つ目の画像を非表示にします。
    /// </summary>
    void Start()
    {
        imageObjects[0].SetActive(true);
        imageObjects[1].SetActive(false);
    }

    /// <summary>
    /// 毎フレーム実行され、時間の経過を計測します。
    /// 指定した時間（switchDelay秒）が経過すると、最初の画像を非表示にして2つ目の画像を表示します。
    /// </summary>
    void Update()
    {
        elapsedTime += Time.deltaTime;

        PerformImageSwitch(elapsedTime);
    }

    /// <summary>
    /// 指定した時間が経過したら、画像を切り替えます。
    /// </summary>
    /// <param name="time">経過時間</param>
    private void PerformImageSwitch(float time)
    {
        if (time >= switchDelay)
        {
            imageObjects[0].SetActive(false);
            imageObjects[1].SetActive(true);
        }
    }
}
