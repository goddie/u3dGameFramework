using System;
using System.Collections.Generic;


public class AttackMessage
{
	public AttackMessage (BattleAgent sender, List<BattleAgent> targets, int spellId)
	{
		this.Sender = sender;
		this.Targets = targets;
		this.SpellId = spellId;
	}

	/// <summary>
	/// 施法者
	/// </summary>
	/// <value>The attacker.</value>
	public BattleAgent Sender {
		get;
		set;
	}

	/// <summary>
	/// 技能
	/// </summary>
	/// <value>The spell identifier.</value>
	public int SpellId {
		get;
		set;
	}

	/// <summary>
	/// 攻击目标
	/// </summary>
	/// <value>The targets.</value>
	public List<BattleAgent> Targets {
		get;
		set;
	}

}
