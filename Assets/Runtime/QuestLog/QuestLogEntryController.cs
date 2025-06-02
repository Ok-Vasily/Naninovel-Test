using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogEntryController : MonoBehaviour
{
    [SerializeField] private TMP_Text _entryText;
    [SerializeField] private Image _image;

    public void SetText(string text)
    {
        _entryText.text = text;
    }

    public void SetCompleted()
    {
        _entryText.fontStyle = FontStyles.Strikethrough;
        _entryText.color = Color.grey;
        _entryText.outlineWidth = 0;
        _image.enabled = false;
    }
}
