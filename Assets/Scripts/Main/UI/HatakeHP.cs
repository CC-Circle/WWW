using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HatakeHP : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Image burnImage;

    public float duration = 0.5f;
    public float strength = 20f;
    public int vibrate = 100;

    public float debugDamageRate = 0.1f;

    private float currentRate = 1f;

    private void Start()
    {
        SetGauge(1f);
        Hatake hatakeScript = GameObject.Find("Hatake").GetComponent<Hatake>();
    }

    public void SetGauge(float value)
    {
        // DoTweenを連結して動かす
        healthImage.DOFillAmount(value, duration)
            .OnComplete(() =>
            {
                burnImage
                    .DOFillAmount(value, duration / 2f)
                    .SetDelay(0.5f);
            });
        transform.DOShakePosition(
            duration / 2f,
            strength, vibrate);

        currentRate = value;
    }

    public void TakeDamage(float rate)
    {
        SetGauge(currentRate - rate);
    }

    private void Update()
    {
        if (Hatake.isCollide)
        {
            TakeDamage(debugDamageRate);
            Hatake.isCollide = false;
        }
        if (currentRate <= 0)
        {
            MySceneManager.flag = true;
        }
    }
}