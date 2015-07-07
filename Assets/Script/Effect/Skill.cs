using System;

/// <summary>
/// 技能
/// </summary>
using System.Collections.Generic;


public abstract class Skill : IEffectContainer
{
	public void spellTo (BattleAgent target)
	{

		for (int i = 0; i < getEffects().Count; i++) {
			IEffect effect = getEffects () [i];
			effect.cast (target);
		}
	}

	#region IEffectContainer implementation
	public virtual List<IEffect> getEffects ()
	{
		return null;
	}
	#endregion
	#region IEffect implementation
	public virtual void cast (BattleAgent target)
	{
 		
	}
	public virtual void reverse ()
	{
 		
	}
	#endregion
}