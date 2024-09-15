// using UnityEngine;
// using TMPro;

// /// <summary>
// /// 指定された一時停止時間の後に、テキストを表示するスクリプト。
// /// TextMeshProを使用してUIテキストを更新します。
// /// </summary>
// public class DisplayTextAfterPause : MonoBehaviour
// {
//     // 経過時間を計測するための変数
//     private float elapsedPauseTime = 0;
//     // テキストを表示するまでの一時停止時間（秒）
//     [SerializeField] private float pauseDuration = 3;
//     // TextMeshProUGUIコンポーネント
//     private TextMeshProUGUI uiText;
//     // 次のシーン名、もしくは表示したいテキスト
//     [SerializeField] private string displayText;

//     /// <summary>
//     /// ゲーム開始時にUIテキストを空に設定します。
//     /// </summary>
//     void Start()
//     {
//         uiText = GetComponent<TextMeshProUGUI>();
//         uiText.text = "";
//     }

//     /// <summary>
//     /// 毎フレーム実行され、経過時間を計測します。
//     /// 一定時間経過後に、テキストを表示します。
//     /// </summary>
//     void Update()
//     {
//         elapsedPauseTime += Time.deltaTime; // 一時停止時間の計測
//         UpdateTextAfterPause(displayText, elapsedPauseTime);
//     }

//     /// <summary>
//     /// 指定した時間が経過したら、UIテキストに指定された文字列を表示します。
//     /// </summary>
//     /// <param name="text">表示するテキスト</param>
//     /// <param name="time">経過時間</param>
//     private void UpdateTextAfterPause(string text, float time)
//     {
//         if (time >= pauseDuration)
//         {
//             uiText.text = text;
//         }
//     }
// }
