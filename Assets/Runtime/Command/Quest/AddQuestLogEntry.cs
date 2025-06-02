using Naninovel;

[CommandAlias("addQuestLogEntry")]
public class AddQuestLogEntry : Command
{
    [ParameterAlias(NamelessParameterAlias)]
    public StringParameter Entry = "";

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var questManager = Engine.GetService<QuestLogManager>();
        questManager.AdvanceQuest(Entry);
        return UniTask.CompletedTask;
    }
}
