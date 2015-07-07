using System;

/// <summary>
/// 敌对控制器
/// </summary>
public class EnemyAgent : BattleAgent
{

	public AgentType AgentType {
		get;
		set;
	}

	public EnemyAgent ()
	{
		this.AgentType = AgentType.Enemy;
	}
}