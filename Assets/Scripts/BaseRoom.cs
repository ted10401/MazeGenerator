using UnityEngine;

public class BaseRoom
{
	public int PivotW { get { return m_pivotW; } }
	public int PivotH { get { return m_pivotH; } }
	public int RoomWidth { get { return m_roomWidth; } }
	public int RoomHeight { get { return m_roomHeight; } }

	private int m_pivotW;
	private int m_pivotH;
	private int m_roomWidth;
	private int m_roomHeight;

	public BaseRoom(int width, int height)
	{
		m_pivotW = Random.Range (1, width - 2);
		m_pivotH = Random.Range (1, height - 2);

		m_roomWidth = Random.Range (2, Mathf.Min(width - m_pivotW, 20));
		m_roomHeight = Random.Range (2, Mathf.Min(height - m_pivotH, 20));

		if((float)m_roomHeight / m_roomWidth > 1.5f)
		{
			m_roomHeight = (int)(m_roomWidth * Random.Range(1.0f, 1.5f));
		}
		else if((float)m_roomWidth / m_roomHeight > 1.5f)
		{
			m_roomWidth = (int)(m_roomHeight * Random.Range(1.0f, 1.5f));
		}
	}
}