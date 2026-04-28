using TMPro;
using UnityEngine;

public class Level1IntroHintController : MonoBehaviour
{
    [SerializeField] private TMP_Text hintText;

    [TextArea(3, 8)]
    [SerializeField] private string startText;

    [TextArea(3, 8)]
    [SerializeField] private string goalText;

    private bool hasChangedText;

    private void Start()
    {
        if (hintText != null)
        {
            hintText.text = startText;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasChangedText)
        {
            return;
        }

        if (!other.TryGetComponent<Player>(out _))
        {
            return;
        }

        hasChangedText = true;

        if (hintText != null)
        {
            hintText.text = goalText;
        }
    }
}