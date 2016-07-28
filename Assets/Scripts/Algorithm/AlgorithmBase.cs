using System;
using System.Collections;

public class AlgorithmBase
{
	public bool IsGenerating
	{
		get { return m_isGenerating; }
	}

	protected MazeGenerator m_mazeGenerator;
	protected bool m_isGenerating = false;

	public AlgorithmBase(MazeGenerator mazeGenerator)
	{
		m_mazeGenerator = mazeGenerator;
	}

	public virtual IEnumerator Update (bool playAnimation, Action onComplete = null)
	{
		return null;
	}
}