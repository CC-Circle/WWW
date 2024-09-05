
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{

    // 敵のヒットポイントを設定
    public int enemyHP;
    private int wkHP;

    public Slider hpSlider;

    // Use this for initialization
    void Start()
    {
        hpSlider.value = (float)enemyHP;
        wkHP = enemyHP + 50;
    }

    // Update is called once per frame
    void Update()
    {
        // スライダーの向きをカメラ方向に固定
        hpSlider.transform.rotation = Camera.main.transform.rotation;
    }

    // 衝突した瞬間に呼ばれる 
    private void OnTriggerEnter(Collider other)
    {
        // あたった場合敵を削除
        Debug.Log("Hit2");
        wkHP -= 50;
        hpSlider.value = (float)wkHP / (float)enemyHP;
        if (wkHP == 0)
        {
            Debug.Log("Destroy");
            Destroy(gameObject, 0f);
        }
        Controller.isCollide = false;
    }
}