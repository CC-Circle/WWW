using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Blinker : MonoBehaviour
{
    public float speed = 1.0f;
    private float time;
    private TextMeshProUGUI text;


    void Start()
    {
        text = this.gameObject.GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        text.color = GetTextColorAlpha(text.color);
    }

    Color GetTextColorAlpha(Color color)
    {
        time += Time.deltaTime * speed * 5.0f;
        color.a = Mathf.Sin(time);

        return color;
    }
}