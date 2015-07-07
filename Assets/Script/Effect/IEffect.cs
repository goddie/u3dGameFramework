using System;
public interface IEffect
{
	/// <summary>
	/// 对实体增加效果
	/// </summary>
	/// <param name="target">Target.</param>
	void cast (BattleAgent target);

	/// <summary>
	/// 移除效果
	/// </summary>
	void reverse ();
}

