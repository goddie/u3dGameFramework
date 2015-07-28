using UnityEngine;
using System.Collections;

/// <summary>
/// 与兵种有关的事件
/// </summary>
public static class SoldierEvent
{
	/// <summary>
	/// 攻击
	/// </summary>
	public static string ATTACK = "soldier_attack";

	/// <summary>
	/// 战斗信息
	/// </summary>
	public static string BATTLE_MESSAGE = "soldier_battle_message";


	/// <summary>
	/// 被击中
	/// </summary>
	public static string HIT = "soldier_hit";

	/// <summary>
	/// 被连击中
	/// </summary>
	public static string COMBO_HIT = "soldier_combo_hit";


	/// <summary>
	/// itween 回调
	/// </summary>
	public static string CALLBACK = "soldier_callback";


	/// <summary>
	/// 大招引导特效
	/// </summary>
	public static string ULT_LOAD = "soldier_ulr_load";

	/// <summary>
	/// 击中浮空
	/// </summary>
	public static string HIT_FLOAT = "soldier_hit_float";

}
