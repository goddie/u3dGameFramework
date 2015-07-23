using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vectrosity;
using System;
 

public class Test : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		MapUtil.GetInstance.DrawGrid ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			
			//Debug.Log (Input.mousePosition);
			Vector2 m = Input.mousePosition; 
			
			Vector2 v = MapUtil.GetInstance.ScreenToMap (Input.mousePosition);

			Vector2 m2 = MapUtil.GetInstance.MapToScreen (v);

			Debug.Log (m + "," + v + "," + m2);

			//FillGrid (v);

			DrawPoint (m2);
			//Debug.Log (m + "," + v + " ," + DateTime.Now.Ticks + ",w:" + Screen.width + ",h:" + Screen.height);
		}
	}


 

	void AddGrid ()
	{

		float rowCount = 12.0f;
		float colCount = 16.0f;

		float colStep = Screen.width / colCount;
		float rowStep = Screen.height / rowCount;



//		Debug.Log (colStep + " " + rowStep);
//		Debug.Log (Screen.width + " " + Screen.height);


//
//		for (int i = 0; i < rowCount; i++) {
//			
//			
//			Vector2 a = new Vector2 (0, i * rowStep);
//			
//			Vector2 b = new Vector2 (Screen.width, i * rowStep);
//
//
//			Debug.DrawLine (a, b, Color.red);
//
//		}
//		
//		
//		for (int j = 0; j < 16; j++) {
//			
//			Vector2 a = new Vector2 (j * colStep, 0);
//			Vector2 b = new Vector2 (j * colStep, Screen.height);
//			
//			Debug.DrawLine (a, b, Color.red);
//		}


		for (int i = 0; i < rowCount; i++) {


			Vector2 a = new Vector2 (0, i * rowStep);

			Vector2 b = new Vector2 (Screen.width, i * rowStep);

			VectorLine line = new VectorLine ("row" + i, new Vector2[]{a,b}, null, 1.0f);
			line.SetColor (Color.green);

			line.Draw ();
		}


		for (int j = 0; j < 16; j++) {

			Vector2 a = new Vector2 (j * colStep, 0);
			Vector2 b = new Vector2 (j * colStep, Screen.height);
			
			VectorLine line = new VectorLine ("col" + j, new Vector2[]{a,b}, null, 1.0f);
			line.SetColor (Color.green);

			line.Draw ();
		}


	}

	/// <summary>
	/// Fills the grid.
	/// </summary>
	void FillGrid (Vector2 map)
	{
		Vector2 screen = MapUtil.GetInstance.MapToScreen (map);

		float x = screen.x - MapUtil.GetInstance.ColStep * 0.5f;
		float y = screen.y - MapUtil.GetInstance.RowStep * 0.5f;
		float w = MapUtil.GetInstance.ColStep;
		float h = MapUtil.GetInstance.RowStep;

		CreateRect (x, y, w, h, Color.red);
	}

	void GetRout ()
	{



		route_pt[] result = null;
		// result_pt是一个简单类，它只有两个成员变量：int x和int y。
		// 参数说明：map是一个二维数组，具体见程序注释
		AStarRoute asr = new AStarRoute (new int[100, 100], 0, 0, 5, 5);

		try {
			result = asr.getResult ();
		} catch (Exception ex) {

		}

		Debug.Log (result);
	}


	/// <summary>
	/// 绘制矩形
	/// </summary>
	private void CreateRect (float posX, float posY, float width, float height, Color color)
	{
		
		VectorLine squareLine = new VectorLine ("Square", new Vector2[8], null, 1.0f, LineType.Discrete, Joins.Weld);
		
		squareLine.MakeRect (new Rect (posX, posY, width, height));
		
		squareLine.SetColor (color);
		
		squareLine.Draw ();
		
	}


	private void DrawPoint (Vector2 screen)
	{
		VectorPoints p = new VectorPoints ("name", new Vector2[]{screen}, null, 5.0f);
		p.SetColor (Color.red);
		p.Draw ();
	}
	

}
