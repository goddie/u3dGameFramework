using System;

/// <summary>
/// 攻击防御变0
/// </summary>
public class PropertyChangeEffect : IEffect
{
	public void cast (BattleAgent target)
	{
//		target.atk = 0;
//		target.def = 0;
//		target.speed = 50;
	}

	public void reverse ()
	{
		throw new NotImplementedException ();
	}
}
