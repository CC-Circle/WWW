using UnityEngine;
using DG.Tweening;

public class IrisShot : MonoBehaviour
{
    [SerializeField] RectTransform unmask;
    readonly Vector2 IRIS_IN_SCALE = new Vector2(30, 30);
    readonly float SCALE_DURATION = 1;

    public void IrisIn()
    {
        unmask.DOScale(IRIS_IN_SCALE, SCALE_DURATION).SetEase(Ease.InCubic);
    }

    public void IrisOut()
    {
        unmask.DOScale(new Vector3(0, 0, 0), SCALE_DURATION).SetEase(Ease.OutCubic);
    }
    private void Start()
    {
        IrisIn();
    }
}