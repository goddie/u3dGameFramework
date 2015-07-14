using System;

/// <summary>
/// 英雄控制器
/// </summary>
public class HeroSoldier : BaseSoldier
{

	public SoldierType AgentType {
		get;
		set;
	}
	
	public HeroSoldier ()
	{
		this.AgentType = SoldierType.Hero;
	}


}
