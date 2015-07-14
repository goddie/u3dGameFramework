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
	ShootOn,

	/// <summary>
	/// 发射大招
	/// </summary>
	UltShootOn
}
