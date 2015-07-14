using System;

/// <summary>
/// 角色信息
/// </summary>
using System.Collections.Generic;


public class Character
{
	public Character ()
	{
	}

	public Character (int id, string name, int health, int attack, string prefab)
	{
		this.id = id;
		this.name = name;
		this.health = health;
		this.attack = attack;
		this.prefab = prefab;
	}

	private BattleAgent battleAgent;

	public BattleAgent BattleAgent {
		get {
			return battleAgent;
		}
		set {
			battleAgent = value;
		}
	}

	/// <summary>
	/// 编号
	/// </summary>
	private int id;

	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	/// <summary>
	/// 名称
	/// </summary>
	private string name;

	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	/// <summary>
	/// 生命
	/// </summary>
	private int health;

	public int Health {
		get {
			return health;
		}
		set {
			health = value;
		}
	}

	/// <summary>
	/// 攻击力
	/// </summary>
	private int attack;

	public int Attack {
		get {
			return attack;
		}
		set {
			attack = value;
		}
	}

	/// <summary>
	/// 动画
	/// </summary>
	private string prefab;

	public string Prefab {
		get {
			return prefab;
		}
		set {
			prefab = value;
		}
	}

}


