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
	/// 动画暂停
	/// </summary>
	StateStop = 11,

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
	UltPrepare = 103,

	/// <summary>
	/// 大招完成
	/// </summary>
	UltEnd=104,

	/// <summary>
	/// 大招击中
	/// 浮空效果
	/// </summary>
	UltAttackOn=105,

	/// <summary>
	/// 浮空状态
	/// </summary>
	FloatOn = 106,

	/// <summary>
	/// 浮空结束
	/// </summary>
	FloatEnd = 107,

	/// <summary>
	/// 连击结束
	/// </summary>
	ComboEnd=108
}

/// <summary>
/// 冷却计时器种类
/// </summary>
public enum CooldownType
{
	/// <summary>
	/// 普通攻击
	/// </summary>
	Attack=1,

	/// <summary>
	/// 大招
	/// </summary>
	Ult,

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
	/// 计算位置和路径
	/// </summary>
	Update
}


/// <summary>
/// 面向方向
/// </summary>
public enum FaceTo
{
	Left=1,
	Right=2
}
