using Naninovel;

[CommandAlias("finishQuest")]
public class FinishQuest : Command
{
    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var questManager = Engine.GetService<QuestLogManager>();
        return questManager.FinishQuest();
    }
}

