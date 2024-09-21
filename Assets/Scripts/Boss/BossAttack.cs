using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private Image healthBarImage; // HPバーの画像
    [SerializeField] private Image burnEffectImage; // 焼き焦げ効果の画像

    [SerializeField] private float animationDuration = 0.5f; // HPバーアニメーションの持続時間
    [SerializeField] private float shakeStrength = 20f; // 画面の振動の強さ
    [SerializeField] private int shakeVibrations = 100; // 画面の振動の回数

    [SerializeField] private float damageRate = 0.1f; // デバッグ用ダメージレート
    [SerializeField] private float pauseDuration = 0f; // 一時停止の持続時間
    [SerializeField] private float attackTimer = 0f; // 攻撃の間隔
    [SerializeField] private bool playShotSound = true; // ショットサウンドの再生フラグ
    [SerializeField] public static float currentHealthRate = 1f; // 現在のHPレート
    private float HatakeHP;

    void Start()
    {
        GetHPAndHealing();
        UpdateHealthBar(HatakeHP);
    }

    void Update()
    {
        CheckHealthStatus();
        Attack();
    }

    private void GetHPAndHealing()
    {
        HatakeHP = PlayerPrefs.GetFloat("HP", 1f);
        HatakeHP += 0.5f;
        if (HatakeHP > 1f) HatakeHP = 1f;
    }

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

    private void Attack()
    {
        //  3秒ごとにダメージを与える
        attackTimer += Time.deltaTime;
        if (attackTimer >= 3)
        {
            ApplyDamage(damageRate);
            attackTimer = 0;
        }

    }

    private void ApplyDamage(float damage)
    {
        UpdateHealthBar(currentHealthRate - damage);
    }

    private void CheckHealthStatus()
    {
        if (currentHealthRate <= 0)
        {
            // HPを0に設定
            currentHealthRate = 0;

            pauseDuration += Time.deltaTime;

            if (pauseDuration >= 3) MySceneManager.flag = true;

            if (playShotSound) { }// PlaySoundEffect(3);
        }
    }

    // private void PlaySoundEffect(int soundIndex)
    // {
    //     SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    //     soundManager.PlaySound(soundIndex);
    //     playShotSound = false;
    // }

    private float GetTimer()
    {
        Timer TimerScripts = GameObject.Find("UIManager").GetComponent<Timer>();
        return TimerScripts.countTime;
    }

    private void SaveHP()
    {
        PlayerPrefs.SetFloat("HP", currentHealthRate);
    }
}
