using System;

/// <summary>
/// 敌对控制器
/// </summary>
public class EnemySoldier : BaseSoldier
{

	public SoldierType AgentType {
		get;
		set;
	}

	public EnemySoldier ()
	{
		this.AgentType = SoldierType.Enemy;
	}
}