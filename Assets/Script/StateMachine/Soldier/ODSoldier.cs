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

	private List<Character> testDB = new List<Character> (){
		new Character(200,"红球",100,3,"Prefabs/drop"),
		new Character(201,"黄柱子",100,3,"Prefabs/effect"),
		new Character(203,"大招特效",100,3,"Prefabs/worldUlt")
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
			baseBullet.BattleAgent = this.battleAgent;
			//baseBullet.transform.position  = battleAgent.GameObject.transform.position;
			baseBullet.Speed = 1136.0f / 1000.0f;


			Vector3 pos = MapUtil.RelativeMovePosition (battleAgent.BaseSprite.HitPoint, battleAgent.GameObject.transform);
			baseBullet.transform.position = new Vector3 (pos.x, pos.y, battleAgent.GameObject.transform.position.z);

			AttackMessage message = new AttackMessage (battleAgent, battleAgent.Targets, 1);

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
			baseBullet.BattleAgent = this.battleAgent;
			baseBullet.transform.position = battleAgent.GameObject.transform.position;
			baseBullet.Speed = 1136.0f / 1000.0f;
			
			AttackMessage message = new AttackMessage (battleAgent, battleAgent.Targets, 1);
			
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
	override protected  void InitSound ()
	{
		soundDict.Add (StateId.Attack, "attack_od");
		soundDict.Add (StateId.Ult, "ult_od");
		soundDict.Add (StateId.Dead, "dead_od");
	}

	override protected void InitTimer ()
	{
		attackTimer = TimerManager.SharedInstance.CreateTimer (2.0f, new TimerEventHandler (AttackHandler));
		ultTimer = TimerManager.SharedInstance.CreateTimer (6.0f, new TimerEventHandler (UltHandler));

		attackTimer.Start ();
		ultTimer.Start ();
	}
	


}