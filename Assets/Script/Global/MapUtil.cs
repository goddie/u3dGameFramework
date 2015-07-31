using System;


using UnityEngine;
using Vectrosity;

/// <summary>
/// 地图工具
/// </summary>
using System.Collections.Generic;

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
	/// 物体的受击点世界坐标
	/// </summary>
	/// <returns>The hit point world.</returns>
	/// <param name="agent">Agent.</param>
	public static Vector3 GetHitPointWorld (BattleAgent agent)
	{
		//return agent.GameObject.transform.position;
		//物体位置
		Vector3 local = agent.GameObject.transform.localPosition;
		//受击点相对坐标
		Vector3 hitLocal = local + agent.BaseSprite.HitPoint;
	
		//受击点世界坐标
		Vector3 hitScreen = StageManager.SharedInstance.HeroLayer.gameObject.transform.TransformVector (hitLocal);
//
		Vector3 hitScreen2 = new Vector3 (hitScreen.x, hitScreen.y, Camera.main.farClipPlane);

		//screenPoint.z = 10.0f; //distance of the plane from the camera
		//Vector3 pos = Camera.main.ScreenToWorldPoint (hitScreen2);

		return hitScreen2;
	}

	/// <summary>
	/// 物体相对位移后的某点
	/// </summary>
	/// <returns>相对位移坐标</returns>
	/// <param name="agent">Agent.</param>
	/// <param name="delta">Delta.</param>
	public static Vector3 GetDeltaPointWorld(BattleAgent agent,Vector3 delta)
	{
		//受击点相对坐标
		Vector3 hitLocal = delta + agent.BaseSprite.HitPoint;
		
		//受击点世界坐标
		Vector3 hitScreen = StageManager.SharedInstance.HeroLayer.gameObject.transform.TransformVector (hitLocal);
		//
		Vector3 hitScreen2 = new Vector3 (hitScreen.x, hitScreen.y, Camera.main.farClipPlane);

		return hitScreen2;
	}


	/// <summary>
	/// 获取屏幕外的一点
	/// 以屏幕对角线为半径画圆与该射线碰撞
	/// </summary>
	/// <returns>The out point.</returns>
	/// <param name="start">攻击点</param>
	/// <param name="end">受击点</param>
	public Vector3 GetOutPoint(Vector3 start,Vector3 end)
	{
		return Vector3.zero;
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
	/// 屏幕坐标转地图坐标
	/// 地图坐标左上角0,0
	/// </summary>
	/// <returns>The to map.</returns>
	/// <param name="screen">Screen.</param>
	public Vector2 ScreenToMap (Vector2 screen)
	{
		
		float dx = Mathf.Ceil (screen.x / colStep);
		float dy = Mathf.Ceil (((float)Screen.height - screen.y) / rowStep);
		
		return new Vector2 (dx - 1, dy - 1);
	}
	
	/// <summary>
	/// 地图坐标转屏幕坐标
	/// 放置在方格正中间
	/// </summary>
	/// <returns>The to screen.</returns>
	/// <param name="map">Map.</param>
	public Vector2 MapToScreen (Vector2 map)
	{
		float dx = map.x * colStep + colStep * 0.5f;
		//float dy = Screen.height - map.x * rowStep + rowStep * 0.5f;
		
		float dy = (MAX_ROW - map.y - 1) * rowStep + 0.5f * rowStep;
		
		return new Vector2 (dx, dy);
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



	/// <summary>
	/// Worlds to map.
	/// </summary>
	/// <returns>The to map.</returns>
	/// <param name="pos">Position.</param>
	public Vector2 WorldToMap (Vector3 pos)
	{


		Vector3 screenPos = Camera.main.WorldToScreenPoint (pos); 
		Vector2 screenPos2 = new Vector2 (screenPos.x, screenPos.y);

		Vector2 map = ScreenToMap (screenPos2);


		return map;
	}


	/// <summary>
	/// 优先同一行站位攻击
	/// </summary>
	/// <returns>The attack position same row.</returns>
	/// <param name="startAgent">Start agent.</param>
	/// <param name="targetAgent">Target agent.</param>
	/// <param name="range">Range.</param>
	public Vector2 GetAttackPosSameRow (BattleAgent startAgent, BattleAgent targetAgent, int range)
	{
		
		Vector2 start = startAgent.MapPos;
		Vector2 target = targetAgent.MapPos;
		
		List<Vector2> list = new List<Vector2> ();
		
		//可攻击点
		for (int i = 0; i < MAX_ROW; i++) {
			for (int j = 0; j < MAX_COL; j++) {
				
				Vector2 a = new Vector2 (i, j);
				float dis = Vector2.Distance (a, target);
				//同一列也不要 不要占屏幕第一行，最后两行
				if (dis <= range && i != target.x && j < 9 && j > 1) {
					list.Add (a);
				}
				
			}
		}
		
		//有人占据的点
		List<Vector2> list2 = GetUsedPos ();
		
		//移除有人占据的点
		for (int j = 0; j < list2.Count; j++) {
			
			if (list.Contains (list2 [j])) {
				list.Remove (list2 [j]);
			}
		}

		List<Vector2> sameRow = new List<Vector2> ();
		for (int i = 0; i < list.Count; i++) {

			if (list [i].y == target.y) {
				sameRow.Add (list [i]);
			}
		}

		if (sameRow.Count == 0) {
			for (int i = 0; i < list.Count; i++) {
				
				if (list [i].y + 1 == target.y || list [i].y - 1 == target.y) {
					sameRow.Add (list [i]);
				}
			}
		}

		int idx = sameRow.Count;
		int next = UnityEngine.Random.Range (0, idx - 1);
		return sameRow [next];
		
	}


	/// <summary>
	/// 根据目标点算出适合攻击的位置按权重从大到小排。
	/// 距离越大，权重越高。
	/// </summary>
	/// <returns>The attack position.</returns>
	/// <param name="map">Map.</param>
	/// <param name="range">Range.</param>
	public Vector2 GetAttackPos (BattleAgent startAgent, BattleAgent targetAgent, int range)
	{
		
		Vector2 start = startAgent.MapPos;
		Vector2 target = targetAgent.MapPos;
		
		List<Vector2> list = new List<Vector2> ();

		//可攻击点
		for (int i = 0; i < MAX_ROW; i++) {
			for (int j = 0; j < MAX_COL; j++) {
				
				Vector2 a = new Vector2 (i, j);
				float dis = Vector2.Distance (a, target);
				//同一列也不要 不要占屏幕第一行，最后两行
				if (dis <= range && i != target.x && j < 9 && j > 1) {
					list.Add (a);
				}
				
			}
		}

		if (list.Count==0) {
			Debug.Log("list count=0");
			return startAgent.MapPos;
		}

		//有人占据的点
		List<Vector2> list2 = GetUsedPos ();

		//移除有人占据的点
		for (int j = 0; j < list2.Count; j++) {
			
			if (list.Contains (list2 [j])) {
				list.Remove (list2 [j]);
			}
		}
		
		List<float> disList = new List<float> ();
		for (int i = 0; i < list.Count; i++) {
			float d = Vector2.Distance (start, list [i]);
			disList.Add (d);
		}
		float min = Mathf.Min (disList.ToArray ());

		//距离攻击方最近的可攻击点
		return list [disList.IndexOf (min)];
		
	}


	/// <summary>
	/// 根据目标点算出适合攻击的位置按权重从大到小排。
	/// 距离越大，权重越高。
	/// </summary>
	/// <returns>The attack position.</returns>
	/// <param name="map">Map.</param>
	/// <param name="range">Range.</param>
	public Vector2 GetRangeAttackPos (BattleAgent startAgent, BattleAgent targetAgent, int range)
	{

		Vector2 start = startAgent.MapPos;
		Vector2 target = targetAgent.MapPos;

		List<Vector2> list = new List<Vector2> ();

		for (int i = 0; i < MAX_ROW; i++) {
			for (int j = 0; j < MAX_COL; j++) {

				Vector2 a = new Vector2 (i, j);
				float dis = Vector2.Distance (a, target);
				//同一列也不要
				if (dis <= range && dis > startAgent.Character.GuardRange && i != target.x && j < 9 && j > 1) {
					list.Add (a);
				}

			}
		}

		List<Vector2> list2 = GetUsedPos ();
		for (int j = 0; j < list2.Count; j++) {
			
			if (list.Contains (list2 [j])) {
				list.Remove (list2 [j]);
			}
		}

		//

		float tmp = Vector2.Distance (startAgent.MapPos, targetAgent.MapPos);


		int idx = list.Count;
		
		int next = UnityEngine.Random.Range (0, idx - 1);
		
		return list [next];


		//如果近身，或者靠近屏幕边缘就随机到下一个位置
//		if (tmp < startAgent.Character.GuardRange && 
//			(startAgent.MapPos.x < 1 || startAgent.MapPos.x > MapUtil.MAX_COL - 1)) {
//
//			int idx = list.Count;
//
//			int next = UnityEngine.Random.Range (0, idx - 1);
//
//			return list [next];
//
//		} else {
//			List<float> disList = new List<float> ();
//			for (int i = 0; i < list.Count; i++) {
//				float d = Vector2.Distance (start, list [i]);
//				disList.Add (d);
//			}
//			float min = Mathf.Min (disList.ToArray ());
//			return list [disList.IndexOf (min)];
//		}





	}


	/// <summary>
	/// 地图上已经占据的位置
	/// </summary>
	/// <returns>The used position.</returns>
	public List<Vector2> GetUsedPos ()
	{
		List<BattleAgent> agent = BattleManager.SharedInstance.GetAgentList ();

		List<Vector2> list = new List<Vector2> ();

		for (int i = 0; i < agent.Count; i++) {

			list.Add (agent [i].MapPos);
		}

		return list;
	}

	/// <summary>
	/// 两点的夹角角度
	/// 主角可能是敌人，也可能是我方。
	/// </summary>
	/// <param name="pos1">主角.</param>
	/// <param name="pos2">目标点.</param>
	public static float MapAngel(Vector2 pos1,Vector2 pos2)
	{
		float dx = pos1.x - pos2.x;
		float dy = pos1.y - pos2.y;
		
		float ang = Mathf.Atan2 (dy, dx);
		float ang2 = ang * (180 / Mathf.PI);

		return ang2;
	}
}









