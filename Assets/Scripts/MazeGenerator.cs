using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
	public BaseMaze maze;
	public int width = 10;
	public int height = 10;
	public int roomTestCount = 10;
	public bool playAnimation = true;
	public AlgorithmBase algorithm;

	private RoomGenerator m_roomGenerator;

	private void SetupCamera()
	{
		Camera.main.transform.position = new Vector3 (width - 1, height - 1, -20) / 2;

		if(Screen.width > Screen.height)
		{
			float ratio = (float)Screen.width / Screen.height;

			if((float)width / height > ratio)
				Camera.main.orthographicSize = (float)width / 2 + 1;
			else
				Camera.main.orthographicSize = (float)height / 2 + 1;
		}
		else
		{
			float ratio = (float)Screen.height / Screen.width;

			if((float)height / width > ratio)
				Camera.main.orthographicSize = (float)height / 2 + 1;
			else
				Camera.main.orthographicSize = (float)width / 2 + 1;
		}
	}


	private void InitializeMaze()
	{
		SetupCamera ();
		maze.InitializeMaze(width, height);
	}


	private bool IsGenerating()
	{
		if (null != algorithm && algorithm.IsGenerating)
		{
			return true;
		}

		return false;
	}


	public void GenerateMaze()
	{
		if(IsGenerating())
		{
			return;
		}

		StopGenerateMaze ();
		InitializeMaze ();
		StartGenerateMaze ();
	}


	public void GenerateRoom()
	{
		if(IsGenerating())
		{
			return;
		}

		InitializeMaze ();
		StartCreateRoom();
	}


	public void GenerateRoomAndMaze()
	{
		if(IsGenerating())
		{
			return;
		}

		StopGenerateMaze();
		InitializeMaze();
		StartCreateRoom();
		StartGenerateMaze(m_roomGenerator.CreateDoors);
	}


	private void StartCreateRoom()
	{
		m_roomGenerator = new RoomGenerator(this, roomTestCount);
		m_roomGenerator.Generate ();
	}


	private void StartGenerateMaze(Action callback = null)
	{
		StartCoroutine(algorithm.Update(playAnimation, callback));
	}


	private void StopGenerateMaze()
	{
		StopCoroutine(algorithm.Update(playAnimation));
	}
}