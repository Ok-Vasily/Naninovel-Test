using Naninovel;
using UnityEngine;

[InitializeAtRuntime]
public class QuestLogManager : IEngineService
{
    private QuestLogUI _questLogUI;

    public UniTask InitializeServiceAsync() => UniTask.CompletedTask;

    public void DestroyService() { }

    public void ResetService()
    {
        if (_questLogUI)
            _questLogUI.ResetQuestLog();
    }

    public UniTask StartQuest()
    {
        _questLogUI = Engine.GetService<UIManager>().GetUI<QuestLogUI>();
        _questLogUI.Show();
        return UniTask.CompletedTask;
    }

    public void AdvanceQuest(string questLogEntry)
    {
        _questLogUI.AddQuestLogEntry(questLogEntry);
    }

    public async UniTask FinishQuest()
    {
        _questLogUI.FinishQuest();
        await UniTask.Delay(500);
        _questLogUI.Hide();
    }
}