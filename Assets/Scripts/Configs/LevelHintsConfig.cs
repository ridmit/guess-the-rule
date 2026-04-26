using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelHintsConfig", menuName = "Game/Level Hints Config")]
public class LevelHintsConfig : ScriptableObject
{
    [SerializeField] private string levelLabelText = "Уровень";

    [TextArea(2, 5)]
    [SerializeField] private List<string> hints = new List<string>();

    public string LevelLabelText => levelLabelText;

    public string GetHint(int levelNumber)
    {
        int index = levelNumber - 1;

        if (index < 0 || index >= hints.Count)
        {
            return "Для этого уровня не предусмотрена подсказка";
        }

        return hints[index];
    }
}