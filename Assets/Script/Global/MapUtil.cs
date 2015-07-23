using System;


using UnityEngine;
using Vectrosity;

/// <summary>
/// 地图工具
/// </summary>

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

	private int[,] mapMatrix;

	private MapUtil ()
	{
		colStep = Mathf.Round (Screen.width / MAX_COL);
		rowStep = Mathf.Round (Screen.height / MAX_ROW);

		InitMapMatrix ();
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
			line.SetColor (Color.red);
			
			line.Draw ();
		}
		
		
		for (int j = 0; j <=MAX_COL; j++) {
			
			Vector2 a = new Vector2 (j * colStep, 0);
			Vector2 b = new Vector2 (j * colStep, Screen.height);
			
			VectorLine line = new VectorLine ("col" + j, new Vector2[]{a,b}, null, 1.0f);
			line.SetColor (Color.red);
			
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



	/// <summary>
	/// 绘制矩形
	/// </summary>
	public void CreateRect (float posX, float posY, float width, float height, Color color)
	{
		
		VectorLine squareLine = new VectorLine ("Square", new Vector2[8], null, 1.0f, LineType.Discrete, Joins.Weld);
		
		squareLine.MakeRect (new Rect (posX, posY, width, height));
		
		squareLine.SetColor (color);
		
		squareLine.Draw ();
		
	}
	
	public void DrawPoint (Vector2 screen)
	{
		VectorPoints p = new VectorPoints ("name", new Vector2[]{screen}, null, 5.0f);
		p.SetColor (Color.red);
		p.Draw ();
	}


	public void DrawMapPoint (Vector2[] map)
	{
		Vector2[] screen = new Vector2[map.Length];

		for (int i = 0; i < map.Length; i++) {
			screen [i] = MapToScreen (map [i]);
		}


		VectorPoints p = new VectorPoints ("name", screen, null, 5.0f);
		p.SetColor (Color.red);
		p.Draw ();
	}
	
	
	/// <summary>
	/// Fills the grid.
	/// </summary>
	public void FillGrid (Vector2 map)
	{
		Vector2 screen = MapUtil.GetInstance.MapToScreen (map);
		
		float x = screen.x - MapUtil.GetInstance.ColStep * 0.5f;
		float y = screen.y - MapUtil.GetInstance.RowStep * 0.5f;
		float w = MapUtil.GetInstance.ColStep;
		float h = MapUtil.GetInstance.RowStep;
		
		CreateRect (x, y, w, h, Color.red);
	}


	public int[,] GetMapMatrix ()
	{
		return mapMatrix;
	}


	public void InitMapMatrix ()
	{

		mapMatrix = new int[(int)MAX_ROW, (int)MAX_COL];

		for (int i = 0; i < MAX_ROW; i++) {
			for (int j = 0; j < MAX_COL; j++) {
				mapMatrix [i, j] = 0;
			}
		}

	}


	/// <summary>
	/// 地图坐标转世界坐标
	/// </summary>
	/// <returns>The to world.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public Vector3 MapToWorld (float x, float y)
	{
		Vector2 screenPos = MapUtil.GetInstance.MapToScreen (new Vector2 (x, y));
		
		Vector3 screenPos3 = new Vector3 (screenPos.x, screenPos.y, Camera.main.farClipPlane);
		
		//battleAgent.GameObject.transform.position = screenPos;
		
		//screenPoint.z = 10.0f; //distance of the plane from the camera
		Vector3 pos = Camera.main.ScreenToWorldPoint (screenPos3);

		return pos;
	}
}