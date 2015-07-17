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
	/// itween 回调
	/// </summary>
	public static string CALLBACK = "soldier_callback";


	/// <summary>
	/// 大招引导特效
	/// </summary>
	public static string ULT_LOAD = "ulr_load";

}
