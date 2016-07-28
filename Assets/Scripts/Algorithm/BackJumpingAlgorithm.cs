using System;
using System.Collections;
using System.Collections.Generic;
using Maze.Utils;

public class BackjumpingAlgorithm : AlgorithmBase
{
	private List<BaseCell> m_cellList;

	public BackjumpingAlgorithm(MazeGenerator mazeGenerator) : base(mazeGenerator)
	{

	}

	public override IEnumerator Update(bool playAnimation, Action onComplete = null)
	{
		m_isGenerating = true;

		m_cellList = new List<BaseCell> ();

		if(playAnimation)
			yield return null;

		m_cellList.Add (new BaseCell(0, 0, m_mazeGenerator));

		BaseCell cellCache = null;
		Direction directionCache = Direction.None;

		while(m_cellList.Count != 0)
		{
			cellCache = m_cellList[m_cellList.Count - 1];
			directionCache = cellCache.GetRandomDirection();

			if(directionCache != Direction.None)
			{
				if(playAnimation)
					yield return null;

				m_cellList.Add(new BaseCell(cellCache, directionCache, m_mazeGenerator));
			}
			else
			{
				m_cellList.RemoveAt(m_cellList.Count - 1);

				if(m_cellList.Count >= 2)
				{
					BaseCell firstCell = m_cellList[0];
					m_cellList.RemoveAt(0);
					m_cellList.Add(firstCell);
				}
			}
		}

		m_isGenerating = false;

		if(null != onComplete)
		{
			onComplete();
		}
	}
}