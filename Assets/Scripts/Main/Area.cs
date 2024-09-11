using UnityEngine;

/// <summary>
/// 指定したコライダー範囲内にオブジェクトの位置を制限するスクリプト。
/// </summary>
public class Area : MonoBehaviour
{
    [SerializeField] private Collider areaCollider; // 範囲を示すコライダー

    private Vector3 minBounds; // 範囲の最小座標
    private Vector3 maxBounds; // 範囲の最大座標

    private void Start()
    {
        InitializeBounds();
    }

    private void Update()
    {
        RestrictPositionWithinBounds(minBounds, maxBounds);
    }

    /// <summary>
    /// コライダーから範囲の最小座標と最大座標を取得します。
    /// </summary>
    private void InitializeBounds()
    {
        if (areaCollider != null)
        {
            Bounds bounds = areaCollider.bounds;
            minBounds = bounds.min;
            maxBounds = bounds.max;
        }
        else
        {
            Debug.LogError("Area Collider is not assigned!");
        }
    }

    /// <summary>
    /// オブジェクトの位置を指定した範囲内に制限します。
    /// </summary>
    /// <param name="min">範囲の最小座標</param>
    /// <param name="max">範囲の最大座標</param>
    private void RestrictPositionWithinBounds(Vector3 min, Vector3 max)
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, min.x, max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, min.y, max.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, min.z, max.z);

        transform.position = clampedPosition;
    }
}
