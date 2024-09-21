using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンの遷移を管理するスクリプト.
/// </summary>
public class MySceneManager : MonoBehaviour
{
    /// <summary>
    /// 現在のシーン名.
    /// </summary>
    private string currentSceneName;

    /// <summary>
    /// シーン遷移をトリガーするフラグ.
    /// </summary>
    public static bool flag;

    /// <summary>
    /// 毎フレーム呼び出され、シーンの遷移処理を実行する.
    /// </summary>
    void Update()
    {
        // 現在のシーン名を取得
        currentSceneName = SceneManager.GetActiveScene().name;

        // キーボード入力によるシーン遷移を処理
        HandleKeyboardInput();

        // シーン遷移の条件をチェック
        HandleSceneTransition();
    }

    /// <summary>
    /// シーン遷移の条件をチェックし、必要に応じてシーンをロードする.
    /// </summary>
    private void HandleSceneTransition()
    {
        // フラグが設定されていない場合は何もしない
        if (!flag) return;

        // 現在のシーンに応じて次のシーンをロード
        switch (currentSceneName)
        {
            case "Start":
                SceneManager.LoadScene("Desc");
                break;
            case "Desc":
                SceneManager.LoadScene("Main");
                break;
            case "Main":
                SceneManager.LoadScene("Boss");
                break;
            case "Boss":
                SceneManager.LoadScene("End");
                break;
            case "End":
                SceneManager.LoadScene("Start");
                break;
        }

        // フラグをリセット
        flag = false;
    }

    /// <summary>
    /// キーボード入力に基づいてシーンを遷移する.
    /// </summary>
    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Desc");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Main");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("End");
        }
    }
}
