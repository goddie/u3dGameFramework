using System;

/// <summary>
/// 蓉蓉
/// </summary>
using System.Collections.Generic;
using UnityEngine;


public class RRSoldier : HeroSoldier
{
	private float speed = 1.0f;
	private BaseBullet baseBullet;
	
//	private List<Character> testDB = new List<Character> (){
//		new Character(200,"治疗效果",100,3,"Prefabs/heal",0)
//	};
	
	override protected void OnShootOn ()
	{

	}
	
	override protected void OnUltShootOn ()
	{
		List<BattleAgent> targets = BattleManager.SharedInstance.GetHeroList();
		for (int i = 0; i < targets.Count; i++) {
			
			List<BattleAgent> tlist=new List<BattleAgent>();
			tlist.Add(targets[i]);
			
			GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [9].Prefab);
			GameObject parent = StageManager.SharedInstance.EffectLayer; 
			GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);

			baseBullet = bullet.AddComponent<BaseBullet> (); 
			baseBullet.BattleAgent = this.BattleAgent;
			baseBullet.transform.position = MapUtil.GetHitPointWorld(targets[i]);
			
			AttackMessage message = new AttackMessage (BattleAgent, tlist, 1);
			baseBullet.FlyToTarget (message,0.3f);
		}
	}
	
	
	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override public void AddSoundDemo ()
	{
		this.BattleAgent.BaseSprite.AddSound (StateId.Attack, "attack_od");
		this.BattleAgent.BaseSprite.AddSound (StateId.Ult, "ult_od");
		this.BattleAgent.BaseSprite.AddSound (StateId.Dead, "dead_od");
		
		this.BattleAgent.AddTimerDemo (new float[]{1.5f, 6});
		this.BattleAgent.AddSkillDemo (CooldownType.Attack, SkillData.testData [2]);
	}

}