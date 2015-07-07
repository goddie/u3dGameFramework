using System;
/// <summary>
/// 伤害特效
/// </summary>
public class DamageEffect : IEffect
{
	private int damage = 100;
	public void cast (BattleAgent target)
	{
		//target.hp -= damage;
	}

	public void reverse ()
	{
		throw new NotImplementedException ();
	}
}