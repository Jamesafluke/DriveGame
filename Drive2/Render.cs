using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive2;
public class Render()
{
    const char wallCharacter = '#';
    const char gapCharacter = ' ';
    const char shipCharacter = '^';
    const int width =30;
    const int height = 20;
    const int gapAmount = 10;

    public char[][] OldFrame { get; set; } = new char[height][];
    public char[][] NewFrame { get; set; } = new char[height][];
    public int shipPosititon { get; set; } 

    public void MoveShipRight()
    {
        if(shipPosititon < width) { shipPosititon++; }
    }
    public void MoveShipLeft()
    {
        if(shipPosititon > 0) { shipPosititon--; }
    }
    public void GenerateAndRenderFirstFrame()
    {
        OldFrame[0] = new char[width];
        int middle = width / 2;
        int endRightWall = middle - (gapAmount / 2);
        int endGap = endRightWall + gapAmount;
        for (int i = 0; i < width; i++)
        {
            if (i < endRightWall)
            {
                OldFrame[0][i] = wallCharacter;
            }
            else if (i < endGap)
            {
                OldFrame[0][i] = gapCharacter;
            }
            else
            {
                OldFrame[0][i] = wallCharacter;
            }
        }

        for (int i = 0; i < height - 1; i++)
        {
            OldFrame[i + 1] = GenerateLine(OldFrame[i]);
        }
        
        NewFrame = OldFrame;

        foreach (var line in NewFrame)
        {
            foreach (var character in line)
            {
                Console.Write(character);
            }
            Console.WriteLine();
        }

        //Find middle, place ship.
        shipPosititon = ((endGap - endRightWall) / 2) + endRightWall;
    }

    private char[] GenerateLine(char[] previousLine)
    {
        int previousFirstWallCount = 0;
        foreach (var c in previousLine)
        {
            if (c == wallCharacter)
            {
                previousFirstWallCount++;
            }
            else
            {
                break;
            }
        }

        int leftWallCount = 0;

        int random = Random.Shared.Next(0, 3);
        if (random == 0)
        {
            leftWallCount = previousFirstWallCount - 1;
        }
        else if (random == 1)
        {
            leftWallCount = previousFirstWallCount;
        }
        else
        {
            leftWallCount = previousFirstWallCount + 1;
        }

        char[] line = new char[width];
        int leftWallCounter = 0;
        int gapCount = 0;
        for (int i = 0; i < width; i++)
        {
            if (leftWallCounter < leftWallCount)
            {
                line[i] = wallCharacter;
                leftWallCounter++;
            }
            else if (gapCount < gapAmount)
            {
                line[i] = gapCharacter;
                gapCount++;
            }
            else
            {
                line[i] = wallCharacter;
            }
        }
        return line;
    }

    public bool RenderFrame()
    {
        Console.CursorVisible = false;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (OldFrame[y][x] == NewFrame[y][x])
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(NewFrame[y][x]);
                }
            }
            Console.WriteLine();
        }
        
        if ((NewFrame[height - 1][shipPosititon]) == wallCharacter)
        {
            return true;
        }
        else
        {
            Console.SetCursorPosition(shipPosititon, height - 1);
            Console.Write(shipCharacter);
        }
        OldFrame = NewFrame;
        return false;
    }

    public void UpdateFrame()
    {
        for (int i = height - 1; i > 0; i--)
        {
            NewFrame[i] = NewFrame[i - 1];
        }
        NewFrame[0] = GenerateLine(NewFrame[1]);
    }

}
