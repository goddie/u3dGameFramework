using System;
public class MXSoldier : HeroSoldier
{
	public MXSoldier ()
	{
	}


	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override public void AddSoundDemo ()
	{
		BattleAgent.BaseSprite.AddSound (StateId.Attack, "attack_mx");
		BattleAgent.BaseSprite.AddSound (StateId.Ult, "ult_mx");
		BattleAgent.BaseSprite.AddSound (StateId.Dead, "dead_mx");

		BattleAgent.AddTimerDemo (new float[]{1, 6});
		this.BattleAgent.AddSkillDemo (CooldownType.Attack, SkillData.testData [3]);
	}

 
	override protected void OnShootOn ()
	{
		
	}
	
 
	
	override protected void OnUltShootOn ()
	{
		
	}


}
