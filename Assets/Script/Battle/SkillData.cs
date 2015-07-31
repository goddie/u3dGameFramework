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
	public const int BOSS_MELEE = 3;

	/// <summary>
	/// 远程距离
	/// </summary>
	public const int RANGE = 8;

	/// <summary>
	/// 大招距离
	/// </summary>
	public const int ULT = 50;

	/// <summary>
	/// 浮空时间
	/// </summary>
	public const float FLOAT_TIME = 1.0f;

	public static Dictionary <int,SkillData> testData = new Dictionary<int, SkillData> ()
	{
		{1,new SkillData(1,"近战攻击",MELEE,10,0)},
		{2,new SkillData(2,"远程攻击",RANGE,10,0)},
		{3,new SkillData(3,"中程攻击",BOSS_MELEE,10,0)},
		{20002,new SkillData(20002,"绿萼大招",ULT,10,0)},
		{20001,new SkillData(20001,"奥丁大招",ULT,10,0)},
		{20004,new SkillData(20004,"寒梦大招",ULT,10,5.0f)},
		{20003,new SkillData(20003,"幕雪大招",ULT,10,0)},
		{20005,new SkillData(20005,"蓉蓉大招",ULT,10,0)},
		{20006,new SkillData(20006,"阿莫大招",ULT,10,0)}

	};


	public SkillData ()
	{

	}

	public SkillData (int id, string name, int range, int damage,float floatTime)
	{
		this.Id = id;
		this.Name = name;
		this.Range = range;
		this.Damage = damage;
		this.FloatTime = floatTime;
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

	/// <summary>
	/// 可以combo的时间
	/// </summary>
	/// <value>The float time.</value>
	public float FloatTime
	{
		get;
		set;
	}
}
