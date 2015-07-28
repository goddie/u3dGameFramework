using System;
using System.Collections.Generic;
using UnityEngine;


public class LESoldier : HeroSoldier
{
	private float speed = 1.0f;
	
	private BaseBullet baseBullet;
	
 
	 	
	/// <summary>
	/// Raises the shoot on event.
	/// </summary>
	override protected void OnShootOnEvent ()
	{
		GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB[3].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
		baseBullet = bullet.AddComponent<BaseBullet> (); 
		baseBullet.BattleAgent = this.BattleAgent;
		//baseBullet.transform.position  = battleAgent.GameObject.transform.position;
		
//		Vector3 pos = MapUtil.RelativeMovePosition (BattleAgent.BaseSprite.HitPoint, BattleAgent.GameObject.transform);
//		baseBullet.transform.position = new Vector3 (pos.x, pos.y, BattleAgent.GameObject.transform.position.z);


		baseBullet.transform.position = MapUtil.GetHitPointWorld(this.BattleAgent);

		
		baseBullet.Speed = 1136.0f / 1000.0f;
		
		AttackMessage message = new AttackMessage (BattleAgent, BattleAgent.Targets, 1);
		
		baseBullet.FlyToTarget (message);
	}
	
 
	
	
	override protected void OnUltShootOnEvent ()
	{
		GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB[4].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
		baseBullet = bullet.AddComponent<BaseBullet> (); 
		baseBullet.BattleAgent = this.BattleAgent;
		//baseBullet.transform.position  = battleAgent.GameObject.transform.position;
		baseBullet.Speed = 1136.0f / 1000.0f;
		Vector3 pos = MapUtil.GetHitPointWorld(BattleAgent.Targets[0]);

		baseBullet.transform.position = pos;

		baseBullet.FaceTo = this.BattleAgent.BaseSprite.FaceTo;
		
		AttackMessage message = new AttackMessage (BattleAgent, BattleAgent.Targets, 1);
		
		//baseBullet.FlyToTargetOutScreen (message);
		baseBullet.FlyToTarget (message);
	}


	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override public void AddSoundDemo ()
	{
		BattleAgent.BaseSprite.AddSound (StateId.Attack, "attack_le");
		BattleAgent.BaseSprite.AddSound (StateId.Ult, "ult_le");
		BattleAgent.BaseSprite.AddSound (StateId.Dead, "dead_le");

		BattleAgent.AddTimerDemo (new float[]{1.0f, 6.0f});
		this.BattleAgent.AddSkillDemo (CooldownType.Attack, SkillData.testData [2]);
	}
	
}
