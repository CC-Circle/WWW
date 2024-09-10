
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
        wkHP = enemyHP;
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
        if (other.gameObject.tag == "Player")
        {

            SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            soundManager.SetVolume(0.5f);
            soundManager.PlaySound(2);


            // あたった場合敵を削除
            wkHP -= 50;
            hpSlider.value = (float)wkHP / (float)enemyHP;
            if (wkHP == 0)
            {
                Debug.Log("Destroy");
                Destroy(gameObject, 0f);
            }
            // Controller.isCollide = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            soundManager.SetVolume(0.5f);
            soundManager.PlaySound(2);


            // あたった場合敵を削除
            wkHP -= 50;
            hpSlider.value = (float)wkHP / (float)enemyHP;
            if (wkHP == 0)
            {
                Destroy(gameObject, 0f);
            }
            // Controller.isCollide = false;
        }
    }
}