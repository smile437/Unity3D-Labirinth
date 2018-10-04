using UnityEngine;
using System.Collections;

public abstract class BaseLevelGenerator
{
	private MazeCell[,] mazeCell;

	public int RowCount{ get;}
	public int ColumnCount { get;}

	public BaseLevelGenerator(int rows, int columns)
	{
		this.RowCount = Mathf.Abs(rows);
		this.ColumnCount = Mathf.Abs(columns);
		if (this.RowCount == 0)
		{
			this.RowCount = 1;
		}
		if (this.ColumnCount == 0)
		{
			this.ColumnCount = 1;
		}

		mazeCell = new MazeCell[rows,columns];

		for (int row = 0; row < rows; row++)
		{
			for(int column = 0; column < columns; column++)
			{
				mazeCell[row,column] = new MazeCell();
			}
		}
	}

	public abstract void GenerateMaze();

	public MazeCell GetMazeCell(int row, int column)
	{
		if (row >= 0 && column >= 0 && row < this.RowCount && column < this.ColumnCount) 
		{
			return mazeCell[row,column];
		}
		else
		{
			Debug.Log(row+" "+column);
			throw new System.ArgumentOutOfRangeException();
		}
	}

	protected void SetMazeCell(int row, int column, MazeCell cell)
	{
		if (row >= 0 && column >= 0 && row < this.RowCount && column < this.ColumnCount) 
		{
			mazeCell[row,column] = cell;
		}
		else
		{
			throw new System.ArgumentOutOfRangeException();
		}
	}
}
