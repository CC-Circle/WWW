using UnityEngine;

/// <summary>
/// サウンドの再生を管理するクラス.
/// </summary>
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// 再生可能な音声クリップの配列.
    /// </summary>
    [SerializeField] private AudioClip[] audioClips;

    /// <summary>
    /// 指定されたインデックスの音声クリップを再生する.
    /// </summary>
    /// <param name="index">再生する音声クリップのインデックス.</param>
    public void PlaySound(int index)
    {
        // インデックスが配列の範囲内か確認
        if (audioClips.Length <= index)
        {
            Debug.LogError("指定されたインデックスが範囲外です。");
            return;
        }

        // AudioSource コンポーネントを取得し、指定した音声クリップを再生
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClips[index]);
    }

    /// <summary>
    /// 音量を設定する.
    /// </summary>
    /// <param name="volume">設定する音量（0.0から1.0の範囲）.</param>
    public void SetVolume(float volume)
    {
        // AudioSource コンポーネントを取得し、音量を設定
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }
}
