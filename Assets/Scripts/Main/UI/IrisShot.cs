using UnityEngine;
using DG.Tweening;

/// <summary>
/// アイリスエフェクトの拡大縮小を管理するスクリプト。
/// アイリスの拡大と縮小をアニメーションで制御します。
/// </summary>
public class IrisShot : MonoBehaviour
{
    [SerializeField] private RectTransform irisTransform; // アイリスのRectTransform
    private readonly Vector2 expandedScale = new Vector2(30, 30); // 拡大時のスケール
    private readonly float animationDuration = 1f; // アニメーションの持続時間

    /// <summary>
    /// アイリスを拡大します。
    /// </summary>
    public void ExpandIris()
    {
        irisTransform.DOScale(expandedScale, animationDuration).SetEase(Ease.InCubic);
    }

    /// <summary>
    /// アイリスを縮小します。
    /// </summary>
    public void ContractIris()
    {
        irisTransform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.OutCubic);
    }

    private void Start()
    {
        ExpandIris();
    }
}
