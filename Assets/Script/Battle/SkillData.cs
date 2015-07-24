using System.Collections.Generic;

public class SkillData
{

	public static Dictionary <int,SkillData> testData = new Dictionary<int, SkillData> ()
	{
		{1,new SkillData(1,"近战攻击",2,10)},
		{2,new SkillData(2,"远程攻击",8,10)},
		{3,new SkillData(3,"绿萼大招",50,10)},
		{4,new SkillData(4,"奥丁大招",50,10)},
		{5,new SkillData(5,"寒梦大招",50,10)},
		{6,new SkillData(6,"幕雪大招",50,10)},
		{7,new SkillData(7,"近战攻击",4,10)}
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
