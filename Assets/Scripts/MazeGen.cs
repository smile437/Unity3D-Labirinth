﻿using UnityEngine;
using System.Collections;

public class MazeGen : BaseLevelGenerator 
{
	public static int GoalsCount = 0;

	public MazeGen(int rows, int columns):base(rows,columns)
	{

	}

	public override void GenerateMaze ()
	{
		VisitCell (0, 0, Direction.Start);
	}

	private void VisitCell(int row, int column, Direction moveMade)
	{
		Direction[] movesAvailable = new Direction[4];
		int movesAvailableCount = 0;

		do
		{
			movesAvailableCount = 0;

			if(column+1 < ColumnCount && !GetMazeCell(row,column+1).IsVisited)
			{
				movesAvailable[movesAvailableCount] = Direction.Right;
				movesAvailableCount++;
			}
			else if(!GetMazeCell(row,column).IsVisited && moveMade != Direction.Left)
			{
				GetMazeCell(row,column).WallRight = true;
			}

			if(row+1 < RowCount && !GetMazeCell(row+1,column).IsVisited)
			{
				movesAvailable[movesAvailableCount] = Direction.Front;
				movesAvailableCount++;
			}
			else if(!GetMazeCell(row,column).IsVisited && moveMade != Direction.Back)
			{
				GetMazeCell(row,column).WallFront = true;
			}

			if(column > 0 && column-1 >= 0 && !GetMazeCell(row,column-1).IsVisited)
			{
				movesAvailable[movesAvailableCount] = Direction.Left;
				movesAvailableCount++;
			}
			else if(!GetMazeCell(row,column).IsVisited && moveMade != Direction.Right)
			{
				GetMazeCell(row,column).WallLeft = true;
			}

			if(row > 0 && row-1 >= 0 && !GetMazeCell(row-1,column).IsVisited)
			{
				movesAvailable[movesAvailableCount] = Direction.Back;
				movesAvailableCount++;
			}
			else if(!GetMazeCell(row,column).IsVisited && moveMade != Direction.Front)
			{
				GetMazeCell(row,column).WallBack = true;
			}

			if(movesAvailableCount == 0 && !GetMazeCell(row,column).IsVisited && GoalsCount<10)
			{
				GetMazeCell(row,column).IsGoal = true;
				GoalsCount++;
			}

			if(movesAvailableCount == 2 && GetMazeCell(row,column).IsVisited && GoalsCount<10)
			{
				GetMazeCell(row,column).IsGoal = true;
				GoalsCount++;
			}

			GetMazeCell(row,column).IsVisited = true;

			if(movesAvailableCount > 0)
			{
				switch (movesAvailable[Random.Range(0,movesAvailableCount)])
				{
				case Direction.Start:
					break;
				case Direction.Right:
					VisitCell(row,column+1,Direction.Right);
					break;
				case Direction.Front:
					VisitCell(row+1,column,Direction.Front);
					break;
				case Direction.Left:
					VisitCell(row,column-1,Direction.Left);
					break;
				case Direction.Back:
					VisitCell(row-1,column,Direction.Back);
					break;
				}
			}

		}
		while(movesAvailableCount > 0);
	}
}
