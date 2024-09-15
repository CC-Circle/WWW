using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// HatakeのHPバーとダメージ処理を管理するスクリプト。
/// HPの変化に応じてバーのアニメーションを制御し、ダメージを受けた際の処理を行います。
/// </summary>
public class HatakeHP : MonoBehaviour
{
    [SerializeField] private Image healthBarImage; // HPバーの画像
    [SerializeField] private Image burnEffectImage; // 焼き焦げ効果の画像

    [SerializeField] private float animationDuration = 0.5f; // HPバーアニメーションの持続時間
    [SerializeField] private float shakeStrength = 20f; // 画面の振動の強さ
    [SerializeField] private int shakeVibrations = 100; // 画面の振動の回数

    [SerializeField] private float damageRate = 0.1f; // デバッグ用ダメージレート
    [SerializeField] private float pauseDuration = 0f; // 一時停止の持続時間
    [SerializeField] private bool playShotSound = true; // ショットサウンドの再生フラグ
    [SerializeField] public static float currentHealthRate = 1f; // 現在のHPレート

    private void Start()
    {
        UpdateHealthBar(1f);
    }

    private void Update()
    {
        Healings();
        HandleCollision();
        CheckHealthStatus();

    }

    /// <summary>
    /// HPバーを指定された値に更新し、アニメーションを実行します。
    /// </summary>
    /// <param name="value">HPバーの新しい値（0から1の範囲）</param>
    public void UpdateHealthBar(float value)
    {
        // DoTweenを使用してHPバーと焼き焦げ効果をアニメーションさせる
        healthBarImage.DOFillAmount(value, animationDuration)
            .OnComplete(() =>
            {
                burnEffectImage
                    .DOFillAmount(value, animationDuration / 2f)
                    .SetDelay(0.5f);
            });
        transform.DOShakePosition(
            animationDuration / 2f,
            shakeStrength, shakeVibrations);

        currentHealthRate = value;
    }

    /// <summary>
    /// 指定された割合でダメージを受けた際にHPバーを更新します。
    /// </summary>
    /// <param name="damage">ダメージの割合（0から1の範囲）</param>
    private void ApplyDamage(float damage)
    {
        UpdateHealthBar(currentHealthRate - damage);
    }

    /// <summary>
    /// HPがゼロ以下の場合にHPを0に設定し、一定時間が経過した後にシーンフラグを設定します。
    /// </summary>
    private void CheckHealthStatus()
    {
        if (currentHealthRate <= 0)
        {
            // HPを0に設定
            currentHealthRate = 0;

            pauseDuration += Time.deltaTime;

            if (pauseDuration >= 3)
            {
                MySceneManager.flag = true;
            }

            if (playShotSound)
            {
                PlaySoundEffect(3);
            }
        }
    }

    /// <summary>
    /// Hatakeが敵と衝突した際にダメージを適用します。
    /// </summary>
    private void HandleCollision()
    {
        if (Hatake.isCollide)
        {
            ApplyDamage(damageRate);
            Hatake.isCollide = false;
        }
    }

    /// <summary>
    /// 指定されたサウンドインデックスのサウンドを再生します。
    /// </summary>
    /// <param name="soundIndex">再生するサウンドのインデックス</param>
    private void PlaySoundEffect(int soundIndex)
    {
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlaySound(soundIndex);
        playShotSound = false;
    }

    private void Healings()
    {
        if (GameObject.Find("Healing(Clone)") == null)
        {
            return;
        }
        else
        {
            Item ItemScripts = GameObject.Find("Healing(Clone)").GetComponent<Item>();
            if (ItemScripts.isHealing)
            {
                if (currentHealthRate < 1)
                {
                    UpdateHealthBar(currentHealthRate + 0.05f);
                }
                // なんかちゃんと消えない時もある
                // なのでItemSpawner.csで強制的に消す処理を追加した
                Destroy(GameObject.Find("Healing(Clone)"));
            }
        }
    }
}
