using UnityEngine;
using System;
using System.Collections.Generic;
using TEDCore.Utils;
using Maze.Utils;

public class BaseCell
{
	public const string PREFAB_NAME = "CellPrefab";

	public Vector2 Center { get { return m_center; } }
	public GameObject Root { get { return m_root; } }

	private Vector2 m_center;
	private GameObject m_root;
	private BaseMaze m_maze;
	private Dictionary<Direction, NeighbourType> m_neighbours;
	private Dictionary<Direction, GameObject> m_walls;

	public BaseCell(int w, int h, MazeGenerator mazeGenerator)
	{
		m_center = new Vector2 (w, h);

		m_root = GameObject.Instantiate (Resources.Load(PREFAB_NAME) as GameObject);
		m_root.name = string.Format("Cell {0},{1}", m_center.x, m_center.y);
		m_root.transform.position = new Vector3 (m_center.x, m_center.y, -0.1f);

		m_maze = mazeGenerator.maze;
		m_maze.SetCell(w, h, this);

		SetupWalls (Direction.None);
		SetupNeighbours ();
	}


	public BaseCell(BaseCell previousCell, Direction direction, MazeGenerator mazeGenerator)
	{
		m_center = previousCell.Center + MazeUtils.DirectionToVector2(direction);

		m_root = GameObject.Instantiate (Resources.Load(PREFAB_NAME) as GameObject);
		m_root.name = string.Format("Cell {0},{1}", m_center.x, m_center.y);
		m_root.transform.position = new Vector3 (m_center.x, m_center.y, -0.1f);

		m_maze = mazeGenerator.maze;
		m_maze.SetCell((int)m_center.x, (int)m_center.y, this);

		SetupWalls (direction);
		SetupNeighbours ();
	}


	public void SetColor(Color color)
	{
		m_root.FindChild("Center").GetComponent<MeshRenderer>().material.color = color;
	}


	private void SetupWalls(Direction initialDirection)
	{
		m_walls = new Dictionary<Direction, GameObject> ();
		for(int cnt = 1; cnt < Enum.GetNames(typeof(Direction)).Length; cnt++)
		{
			GameObject wall = m_root.FindChild("Wall" + ((Direction)cnt).ToString());
			m_walls.Add((Direction)cnt, wall);

			if(initialDirection != Direction.None)
			{
				if((Direction)cnt == MazeUtils.GetOppositeDirection(initialDirection))
				{
					DisableWalls((Direction)cnt);
				}
			}
		}
	}


	public void DisableWalls(Direction direction)
	{
		m_walls [direction].SetActive (false);
	}


	private void SetupNeighbours()
	{
		m_neighbours = new Dictionary<Direction, NeighbourType> ();
		for(int cnt = 1; cnt < Enum.GetNames(typeof(Direction)).Length; cnt++)
		{
			m_neighbours.Add((Direction)cnt, NeighbourType.None);
		}

		if (m_center.y + 1 == m_maze.Height)
			m_neighbours[Direction.Up] = NeighbourType.Wall;

		if (m_center.y - 1 < 0)
			m_neighbours[Direction.Down] = NeighbourType.Wall;

		if (m_center.x - 1 < 0)
			m_neighbours[Direction.Left] = NeighbourType.Wall;

		if (m_center.x + 1 == m_maze.Width)
			m_neighbours[Direction.Right] = NeighbourType.Wall;

		UpdateNeighbours ();
	}


	private void UpdateNeighbours()
	{
		for(int cnt = 1; cnt < Enum.GetNames(typeof(Direction)).Length; cnt++)
		{			
			Vector2 direction = m_center + MazeUtils.DirectionToVector2 ((Direction)cnt);
			if(direction.x < 0 || direction.x == m_maze.Width ||
				direction.y < 0 || direction.y == m_maze.Height)
				continue;

			if(m_maze.HasCell((int)direction.x, (int)direction.y))
			{
				m_neighbours[(Direction)cnt] = NeighbourType.Visited;
			}
		}
	}


	public Direction GetRandomDirection()
	{
		UpdateNeighbours ();

		List<int> directions = new List<int> ();
		bool noNeighbour = true;
		for(int cnt = 1; cnt < Enum.GetNames(typeof(Direction)).Length; cnt++)
		{
			if(m_neighbours[(Direction)cnt] == NeighbourType.None)
			{
				noNeighbour = false;
				directions.Add(cnt);
			}
		}

		if (noNeighbour)
			return Direction.None;

		int randomNum = UnityEngine.Random.Range (0, directions.Count);
		Direction randomDirection = (Direction)directions[randomNum];

		DisableWalls (randomDirection);

		return randomDirection;
	}
}