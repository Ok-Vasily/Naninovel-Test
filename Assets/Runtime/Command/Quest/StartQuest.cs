using Naninovel;

[CommandAlias("startQuest")]
public class StartQuest : Command
{
    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var questManager = Engine.GetService<QuestLogManager>();
        return questManager.StartQuest();
    }
}
