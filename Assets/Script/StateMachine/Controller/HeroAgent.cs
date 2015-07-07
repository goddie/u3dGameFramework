using System;

/// <summary>
/// 英雄控制器
/// </summary>
public class HeroAgent : BattleAgent
{

	public AgentType AgentType {
		get;
		set;
	}
	
	public HeroAgent ()
	{
		this.AgentType = AgentType.Hero;
	}
}
