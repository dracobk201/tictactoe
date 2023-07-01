using UnityEngine;

public class GameplayController : MonoBehaviour
{
    private GameplayView gameplayView;
    private EnemyBehaviour enemy;
    private GameGrid gameGrid;
    private Mark actualTurn;

    public void Setup(GameplayView gameplayView)
    {
        this.gameplayView = gameplayView;
        gameGrid = new GameGrid();
        enemy = new EnemyBehaviour(this, gameGrid);
        actualTurn = Mark.Null;
        ChangeTurn();
    }

    public void MarkSpecificCell(int index, Mark markType)
    {
        Debug.Log($"ActualTurn: {actualTurn} - Index: {index} - Mark: {markType}");
        if (markType != actualTurn)
        {
            return;
        }
        if (markType == Mark.O)
        {
            gameplayView.ActivateCell(index);
        }
        gameGrid.MarkCell(index, markType);
        ChangeTurn();
        Debug.Log($"Next Turn: {actualTurn}");

    }

    public void ChangeTurn()
    {
        if (!CheckWinner(actualTurn))
        {
            if (actualTurn.Equals(Mark.X))
            {
                actualTurn = Mark.O;
                Invoke(nameof(PerformEnemyTurn), 1f);
            }
            else
            {
                actualTurn = Mark.X;
            }
            gameplayView.ShowCurentTurn(actualTurn);
        }
    }

    private bool CheckWinner(Mark playerToVerify)
    {
        GameResult playerResult = GameGrid.VerifyPlayerVictory(playerToVerify);
        Debug.Log($"{playerResult} - {playerToVerify}");
        bool isGameOver = false;
        if (playerResult.Equals(GameResult.Win))
        {
            if (actualTurn.Equals(Mark.X))
            {
                gameplayView.ShowWinner();
                isGameOver = true;
            }
            else if(actualTurn.Equals(Mark.O))
            {
                gameplayView.ShowLoser();
                isGameOver = true;
            }
        }
        else if (playerResult.Equals(GameResult.Draw))
        {
            gameplayView.ShowDraw();
            isGameOver = true;
        }
        return isGameOver;
    }

    private void PerformEnemyTurn()
    {
        enemy.DoTurn();
    }
}
