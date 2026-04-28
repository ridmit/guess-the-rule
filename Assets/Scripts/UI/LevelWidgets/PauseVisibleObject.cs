using UnityEngine;
using UnityEngine.Tilemaps;

public class PauseVisibleObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private TilemapRenderer[] tilemapRenderers;

    private void Awake()
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        }

        if (tilemapRenderers == null || tilemapRenderers.Length == 0)
        {
            tilemapRenderers = GetComponentsInChildren<TilemapRenderer>(true);
        }

        SetVisible(false);
    }

    public void SetVisible(bool visible)
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = visible;
            }
        }

        foreach (TilemapRenderer tilemapRenderer in tilemapRenderers)
        {
            if (tilemapRenderer != null)
            {
                tilemapRenderer.enabled = visible;
            }
        }
    }
}