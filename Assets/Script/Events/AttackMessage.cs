using System;
using System.Collections.Generic;

/// <summary>
/// 攻击事件参数
/// </summary>
public class AttackMessage
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AttackMessage"/> class.
	/// </summary>
	/// <param name="sender">施法者</param>
	/// <param name="targets">攻击目标</param>
	/// <param name="spellId">技能</param>
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
