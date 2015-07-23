using System;
using System.Collections.Generic;
using UnityEngine;


public class LESoldier : HeroSoldier
{
	private float speed = 1.0f;
	
	private BaseBullet baseBullet;
	
	private List<Character> testDB = new List<Character> (){
		new Character(300,"箭矢",100,3,"Prefabs/arrow"),
		new Character(301,"冰箭",100,3,"Prefabs/arrowUlt")
	};
	
	public override void TriggerKeyEvent (KeyEventId keyId)
	{
		
		if (keyId == KeyEventId.AttackOn) {
			
			//Debug.Log("OD AttackOn");
			//			for (int i = 0; i < battleAgent.Targets.Count; i++) {
			//
			//				BattleAgent t = battleAgent.Targets [i];
			//				
			//				t.dispatchEvent (SoldierEvent.HIT, message);
			//			}
		}
		
		if (keyId == KeyEventId.ShootOn) {
			
			
			
			
			
			GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [0].Prefab);
			GameObject parent = StageManager.SharedInstance.EffectLayer; 
			GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
			baseBullet = bullet.AddComponent<BaseBullet> (); 
			baseBullet.BattleAgent = this.BattleAgent;
			//baseBullet.transform.position  = battleAgent.GameObject.transform.position;

			Vector3 pos = MapUtil.RelativeMovePosition (BattleAgent.BaseSprite.HitPoint, BattleAgent.GameObject.transform);
			baseBullet.transform.position = new Vector3 (pos.x, pos.y, BattleAgent.GameObject.transform.position.z);


			baseBullet.Speed = 1136.0f / 1000.0f;
			
			AttackMessage message = new AttackMessage (BattleAgent, BattleAgent.Targets, 1);
			
			baseBullet.FlyToTarget (message);
			
			
			//			for (int i = 0; i < battleAgent.Targets.Count; i++) {
			//
			//				BattleAgent t = battleAgent.Targets [i];
			//				
			//				t.dispatchEvent (SoldierEvent.HIT, message);
			//			}
			
		}
		
		
		if (keyId == KeyEventId.UltShootOn) {
			
			GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [1].Prefab);
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

 
			
			//			for (int i = 0; i < battleAgent.Targets.Count; i++) {
			//				
			//				BattleAgent t = battleAgent.Targets [i];
			//				
			//				t.dispatchEvent (SoldierEvent.HIT, message);
			//			}
		}
	}

	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override public void AddSoundDemo ()
	{
		BattleAgent.BaseSprite.AddSound (StateId.Attack, "attack_le");
		BattleAgent.BaseSprite.AddSound (StateId.Ult, "ult_le");
		BattleAgent.BaseSprite.AddSound (StateId.Dead, "dead_le");

		BattleAgent.AddTimerDemo (new float[]{1.2f, 6.0f});
	}
	
}
