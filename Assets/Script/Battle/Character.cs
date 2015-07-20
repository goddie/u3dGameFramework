using System;

/// <summary>
/// 角色信息
/// </summary>
using System.Collections.Generic;
using UnityEngine;


public class Character
{
	public Character ()
	{

	}

	public Character (int id, string name, int health, int attack, string prefab)
	{
		this.Id = id;
		this.Name = name;
		this.Health = health;
		this.Attack = attack;
		this.Prefab = prefab;
		this.AttackPoint = new Vector3(0,60,0);
		this.HitPoint = new Vector3(0,60,0);
	}


	public BattleAgent BattleAgent {
		get;
		set;
	}

	/// <summary>
	/// 编号
	/// </summary>

	public int Id {
		get;
		set;
	}

	/// <summary>
	/// 名称
	/// </summary>

	public string Name {
		get;
		set;
	}

	/// <summary>
	/// 生命
	/// </summary>

	public int Health {
		get;
		set;
	}

	/// <summary>
	/// 攻击力
	/// </summary>

	public int Attack {
		get;
		set;
	}

	/// <summary>
	/// 动画
	/// </summary>

	public string Prefab {
		get;
		set;
	}

	/// <summary>
	/// 攻击点
	/// 相对于物体原点的位移
	/// </summary>
	/// <value>The attack point.</value>
	public Vector2 AttackPoint
	{
		get;
		set;
	}


	/// <summary>
	/// 受击点
	/// 相对于物体原点的位移
	/// </summary>
	/// <value>The hit point.</value>
	public Vector2 HitPoint
	{
		get;
		set;
	}

}


