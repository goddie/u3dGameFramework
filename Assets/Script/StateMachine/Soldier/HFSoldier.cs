using System;

/// <summary>
/// 黑风
/// </summary>
public class HFSoldier : EnemySoldier
{


	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override public void AddSoundDemo ()
	{
		this.BattleAgent.BaseSprite.AddSound (StateId.Attack, "attack_hf");
		this.BattleAgent.BaseSprite.AddSound (StateId.Dead, "dead_hf");
		this.BattleAgent.AddTimerDemo (new float[]{1.0f, 6.0f});

		this.BattleAgent.AddSkillDemo (CooldownType.Attack, SkillData.testData [1]);

	}


	
	/// <summary>
	/// Raises the shoot on event.
	/// </summary>
	override protected void OnShootOn ()
	{
		
	}
 
	
	override protected void OnUltShootOn ()
	{
		
	}
}
