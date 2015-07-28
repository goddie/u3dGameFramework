using System.Collections.Generic;

public class SkillData
{
	/// <summary>
	/// 近战距离
	/// </summary>
	public const int MELEE = 2;

	/// <summary>
	/// BOSS近战距离
	/// </summary>
	public const int BOSS_MELEE = 5;

	/// <summary>
	/// 远程距离
	/// </summary>
	public const int RANGE = 10;

	/// <summary>
	/// 大招距离
	/// </summary>
	public const int ULT = 50;

	public static Dictionary <int,SkillData> testData = new Dictionary<int, SkillData> ()
	{
		{1,new SkillData(1,"近战攻击",MELEE,10)},
		{2,new SkillData(2,"远程攻击",RANGE,10)},
		{3,new SkillData(3,"中程攻击",BOSS_MELEE,10)},
		{20002,new SkillData(20002,"绿萼大招",ULT,10)},
		{20001,new SkillData(20001,"奥丁大招",ULT,10)},
		{20004,new SkillData(20004,"寒梦大招",ULT,10)},
		{20003,new SkillData(20003,"幕雪大招",ULT,10)},
		{20005,new SkillData(20005,"蓉蓉大招",ULT,10)},
		{20006,new SkillData(20006,"阿莫大招",ULT,10)}

	};


	public SkillData ()
	{

	}

	public SkillData (int id, string name, int range, int damage)
	{
		this.Id = id;
		this.Name = name;
		this.Range = range;
		this.Damage = damage;
	}

	public int Id {
		get;
		set;
	}

	public string Name {
		get;
		set;
	}

	public int Level {
		get;
		set;
	}

	public int Damage {
		get;
		set;
	}

	public int Range {
		get;
		set;
	}

	public int[] RangeLevel {
		get;
		set;
	}

	public int[] DamgeLevel {
		get;
		set;
	}
}
