using System;

/// <summary>
/// 地图工具
/// </summary>
using UnityEngine;


public class MapUtil
{
	public MapUtil ()
	{
	}



	/// <summary>
	/// 相对位移后position
	/// </summary>
	/// <returns>The move.</returns>
	/// <param name="movement">Movement.</param>
	/// <param name="trans">Trans.</param>
	public static Vector3 RelativeMovePosition (Vector3 movement, Transform trans)
	{
 
		Vector3 origin = new Vector3 (trans.localPosition.x, trans.localPosition.y, trans.localPosition.z);
		//目标受击点
		Vector3 targetLocal = new Vector3 (origin.x + movement.x, origin.y + movement.y, origin.z + movement.z);
		
		Vector3 targetScreen = trans.TransformVector (targetLocal);

		return targetScreen;
	}
}