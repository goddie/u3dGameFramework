using System;
using System.Collections.Generic;


public static class TestData
{


	public static List<Character> charDB = new List<Character>()
	{
		new Character(0,"OD火柱",100,3,"Prefabs/fire",0),
		new Character(1,"OD龙卷风",100,3,"Prefabs/bigFire",0),
		new Character(2,"大招特效",100,3,"Prefabs/worldUlt",0),

		new Character(3,"LE箭矢",100,3,"Prefabs/arrow",0),
		new Character(4,"LE冰箭",100,3,"Prefabs/arrowUlt",0),

		new Character(5,"落位黄光",100,3,"Prefabs/down",0),
		new Character(6,"受击火花",100,3,"Prefabs/flash",0),


		new Character(7,"大招特效",100,3,"Prefabs/worldUlt",0),
		new Character(8,"大招遮罩",100,3,"Prefabs/mask",0),

		new Character(9,"RR治疗效果",100,3,"Prefabs/heal",0),
		new Character(10,"MX大招",100,3,"Prefabs/mxEffect",0),

		new Character(11,"黑风",100,3,"Prefabs/hf",1),
		new Character(12,"奥丁",120,5,"Prefabs/od",SkillData.MELEE),
		new Character(13,"绿萼",110,4,"Prefabs/le",SkillData.MELEE),
		new Character(14,"慕雪",110,4,"Prefabs/mx",1),
		new Character(15,"寒梦",110,4,"Prefabs/hm",SkillData.MELEE),
		new Character(16,"蓉蓉",110,4,"Prefabs/rr",SkillData.MELEE),
		new Character(17,"阿莫",110,4,"Prefabs/am",SkillData.MELEE),
		new Character(18,"combo特效",110,4,"Prefabs/combo",0),
		new Character(19,"combo特效",110,4,"Prefabs/comboNum",0)
	}
	;

}
