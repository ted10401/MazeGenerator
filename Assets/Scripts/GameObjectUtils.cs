using UnityEngine;

namespace TEDCore.Utils
{
	public static class GameObjectUtils
	{
		public static GameObject FindChild(this GameObject root, string name)
		{
			GameObject temp = null;
			Transform transformCache = root.transform;
			int childCount = root.transform.childCount;
			int cnt = 0;

			for(cnt = 0; cnt < childCount; cnt++)
			{
				temp = transformCache.GetChild(cnt).gameObject;

				if(name == temp.name)
				{
					return temp;
				}
			}

			for(cnt = 0; cnt < childCount; cnt++)
			{
				temp = transformCache.GetChild(cnt).gameObject;

				if(transformCache.childCount > 0)
				{
					temp = temp.FindChild(name);

					if(null != temp)
					{
						return temp;
					}
				}
			}

			return null;
		}
	}
}