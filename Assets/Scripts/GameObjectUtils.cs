using UnityEngine;

namespace TEDCore.Utils
{
	public static class GameObjectUtils
	{
		public static GameObject FindChild(this GameObject root, string name)
		{
			GameObject tempObject = null;
			Transform transformCache = root.transform;
			int childCount = root.transform.childCount;
			int cnt = 0;

			for(cnt = 0; cnt < childCount; cnt++)
			{
				tempObject = transformCache.GetChild(cnt).gameObject;

				if(name == tempObject.name)
				{
					return tempObject;
				}
			}

			for(cnt = 0; cnt < childCount; cnt++)
			{
				tempObject = transformCache.GetChild(cnt).gameObject;

				if(tempObject.transform.childCount > 0)
				{
					tempObject = tempObject.FindChild(name);

					if(null != tempObject)
					{
						return tempObject;
					}
				}
			}

			return null;
		}
	}
}