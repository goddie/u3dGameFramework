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
	
	private List<Character> testDB = new List<Character> (){
		new Character(200,"治疗效果",100,3,"Prefabs/heal",0)
	};
	
	
	
	override protected void OnShootOn ()
	{
		GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [0].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
		baseBullet = bullet.AddComponent<BaseBullet> (); 
		baseBullet.BattleAgent = this.BattleAgent;
		//baseBullet.transform.position  = battleAgent.GameObject.transform.position;
		baseBullet.Speed = 1136.0f / 1000.0f;
		
		
		Vector3 pos = MapUtil.RelativeMovePosition (BattleAgent.BaseSprite.HitPoint, BattleAgent.GameObject.transform);
		baseBullet.transform.position = new Vector3 (pos.x, pos.y, BattleAgent.GameObject.transform.position.z);
		
		AttackMessage message = new AttackMessage (BattleAgent, BattleAgent.Targets, 1);
		
		baseBullet.FlyToTarget (message);
	}
	
	
	
	
	override protected void OnUltShootOn ()
	{
		GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [1].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
		baseBullet = bullet.AddComponent<BaseBullet> (); 
		baseBullet.BattleAgent = this.BattleAgent;
		baseBullet.transform.position = BattleAgent.GameObject.transform.position;
		baseBullet.Speed = 1136.0f / 1000.0f;
		
		AttackMessage message = new AttackMessage (BattleAgent, BattleAgent.Targets, 1);
		
		baseBullet.FlyToTarget (message);
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