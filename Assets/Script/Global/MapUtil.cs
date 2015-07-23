using System;

/// <summary>
/// 地图工具
/// </summary>
using UnityEngine;
using Vectrosity;


public class MapUtil
{

	
	public static readonly MapUtil GetInstance = new MapUtil ();
	
	
	public const float  MAX_ROW = 12.0f;
	
	public const float  MAX_COL = 16.0f;
	
	private float rowStep;

	public float RowStep {
		get {
			return rowStep;
		}
	}
	
	private float colStep;

	public float ColStep {
		get {
			return colStep;
		}
	}

 


	private MapUtil ()
	{
		colStep = Mathf.Round (Screen.width / MAX_COL);
		rowStep = Mathf.Round (Screen.height / MAX_ROW);
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





	/// <summary>
	/// 画出网格
	/// </summary>
	public void DrawGrid ()
	{
		
		
		for (int i = 0; i <= MAX_ROW; i++) {
			
			
			Vector2 a = new Vector2 (0, Screen.height - i * rowStep);
			Vector2 b = new Vector2 (Screen.width, Screen.height - i * rowStep);
			VectorLine line = new VectorLine ("row" + i, new Vector2[]{a,b}, null, 1.0f);
			line.SetColor (Color.green);
			
			line.Draw ();
		}
		
		
		for (int j = 0; j <=MAX_COL; j++) {
			
			Vector2 a = new Vector2 (j * colStep, 0);
			Vector2 b = new Vector2 (j * colStep, Screen.height);
			
			VectorLine line = new VectorLine ("col" + j, new Vector2[]{a,b}, null, 1.0f);
			line.SetColor (Color.green);
			
			line.Draw ();
		}

		
		
	}



	/// <summary>
	/// 屏幕坐标转地图坐标
	/// 地图坐标左上角0,0
	/// </summary>
	/// <returns>The to map.</returns>
	/// <param name="screen">Screen.</param>
	public Vector2 ScreenToMap (Vector2 screen)
	{

		float dx = Mathf.Ceil (screen.x / colStep);
		float dy = Mathf.Ceil (((float)Screen.height - screen.y) / rowStep);

		return new Vector2 (dy - 1, dx - 1);
	}

	/// <summary>
	/// 地图坐标转屏幕坐标
	/// 放置在方格正中间
	/// </summary>
	/// <returns>The to screen.</returns>
	/// <param name="map">Map.</param>
	public Vector2 MapToScreen (Vector2 map)
	{
		float dx = map.y * colStep + colStep * 0.5f;
		//float dy = Screen.height - map.x * rowStep + rowStep * 0.5f;

		float dy = (MAX_ROW - map.x - 1) * rowStep + 0.5f * rowStep;

		return new Vector2 (dx, dy);
	}


}