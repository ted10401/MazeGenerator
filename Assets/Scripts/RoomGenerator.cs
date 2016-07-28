using UnityEngine;
using System.Collections.Generic;

public class RoomGenerator
{
	private MazeGenerator m_mazeGenerator;
	private int m_testCount;
	private List<BaseRoom> m_rooms;

	public RoomGenerator(MazeGenerator mazeGenerator, int testCount = 1)
	{
		m_mazeGenerator = mazeGenerator;
		m_testCount = testCount;
	}

	public void Generate()
	{
		m_rooms = new List<BaseRoom> ();

		for(int cnt = 0; cnt < m_testCount; cnt++)
		{
			BaseRoom baseRoom = new BaseRoom(m_mazeGenerator.width, m_mazeGenerator.height);
			if(CanCreateRoom(baseRoom))
			{
				CreateRoom(baseRoom);
			}
		}
	}

	private bool CanCreateRoom(BaseRoom baseRoom)
	{
		if (m_mazeGenerator.maze.HasCell(baseRoom.PivotW, baseRoom.PivotH) ||
			m_mazeGenerator.maze.HasCell(baseRoom.PivotW, baseRoom.PivotH + baseRoom.RoomHeight - 1) ||
			m_mazeGenerator.maze.HasCell(baseRoom.PivotW + baseRoom.RoomWidth - 1, baseRoom.PivotH) ||
			m_mazeGenerator.maze.HasCell(baseRoom.PivotW + baseRoom.RoomWidth - 1, baseRoom.PivotH + baseRoom.RoomHeight - 1))
			return false;

		for(int h = baseRoom.PivotH - 1; h < baseRoom.PivotH + baseRoom.RoomHeight + 1; h++)
		{
			for(int w = baseRoom.PivotW - 1; w < baseRoom.PivotW + baseRoom.RoomWidth + 1; w++)
			{
				if(w == m_mazeGenerator.maze.Width || h == m_mazeGenerator.maze.Width ||
				   w < 0 || h < 0)
					return false;

				if(m_mazeGenerator.maze.HasCell(w, h))
					return false;
			}
		}

		return true;
	}

	private void CreateRoom(BaseRoom baseRoom)
	{
		m_rooms.Add(baseRoom);

		BaseCell baseCell = null;
		float randomR = Random.Range(0, 255f) / 255;
		float randomG = Random.Range(0, 255f) / 255;
		float randomB = Random.Range(0, 255f) / 255;
		
		for(int h = baseRoom.PivotH; h < baseRoom.PivotH + baseRoom.RoomHeight; h++)
		{
			for(int w = baseRoom.PivotW; w < baseRoom.PivotW + baseRoom.RoomWidth; w++)
			{
				baseCell = new BaseCell (w, h, m_mazeGenerator);
				baseCell.SetColor(new Color(randomR, randomG, randomB));
				
				#region corner
				if(w == baseRoom.PivotW && h == baseRoom.PivotH)
				{
					baseCell.DisableWalls (Maze.Utils.Direction.Up);
					baseCell.DisableWalls (Maze.Utils.Direction.Right);
					continue;
				}
				
				if(w == baseRoom.PivotW && h == baseRoom.PivotH + baseRoom.RoomHeight - 1)
				{
					baseCell.DisableWalls (Maze.Utils.Direction.Down);
					baseCell.DisableWalls (Maze.Utils.Direction.Right);
					continue;
				}
				
				if(w == baseRoom.PivotW + baseRoom.RoomWidth - 1 && h == baseRoom.PivotH)
				{
					baseCell.DisableWalls (Maze.Utils.Direction.Up);
					baseCell.DisableWalls (Maze.Utils.Direction.Left);
					continue;
				}
				
				if(w == baseRoom.PivotW + baseRoom.RoomWidth - 1 && h == baseRoom.PivotH + baseRoom.RoomHeight - 1)
				{
					baseCell.DisableWalls (Maze.Utils.Direction.Down);
					baseCell.DisableWalls (Maze.Utils.Direction.Left);
					continue;
				}
				#endregion
				
				#region edge
				if(h == baseRoom.PivotH)
				{
					baseCell.DisableWalls (Maze.Utils.Direction.Up);
					baseCell.DisableWalls (Maze.Utils.Direction.Left);
					baseCell.DisableWalls (Maze.Utils.Direction.Right);
					continue;
				}
				
				if(h == baseRoom.PivotH + baseRoom.RoomHeight - 1)
				{
					baseCell.DisableWalls (Maze.Utils.Direction.Down);
					baseCell.DisableWalls (Maze.Utils.Direction.Left);
					baseCell.DisableWalls (Maze.Utils.Direction.Right);
					continue;
				}
				
				if(w == baseRoom.PivotW)
				{
					baseCell.DisableWalls (Maze.Utils.Direction.Right);
					baseCell.DisableWalls (Maze.Utils.Direction.Up);
					baseCell.DisableWalls (Maze.Utils.Direction.Down);
					continue;
				}
				
				if(w == baseRoom.PivotW + baseRoom.RoomWidth - 1)
				{
					baseCell.DisableWalls (Maze.Utils.Direction.Left);
					baseCell.DisableWalls (Maze.Utils.Direction.Up);
					baseCell.DisableWalls (Maze.Utils.Direction.Down);
					continue;
				}
				#endregion
				
				baseCell.DisableWalls (Maze.Utils.Direction.Up);
				baseCell.DisableWalls (Maze.Utils.Direction.Down);
				baseCell.DisableWalls (Maze.Utils.Direction.Left);
				baseCell.DisableWalls (Maze.Utils.Direction.Right);
			}
		}
	}


	public void CreateDoors()
	{
		for(int cnt = 0; cnt < m_rooms.Count; cnt++)
		{
			CreateDoor(m_rooms[cnt]);
		}
	}


	private void CreateDoor(BaseRoom baseRoom)
	{
		int randomDirection = Random.Range (0, 4);
		int randomW = 0;
		int randomH = 0;

		switch(randomDirection)
		{
		case 0:
			randomW = Random.Range(baseRoom.PivotW, baseRoom.PivotW + baseRoom.RoomWidth);
			randomH = baseRoom.PivotH + baseRoom.RoomHeight - 1;
			m_mazeGenerator.maze.GetCell(randomW, randomH).DisableWalls(Maze.Utils.Direction.Up);
			m_mazeGenerator.maze.GetCell(randomW, randomH + 1).DisableWalls(Maze.Utils.Direction.Down);
			break;
		case 1:
			randomW = Random.Range(baseRoom.PivotW, baseRoom.PivotW + baseRoom.RoomWidth);
			randomH = baseRoom.PivotH;
			m_mazeGenerator.maze.GetCell(randomW, randomH).DisableWalls(Maze.Utils.Direction.Down);
			m_mazeGenerator.maze.GetCell(randomW, randomH - 1).DisableWalls(Maze.Utils.Direction.Up);
			break;
		case 2:
			randomW = baseRoom.PivotW;
			randomH = Random.Range(baseRoom.PivotH, baseRoom.PivotH + baseRoom.RoomHeight);
			m_mazeGenerator.maze.GetCell(randomW, randomH).DisableWalls(Maze.Utils.Direction.Left);
			m_mazeGenerator.maze.GetCell(randomW - 1, randomH).DisableWalls(Maze.Utils.Direction.Right);
			break;
		case 3:
			randomW = baseRoom.PivotW + baseRoom.RoomWidth - 1;
			randomH = Random.Range(baseRoom.PivotH, baseRoom.PivotH + baseRoom.RoomHeight);
			m_mazeGenerator.maze.GetCell(randomW, randomH).DisableWalls(Maze.Utils.Direction.Right);
			m_mazeGenerator.maze.GetCell(randomW + 1, randomH).DisableWalls(Maze.Utils.Direction.Left);
			break;
		}
	}
}