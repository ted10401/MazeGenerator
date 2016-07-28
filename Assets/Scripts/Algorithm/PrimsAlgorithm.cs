using System;
using System.Collections;
using System.Collections.Generic;
using Maze.Utils;

public class PrimsAlgorithm : AlgorithmBase
{
	private List<BaseCell> m_cellList;
	
	public PrimsAlgorithm(MazeGenerator mazeGenerator) : base(mazeGenerator)
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
		int randomCache = 0;
		
		while(m_cellList.Count != 0)
		{
			randomCache = UnityEngine.Random.Range(0, m_cellList.Count);
			cellCache = m_cellList[randomCache];
			directionCache = cellCache.GetRandomDirection();

			if(directionCache != Direction.None)
			{
				if(playAnimation)
					yield return null;

				m_cellList.Add(new BaseCell(cellCache, directionCache, m_mazeGenerator));
			}
			else
			{
				m_cellList.RemoveAt(randomCache);
			}
		}
		
		m_isGenerating = false;

		if(null != onComplete)
		{
			onComplete();
		}
	}
}