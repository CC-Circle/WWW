using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScriptsManager : MonoBehaviour
{
    [SerializeField] private float delayTime = 0;
    private string currentSceneName;
    private float time = 0;

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

    }

    // Update is called once per frame
    void Update()
    {
        BossSceneScriptsManager();
    }

    void BossSceneScriptsManager()
    {
        BossAppear bossAppearScripts = GameObject.FindObjectOfType<BossAppear>();
        if (bossAppearScripts == null)
        {
            Debug.LogError("ScriptsManagerにBossAppearがアタッチされていません");
        }

        if (currentSceneName == "Boss")
        {
            delayTime = 5;
            time += Time.deltaTime;
            Debug.Log(time);

            if (time < delayTime)
            {
                bossAppearScripts.BeforeBossFight();

                var bossScripts = AssetDatabase.FindAssets("t:Script", new[] { "Assets/Scripts/Boss/Main" });
                foreach (var guid in bossScripts)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                    var monoBehaviour = GameObject.FindObjectOfType(script.GetClass()) as MonoBehaviour;
                    if (monoBehaviour != null)
                    {
                        monoBehaviour.enabled = false;
                    }
                }
            }
            else
            {
                bossAppearScripts.AfterBossFight();

                var bossScripts = AssetDatabase.FindAssets("t:Script", new[] { "Assets/Scripts/Boss/Main" });
                foreach (var guid in bossScripts)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                    var monoBehaviour = GameObject.FindObjectOfType(script.GetClass()) as MonoBehaviour;
                    if (monoBehaviour != null)
                    {
                        monoBehaviour.enabled = true;
                    }
                }
            }
        }
    }
}

