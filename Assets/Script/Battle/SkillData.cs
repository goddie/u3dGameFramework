using System.Collections.Generic;

public class SkillData
{

	public static List<SkillData> testSkill = new List<SkillData> ()
	{
		new SkillData(1,"近战攻击",2,10),
		new SkillData(2,"远程攻击",16,10),
		new SkillData(3,"绿萼大招",50,10),
		new SkillData(4,"奥丁大招",50,10),
		new SkillData(5,"寒梦大招",50,10),
		new SkillData(6,"幕雪大招",50,10)
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
