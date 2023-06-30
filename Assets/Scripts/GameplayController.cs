using System;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    private GameplayView gameplayView;
    private Mark[] markGrid;

    public void Setup(GameplayView gameplayView)
    {
        this.gameplayView = gameplayView;
        markGrid = new Mark[9];
        Array.Fill(markGrid, Mark.Null);
    }

    public void MarkSpecificCell(int index, Mark markType)
    {
        markGrid[index] = markType;
        CheckWinner();
    }

    public void CheckWinner()
    {
        GameResult playerResult = VerifyPlayerVictory(Mark.X);

        switch (playerResult)
        {
            case GameResult.Win:
                gameplayView.ShowWinner();
                break;
            case GameResult.Lose:
                gameplayView.ShowLoser();
                break;
            case GameResult.Draw:
                gameplayView.ShowDraw();
                break;
            case GameResult.Continue:
                break;
            default:
                break;
        }
    }

    private GameResult VerifyPlayerVictory(Mark markType)
    {
        var markAmount = Array.FindAll(markGrid, x=> x.Equals(markType));
        var nullAmount = Array.FindAll(markGrid, x=> x.Equals(Mark.Null));
        if (markAmount.Length >= 3)
        {
            if (markGrid[0].Equals(markType))
            {
                if (markGrid[1].Equals(markType) && markGrid[2].Equals(markType) ||
                    markGrid[3].Equals(markType) && markGrid[6].Equals(markType) ||
                    markGrid[4].Equals(markType) && markGrid[8].Equals(markType))
                {
                    return GameResult.Win;
                }
            }
            else if (markGrid[2].Equals(markType))
            {
                if (markGrid[5].Equals(markType) && markGrid[8].Equals(markType) ||
                    markGrid[4].Equals(markType) && markGrid[6].Equals(markType))
                {
                    return GameResult.Win;
                }
            }
            else if (markGrid[3].Equals(markType) && markGrid[4].Equals(markType) && markGrid[5].Equals(markType) ||
                    markGrid[0].Equals(markType) && markGrid[3].Equals(markType) && markGrid[6].Equals(markType) ||
                    markGrid[1].Equals(markType) && markGrid[4].Equals(markType) && markGrid[7].Equals(markType))
            {
                return GameResult.Win;
            }
        }

        if (nullAmount.Length == 0)
        {
            return GameResult.Draw;
        }
        else
        {
            return GameResult.Continue;
        }
    }
}
