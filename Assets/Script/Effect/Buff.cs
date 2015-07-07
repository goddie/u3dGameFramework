using System;
using System.Collections.Generic;

/// <summary>
/// BUFF
/// </summary>
public abstract class Buff : IEffectContainer
{

	public void update ()
	{
		for (int i = 0; i < getEffects().Count; i++) {
			IEffect effect = getEffects () [i];
			//effect.cast (target);
		}   
	}

	public List<IEffect> getEffects ()
	{
		throw new NotImplementedException ();
	}

	public void cast (BattleAgent target)
	{
		throw new NotImplementedException ();
	}

	public void reverse ()
	{
		throw new NotImplementedException ();
	}
}

