using System;
using System.Collections.Generic;
using UnityEngine;


public class HMSoldier : HeroSoldier
{
	private float speed = 1.0f;
	
	private BaseBullet baseBullet;
	
	private List<Character> testDB = new List<Character> (){
		new Character(300,"光球",100,3,"Prefabs/ball")
	};
	
	/// <summary>
	/// Raises the shoot on event.
	/// </summary>
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
		
		baseBullet.BornToTarget (message);
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
		BattleAgent.BaseSprite.AddSound (StateId.Attack, "attack_hm");
		BattleAgent.BaseSprite.AddSound (StateId.Ult, "ult_hm");
		BattleAgent.BaseSprite.AddSound (StateId.Dead, "dead_hm");

		BattleAgent.AddTimerDemo (new float[]{1.5f, 6.0f});
	}


}
