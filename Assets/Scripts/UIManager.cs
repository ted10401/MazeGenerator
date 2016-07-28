using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public MazeGenerator mazeGenerator;
	[Header("Buttons")]
	public Button generateMazeButton;
	public Button generateRoomButton;
	public Button generateRoomAndMazeButton;
	[Header("Inputs")]
	public InputField widthInput;
	public InputField heightInput;
	public InputField roomTestCountInput;
	[Header("Toggles")]
	public Toggle backtrackingToggle;
	public Toggle backjumpingToggle;
	public Toggle primsToggle;
	public Toggle playAnimationToggle;

	void Awake()
	{
		generateMazeButton.onClick.AddListener(OnGenerateMazeClicked);
		generateRoomButton.onClick.AddListener(OnGenerateRoomClicked);
		generateRoomAndMazeButton.onClick.AddListener(OnGenerateRoomAndMazeClicked);

		widthInput.onEndEdit.AddListener (OnWidthInputFinished);
		heightInput.onEndEdit.AddListener (OnHeightInputFinished);
		roomTestCountInput.onEndEdit.AddListener (OnRoomTestCountInputFinished);

		backtrackingToggle.onValueChanged.AddListener (OnBacktrackingToggle);
		backjumpingToggle.onValueChanged.AddListener (OnBackjumpingToggle);
		primsToggle.onValueChanged.AddListener (OnPrimsToggle);
		playAnimationToggle.onValueChanged.AddListener (OnPlayAnimationToggle);

		OnBacktrackingToggle (true);
	}

	public void OnGenerateMazeClicked()
	{
		mazeGenerator.GenerateMaze ();
	}

	public void OnGenerateRoomClicked()
	{
		mazeGenerator.GenerateRoom ();
	}

	public void OnGenerateRoomAndMazeClicked()
	{
		mazeGenerator.GenerateRoomAndMaze();
	}

	public void OnWidthInputFinished(string value)
	{
		mazeGenerator.width = int.Parse (value);
	}

	public void OnHeightInputFinished(string value)
	{
		mazeGenerator.height = int.Parse (value);
	}


	public void OnRoomTestCountInputFinished(string value)
	{
		mazeGenerator.roomTestCount = int.Parse (value);
	}


	private void OnBacktrackingToggle(bool value)
	{
		if (!value)
			return;

		mazeGenerator.algorithm = new BacktrackingAlgorithm (mazeGenerator);
	}


	private void OnBackjumpingToggle(bool value)
	{
		if (!value)
			return;
		
		mazeGenerator.algorithm = new BackjumpingAlgorithm (mazeGenerator);
	}


	private void OnPrimsToggle(bool value)
	{
		if (!value)
			return;
		
		mazeGenerator.algorithm = new PrimsAlgorithm (mazeGenerator);
	}


	private void OnPlayAnimationToggle(bool value)
	{		
		mazeGenerator.playAnimation = value;
	}
}