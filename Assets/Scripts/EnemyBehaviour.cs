public class EnemyBehaviour
{
    private GameplayController gameplayController;
    private GameGrid gameGrid;

    public EnemyBehaviour(GameplayController gameplayController, GameGrid gameGrid)
    {
        this.gameplayController = gameplayController;
        this.gameGrid = gameGrid;
    }

    public void DoTurn()
    {
        int validIndex = gameGrid.CheckAndReturnAvailableSlot();
        gameplayController.MarkSpecificCell(validIndex, Mark.O);
    }
}
