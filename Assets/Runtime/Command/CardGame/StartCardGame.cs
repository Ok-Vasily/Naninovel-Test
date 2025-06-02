using Naninovel;

[CommandAlias("cardGame")]
public class StartCardGame : Command
{
    [ParameterAlias("pairs")]
    public IntegerParameter TotalPairs = 4;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var gameManager = Engine.GetService<CardGameManager>();
        return gameManager.StartGame(TotalPairs);
    }
}
