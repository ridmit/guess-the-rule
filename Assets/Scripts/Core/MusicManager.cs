using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public const string VolumeKey = "MusicVolume";

    private const float DefaultVolume = 1f;

    public static MusicManager Instance { get; private set; }

    public static event Action<float> VolumeChanged;

    [SerializeField] private AudioClip musicClip;

    private AudioSource audioSource;

    public static float Volume => PlayerPrefs.GetFloat(VolumeKey, DefaultVolume);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        if (musicClip != null)
        {
            audioSource.clip = musicClip;
        }

        ApplyVolume(Volume);

        if (audioSource.clip != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public static void SetVolume(float volume)
    {
        float clampedVolume = Mathf.Clamp01(volume);

        PlayerPrefs.SetFloat(VolumeKey, clampedVolume);
        PlayerPrefs.Save();

        if (Instance != null)
        {
            Instance.ApplyVolume(clampedVolume);
        }

        VolumeChanged?.Invoke(clampedVolume);
    }

    private void ApplyVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}