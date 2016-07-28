using System;
using System.Collections;
using System.Collections.Generic;
using Maze.Utils;

public class BacktrackingAlgorithm : AlgorithmBase
{
	private Stack<BaseCell> m_cellStack;

	public BacktrackingAlgorithm(MazeGenerator mazeGenerator) : base(mazeGenerator)
	{

	}

	public override IEnumerator Update(bool playAnimation, Action onComplete = null)
	{
		m_isGenerating = true;

		m_cellStack = new Stack<BaseCell> ();

		if(playAnimation)
			yield return null;

		m_cellStack.Push (new BaseCell(0, 0, m_mazeGenerator));

		BaseCell cellCache = null;
		Direction directionCache = Direction.None;

		while(m_cellStack.Count != 0)
		{
			cellCache = m_cellStack.Peek();
			directionCache = cellCache.GetRandomDirection();

			if(directionCache != Direction.None)
			{
				if(playAnimation)
					yield return null;

				m_cellStack.Push(new BaseCell(cellCache, directionCache, m_mazeGenerator));
			}
			else
			{
				m_cellStack.Pop();
			}
		}

		m_isGenerating = false;

		if(null != onComplete)
		{
			onComplete();
		}
	}
}