using UnityEngine;
using System.Collections;

public class BaseMaze : MonoBehaviour
{
	public int Width { get { return m_width; } }
	public int Height { get { return m_height; } }

	public GameObject emptyCellPrefab;
	public Transform emptyCellRoot;
	public Transform cellRoot;

	private int m_width;
	private int m_height;
	private BaseCell[,] m_maze;
	private GameObject[,] m_emptyMaze;

	public void InitializeMaze(int width, int height)
	{
		DestroyMaze();

		m_width = width;
		m_height = height;
		m_maze = new BaseCell[m_width, m_height];
		m_emptyMaze = new GameObject[m_width, m_height];

		for(int w = 0; w < m_width; w++)
		{
			for(int h = 0; h < m_height; h++)
			{
				m_emptyMaze[w, h] = Instantiate(emptyCellPrefab);
				m_emptyMaze[w, h].transform.position = new Vector3(w, h);
				m_emptyMaze[w, h].transform.SetParent(emptyCellRoot);
			}
		}
	}


	public BaseCell GetCell(int width, int height)
	{
		if(width < 0 || width >= m_width ||
			height < 0 || height >= m_height)
			return null;

		return m_maze[width, height];
	}


	public void SetCell(int width, int height, BaseCell cell)
	{
		if(width < 0 || width >= m_width ||
			height < 0 || height >= m_height)
			return;

		m_maze[width, height] = cell;
		m_maze[width, height].Root.transform.SetParent(cellRoot);
	}


	private void DestroyMaze()
	{
		if (null == m_maze)
			return;

		BaseCell cell = null;
		GameObject cellObject = null;

		for(int h = 0; h < m_height; h++)
		{
			for(int w = 0; w < m_width; w++)
			{
				cell = GetCell(w, h);
				if(null != cell)
				{
					Destroy(m_maze[w, h].Root);
				}

				cellObject = m_emptyMaze[w, h];
				if(null != cellObject)
				{
					Destroy(cellObject);
				}
			}
		}

		m_maze = null;
		m_emptyMaze = null;
	}


	public bool HasCell(int width, int height)
	{
		return null != GetCell(width, height);
	}
}