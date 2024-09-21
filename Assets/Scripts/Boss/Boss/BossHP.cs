using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    public int maxHealth = 30;
    private GameObject BossPrefab;


    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
        ChangeMaterial();
    }

    void Die()
    {
        // ゆっくりと下に沈む
        transform.Translate(Vector3.down * Time.deltaTime * 100);
    }

    void Update()
    {
        Debug.Log(maxHealth);
        if (maxHealth <= 0)
        {
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
