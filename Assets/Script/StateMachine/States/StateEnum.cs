using System;

/// <summary>
/// 主状态
/// 角色同时只能有1个主状态
/// </summary>
public enum StateId
{
	/// <summary>
	/// The null state identifier.
	/// </summary>
	NullStateId = 0,

	/// <summary>
	/// 空闲
	/// </summary>
	Idle = 1,

	/// <summary>
	/// 行走
	/// </summary>
	Walk = 2,

	/// <summary>
	/// 跑步
	/// </summary>
	Run = 3,

	/// <summary>
	/// 攻击
	/// </summary>
	Attack = 4,

	/// <summary>
	/// 引导施法
	/// </summary>
	Cast = 5,

	/// <summary>
	/// 大招
	/// </summary>
	Ult = 6,

	/// <summary>
	/// 大招之前的蓄气
	/// </summary>
	UltWait,

	/// <summary>
	/// 受击
	/// </summary>
	Hit,


	/// <summary>
	/// 冰冻
	/// </summary>
	Frozen,

	/// <summary>
	/// 眩晕
	/// </summary>
	Vertigo,

	/// <summary>
	/// 吃惊
	/// </summary>
	Surprise,

	/// <summary>
	/// 潜入
	/// </summary>
	Steal,

	/// <summary>
	/// 死亡
	/// </summary>
	Dead,

	/// <summary>
	/// 起始Id
	/// </summary>
	SubStateStartId = 500,


	/// <summary>
	/// 变羊
	/// </summary>
	Sheep,

	/// <summary>
	/// 中毒
	/// </summary>
	Poisoning,

	/// <summary>
	/// 沉默
	/// </summary>
	Silence,

	/// <summary>
	/// 浮空
	/// </summary>
	Float
}
 
public enum StateType
{
	/// <summary>
	/// 未知
	/// </summary>
	UnKnown = -1,

	/// <summary>
	/// 主状态
	/// </summary>
	Major = 1,

	/// <summary>
	/// 子状态
	/// </summary>
	Sub = 2
}