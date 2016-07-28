using UnityEngine;

namespace Maze.Utils
{
	public enum Direction
	{
		None,
		Up,
		Down,
		Left,
		Right,
	}
	
	public enum NeighbourType
	{
		Wall,
		Visited,
		None,
	}

	public static class MazeUtils
	{
		public static Vector2 DirectionToVector2(Direction direction)
		{
			Vector2 result = Vector2.zero;

			switch(direction)
			{
			case Direction.Up:
				result = Vector2.up;
				break;
			case Direction.Down:
				result = Vector2.down;
				break;
			case Direction.Left:
				result = Vector2.left;
				break;
			case Direction.Right:
				result = Vector2.right;
				break;
			}

			return result;
		}
		
		
		public static Direction GetOppositeDirection(Direction direction)
		{
			Direction result = Direction.None;

			switch(direction)
			{
			case Direction.Up:
				result = Direction.Down;
				break;
			case Direction.Down:
				result = Direction.Up;
				break;
			case Direction.Left:
				result = Direction.Right;
				break;
			case Direction.Right:
				result = Direction.Left;
				break;
			}
			
			return result;
		}
	}
}