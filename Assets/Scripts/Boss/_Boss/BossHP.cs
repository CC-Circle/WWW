using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    public int maxHealth = 30;
    private GameObject BossPrefab;

    [SerializeField] private AudioSource delayedAudioSource; // 遅延再生しているオーディオソース
    [SerializeField] private AudioSource immediateAudioSource; // 撃破音を定義

    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
        ChangeMaterial();
    }

    void Die()
    {   
        // ゆっくりと下に沈む
        transform.Translate(Vector3.down * Time.deltaTime * 100);

         // 優先的にオーディオソースを再生
        immediateAudioSource.Play();

        if (transform.position.y < -300)
        {
            MySceneManager.flag = true;
        }

    }

    void Update()
    {
        // Debug.Log(maxHealth);
        if (maxHealth <= 0)
        {
            //再生しているものを停止
            delayedAudioSource.Stop();
            Die();
        }
    }

    private void ChangeMaterial()
    {
        // BossBodyのBossマテリアルを取得
        BossPrefab = GameObject.Find("BossBody");
        // maxHealthが30から0に近づくにつれて色を変化させる
        if (maxHealth <= 30)
        {
            float t = Mathf.InverseLerp(30, 0, maxHealth); // maxHealthが30→0に近づくとtが0→1になる
            Color startColor = new Color(0f, 0f, 0f); // 焦げ茶色
            Color endColor = Color.red; // 真っ赤
            // materials[1] = BossBodyのマテリアルのBossマテリアル
            BossPrefab.GetComponent<Renderer>().materials[1].color = Color.Lerp(startColor, endColor, t);
        }
    }
}
