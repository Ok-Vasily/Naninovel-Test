using Naninovel;
using Naninovel.UI;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogUI : CustomUI
{
    [SerializeField] private Transform _questLogPanel;
    [SerializeField] private GameObject _questLogEntryPrefab;

    private QuestLogEntryController _currentEntry;
    private List<GameObject> _entries = new();

    public override UniTask InitializeAsync()
    {
        Hide();
        return base.InitializeAsync();
    }

    public void AddQuestLogEntry(string entry)
    {
        if (_currentEntry)
            _currentEntry.SetCompleted();

        var newEntry = Instantiate(_questLogEntryPrefab, _questLogPanel).GetComponent<QuestLogEntryController>();
        newEntry.SetText(entry);
        _entries.Add(newEntry.gameObject);
        _currentEntry = newEntry;
    }

    public void FinishQuest()
    {
        if (_currentEntry)
            _currentEntry.SetCompleted();
    }

    public void ResetQuestLog()
    {
        _currentEntry = null;

        foreach (var entry in _entries)
            Destroy(entry);

        _entries.Clear();
    }
}
