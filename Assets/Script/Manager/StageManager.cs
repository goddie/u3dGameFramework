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

	/// <summary>
	/// 敌人
	/// </summary>
	private static string NPC_LAYER = "NPC";

	/// <summary>
	/// 英雄
	/// </summary>
	private static string HERO_LAYER = "Hero";

	/// <summary>
	/// 特效
	/// </summary>
	private static string EFFECT_LAYER = "Effect";

	/// <summary>
	/// 背景
	/// </summary>
	private static string BG_LAYER = "BgLayer";

	/// <summary>
	/// 大招遮罩
	/// </summary>
	private static string MASK_LAYER = "MaskLayer";


	/// <summary>
	/// 决斗画面层
	/// </summary>
	private static string SLASH_LAYER = "Slash";

	/// <summary>
	/// Loading图
	/// </summary>
	private static string Loading_LAYER = "Loading";

	/// <summary>
	/// 获取图层
	/// </summary>
	/// <returns>The layer.</returns>
	/// <param name="layerName">Layer name.</param>
	private GameObject getLayer (string layerName)
	{
		return GameObject.Find (layerName);
	}

	private GameObject slashLayer;

	public GameObject SlashLayer {
		get {
			return slashLayer;
		}
	}

	private GameObject maskLayer;

	public GameObject MaskLayer {
		get {
			return maskLayer;
		}
	}

	/// <summary>
	/// 背景
	/// </summary>
	private GameObject bgLayer;

	public GameObject BgLayer {
		get {
			return bgLayer;
		}
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

	/// <summary>
	/// Loading层
	/// </summary>
	/// <value>The loading layer.</value>
	private GameObject loadingLayer;
	public GameObject LoadingLayer {
		get {
			return loadingLayer;
		}
	}



	void Awake ()
	{
//		Debug.Log ("Stage Awake");
		npcLayer = getLayer (NPC_LAYER);
		heroLayer = getLayer (HERO_LAYER);
		effectLayer = getLayer(EFFECT_LAYER);
		bgLayer = getLayer(BG_LAYER);
		maskLayer = getLayer(MASK_LAYER);
		slashLayer = getLayer(SLASH_LAYER);
		loadingLayer = getLayer(Loading_LAYER);
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
 

