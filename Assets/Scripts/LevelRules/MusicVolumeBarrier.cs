using UnityEngine;

public class MusicVolumeBarrier : MonoBehaviour
{
    [SerializeField] private float visibleVolumeThreshold = 0.001f;

    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private Collider2D[] colliders;
    [SerializeField] private Animator[] animators;

    private void Awake()
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        }

        if (colliders == null || colliders.Length == 0)
        {
            colliders = GetComponentsInChildren<Collider2D>(true);
        }

        if (animators == null || animators.Length == 0)
        {
            animators = GetComponentsInChildren<Animator>(true);
        }
    }

    private void OnEnable()
    {
        MusicManager.VolumeChanged += OnMusicVolumeChanged;
        ApplyVolumeState(MusicManager.Volume);
    }

    private void OnDisable()
    {
        MusicManager.VolumeChanged -= OnMusicVolumeChanged;
    }

    private void Start()
    {
        ApplyVolumeState(MusicManager.Volume);
    }

    private void OnMusicVolumeChanged(float volume)
    {
        ApplyVolumeState(volume);
    }

    private void ApplyVolumeState(float volume)
    {
        bool shouldBeVisible = volume > visibleVolumeThreshold;

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = shouldBeVisible;
            }
        }

        foreach (Collider2D collider in colliders)
        {
            if (collider != null)
            {
                collider.enabled = shouldBeVisible;
            }
        }

        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                animator.enabled = shouldBeVisible;
            }
        }
    }
}