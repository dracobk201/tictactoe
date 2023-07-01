using System;
using System.Collections.Generic;

public class GameGrid
{
    public static Mark[] markGrid;

    public GameGrid()
    {
        markGrid = new Mark[9];
        Array.Fill(markGrid, Mark.Null);
    }

    public void MarkCell(int index, Mark markType)
    {
        if (!markGrid[index].Equals(Mark.Null))
        {
            return;
        }
        markGrid[index] = markType;
    }

    public int CheckAndReturnAvailableSlot()
    {
        List<int> indexArray = new List<int>();
        for (int i = 0; i < markGrid.Length; i++)
        {
            if (markGrid[i].Equals(Mark.Null))
            {
                indexArray.Add(i);
            }
        }
        if (indexArray.Count == 0)
        {
            return -1;
        }
        int randomArrayIndex = UnityEngine.Random.Range(0, indexArray.Count);
        return indexArray[randomArrayIndex];
    }

    public static GameResult VerifyPlayerVictory(Mark markType)
    {
        if (markType == Mark.Null)
        {
            return GameResult.Continue;
        }
        Mark[] markAmount = Array.FindAll(markGrid, x => x.Equals(markType));
        var nullAmount = Array.FindAll(markGrid, x => x.Equals(Mark.Null));
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
