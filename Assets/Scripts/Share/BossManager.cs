using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    [SerializeField] private float delayTime = 5f; // デフォルトの遅延時間を設定
    private string currentSceneName;
    private float elapsedTime = 0f;

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (currentSceneName == "Boss")
        {
            ManageBossScripts();
        }
    }

    private void ManageBossScripts()
    {
        BossAppear bossAppear = FindObjectOfType<BossAppear>();
        if (bossAppear == null)
        {
            Debug.LogError("ScriptsManagerにBossAppearがアタッチされていません");
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime < delayTime)
        {
            bossAppear.BeforeBossFight();
            SetBossScriptsEnabled(false);
        }
        else
        {
            bossAppear.AfterBossFight();
            SetBossScriptsEnabled(true);
        }
    }

    private void SetBossScriptsEnabled(bool isEnabled)
    {
        // Boss関連のスクリプトをシーン内から取得し、無効/有効にする
        MonoBehaviour[] bossScripts = FindObjectsOfType<MonoBehaviour>();

        foreach (var script in bossScripts)
        {
            if (script.CompareTag("BossScript")) // スクリプトにタグを設定している場合
            {
                script.enabled = isEnabled;
            }
        }
    }
}
