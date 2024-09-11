using UnityEngine;

/// <summary>
/// スカイボックスを回転させるスクリプト.
/// </summary>
public class RotateSkyBox : MonoBehaviour
{
    /// <summary>
    /// スカイボックスの回転スピード.
    /// </summary>
    [SerializeField] private float rotateSpeed = 0.5f;

    /// <summary>
    /// スカイボックスのマテリアル.
    /// </summary>
    private Material skyboxMaterial;

    /// <summary>
    /// スクリプトが開始されるときに呼び出される.
    /// </summary>
    void Start()
    {
        // Lighting Settingsで指定したスカイボックスのマテリアルを取得
        skyboxMaterial = RenderSettings.skybox;
    }

    /// <summary>
    /// 毎フレーム呼び出される.
    /// </summary>
    void Update()
    {
        RotateSkybox();
    }

    /// <summary>
    /// スカイボックスを回転させる.
    /// </summary>
    private void RotateSkybox()
    {
        // スカイボックスのマテリアルが設定されている場合のみ処理
        if (skyboxMaterial != null)
        {
            // スカイボックスマテリアルのRotationを操作して角度を変化させる
            float currentRotation = skyboxMaterial.GetFloat("_Rotation");
            float newRotation = Mathf.Repeat(currentRotation + rotateSpeed * Time.deltaTime, 360f);
            skyboxMaterial.SetFloat("_Rotation", newRotation);
        }
    }
}
