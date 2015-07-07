using System;
/// <summary>
/// 被动技能
/// </summary>
public abstract class BuffSkill : Buff
{
	public void install ()
	{
		foreach (IEffect effect in getEffects()) {
			//effect.cast (target);
		}    
	}
	
	public void unstall ()
	{
		foreach (IEffect effect in getEffects()) {
			//effect.reverse (target);
		}    
	}
}
