using System;
using System.Collections.Generic;
using UnityEngine;


public class HMSoldier : HeroSoldier
{
	private float speed = 1.0f;
	
	private BaseBullet baseBullet;
	
	private List<Character> testDB= new List<Character>(){
		new Character(300,"光球",100,3,"Prefabs/ball")
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
			
			
			
			
			
			GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (testDB[0].Prefab);
			GameObject parent = StageManager.SharedInstance.EffectLayer; 
			GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
			baseBullet = bullet.AddComponent<BaseBullet>(); 
			baseBullet.BattleAgent = this.battleAgent;
			baseBullet.transform.position  = battleAgent.GameObject.transform.position;
			baseBullet.Speed = 1136.0f / 1000.0f;
			
			AttackMessage message = new AttackMessage (battleAgent, battleAgent.Targets, 1);
			
			baseBullet.BornToTarget(message);
			
			
			//			for (int i = 0; i < battleAgent.Targets.Count; i++) {
			//
			//				BattleAgent t = battleAgent.Targets [i];
			//				
			//				t.dispatchEvent (SoldierEvent.HIT, message);
			//			}
			
		}
		
		
		if (keyId == KeyEventId.UltShootOn)
		{
			
			GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (testDB[1].Prefab);
			GameObject parent = StageManager.SharedInstance.EffectLayer; 
			GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
			baseBullet = bullet.AddComponent<BaseBullet>(); 
			baseBullet.BattleAgent = this.battleAgent;
			baseBullet.transform.position  = battleAgent.GameObject.transform.position;
			baseBullet.Speed = 1136.0f / 1000.0f;
			
			AttackMessage message = new AttackMessage (battleAgent, battleAgent.Targets, 1);
			
			baseBullet.FlyToTarget(message);

 
			
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
	override protected   void InitSound()
	{
		soundDict.Add(StateId.Attack,"attack_hm");
		soundDict.Add(StateId.Ult,"ult_hm");
		soundDict.Add(StateId.Dead,"dead_hm");
	}
}
