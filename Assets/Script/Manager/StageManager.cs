using System;
using UnityEngine;

/// <summary>
/// 场景管理
/// </summary>
public class StageManager : MonoBehaviour
{
	private static StageManager instance = null;

	public static StageManager SharedInstance {
		get {
			if (instance == null) {
				instance = MainComponentManager.AddMainComponent<StageManager> ();
			}
			return instance;
		}
	}

	private static string NPC_LAYER = "NPC";
	private static string HERO_LAYER = "Hero";
	private static string EFFECT_LAYER = "Effect";


	/// <summary>
	/// 获取图层
	/// </summary>
	/// <returns>The layer.</returns>
	/// <param name="layerName">Layer name.</param>
	private GameObject getLayer (string layerName)
	{
		return GameObject.Find (layerName);
	}

	/// <summary>
	/// 放敌人的层
	/// </summary>
	private GameObject npcLayer;

	public GameObject NpcLayer {
		get {
			return npcLayer;
		}
	}

	/// <summary>
	/// 放英雄的层
	/// </summary>
	private GameObject heroLayer;

	public GameObject HeroLayer {
		get {
			return heroLayer;
		}
	}


	/// <summary>
	/// 特效层
	/// </summary>
	private GameObject effectLayer;

	public GameObject EffectLayer {
		get {
			return effectLayer;
		}
	}

	void Awake ()
	{
//		Debug.Log ("Stage Awake");
		npcLayer = getLayer (NPC_LAYER);
		heroLayer = getLayer (HERO_LAYER);
		effectLayer = getLayer(EFFECT_LAYER);
	}

	void Start ()
	{
//		Debug.Log ("Stage Start");
	}


	/// <summary>
	/// 将对象放入场景中对应的层级
	/// </summary>
	/// <param name="child">Child.</param>
	/// <param name="layer">Layer.</param>
	public GameObject AddToStage (GameObject parentLayer, GameObject child)
	{
		return NGUITool.AddChild (parentLayer, child);
	}


	public void SetStagePosition (float x, float y)
	{

	}

	public void SetMapPosition (float x, float y)
	{

	}
}
 

