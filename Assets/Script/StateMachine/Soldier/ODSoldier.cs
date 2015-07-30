using System;

/// <summary>
/// 奥丁
/// </summary>
using UnityEngine;
using System.Collections.Generic;

public class ODSoldier : HeroSoldier
{

	private float speed = 1.0f;
	private BaseBullet baseBullet;
	 
	override protected void OnShootOnEvent ()
	{
		GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [0].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);


		baseBullet = bullet.AddComponent<BaseBullet> (); 
		baseBullet.BattleAgent = this.BattleAgent;
		baseBullet.Speed = 1136.0f / 1000.0f;
		
		//从攻击点创建子弹
//		Vector3 pos = MapUtil.RelativeMovePosition (this.BattleAgent.BaseSprite.HitPoint, BattleAgent.GameObject.transform);
//		baseBullet.transform.position = new Vector3 (pos.x, pos.y, this.BattleAgent.GameObject.transform.position.z);


		baseBullet.transform.position = MapUtil.GetHitPointWorld (this.BattleAgent);
		
		AttackMessage message = new AttackMessage (this.BattleAgent, BattleAgent.Targets, 1);
		baseBullet.FlyToTarget (message);
	}

	override protected void OnUltShootOnEvent ()
	{


		List<BattleAgent> targets = BattleManager.SharedInstance.GetEnemyList ();
		for (int i = 0; i < targets.Count; i++) {

			List<BattleAgent> tlist=new List<BattleAgent>();
			tlist.Add(targets[i]);

			GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [1].Prefab);
			GameObject parent = StageManager.SharedInstance.EffectLayer; 
			GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
			baseBullet = bullet.AddComponent<BaseBullet> (); 
			baseBullet.BattleAgent = this.BattleAgent;
			baseBullet.transform.position = MapUtil.GetHitPointWorld(targets[i]);

			AttackMessage message = new AttackMessage (BattleAgent, tlist, 1);
			baseBullet.FlyToTargetRoot (message,0.5f);
		}
	}


//	override protected void OnUltShootOn ()
//	{
//		GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB[1].Prefab);
//		GameObject parent = StageManager.SharedInstance.EffectLayer; 
//		GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
//		baseBullet = bullet.AddComponent<BaseBullet> (); 
//		baseBullet.BattleAgent = this.BattleAgent;
//		baseBullet.transform.position = BattleAgent.GameObject.transform.position;
//		baseBullet.Speed = 1136.0f / 1000.0f;
//		
//		AttackMessage message = new AttackMessage (BattleAgent, BattleAgent.Targets, 1);
//		
//		baseBullet.FlyToTarget (message);
//	}
	

	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override public void AddSoundDemo ()
	{
		this.BattleAgent.BaseSprite.AddSound (StateId.Attack, "od_action_attack_02");
		this.BattleAgent.BaseSprite.AddSound (StateId.Ult, "od_action_ult_01");
		this.BattleAgent.BaseSprite.AddSound (StateId.Dead, "dead_od");

		this.BattleAgent.AddTimerDemo (new float[]{0.8f, 6});
		this.BattleAgent.AddSkillDemo (CooldownType.Attack, SkillData.testData [2]);
	}




}