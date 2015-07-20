using System;
public class EnumUtil
{
}

public enum KeyEventId
{
	/// <summary>
	/// 动画开始
	/// </summary>
	StateStart = 0,

	/// <summary>
	/// 动画结束
	/// </summary>
	StateEnd = 10,

	/// <summary>
	/// 攻击生效
	/// </summary>
	AttackOn = 100,

	/// <summary>
	/// 发射出子弹
	/// </summary>
	ShootOn = 101,

	/// <summary>
	/// 发射大招
	/// </summary>
	UltShootOn = 102,

	/// <summary>
	/// 大招准备
	/// </summary>
	UltPrepare = 103
}

/// <summary>
/// 冷却计时器种类
/// </summary>
public enum CooldownType
{
	/// <summary>
	/// 普通攻击
	/// </summary>
	Attack,

	/// <summary>
	/// 技能1
	/// </summary>
	Spell1,

	/// <summary>
	/// 技能2
	/// </summary>
	Spell2,

	/// <summary>
	/// 技能3
	/// </summary>
	Spell3,

	/// <summary>
	/// 大招
	/// </summary>
	Ult
}
