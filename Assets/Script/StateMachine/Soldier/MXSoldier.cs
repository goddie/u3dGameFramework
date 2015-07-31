using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MXSoldier : HeroSoldier
{
	/// <summary>
	/// 大招之前位置
	/// </summary>
	private Vector3 lastPosition;

	/// <summary>
	/// 大招效果
	/// </summary>
	private BaseBullet baseBullet;

	private List<BattleAgent> lastTargets;

	private bool isComboAttack = false;

	public MXSoldier ()
	{
	}


	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override public void AddSoundDemo ()
	{
		BattleAgent.BaseSprite.AddSound (StateId.Attack, "mx_action_attack_01");
		BattleAgent.BaseSprite.AddSound (StateId.Ult, "mx_action_ult_01");
		BattleAgent.BaseSprite.AddSound (StateId.Dead, "dead_mx");

		BattleAgent.AddTimerDemo (new float[]{1, 6});
		this.BattleAgent.AddSkillDemo (CooldownType.Attack, SkillData.testData [3]);
	}
 
	override protected void OnShootOnEvent ()
	{
		
	}
	
 
	/// <summary>
	/// 大招触发
	/// </summary>
	override protected void OnUltShootOnEvent ()
	{
		this.lastTargets = this.BattleAgent.Targets;

		//优先攻击浮空目标 Combo
		List<BattleAgent> floatList = BattleManager.SharedInstance.GetFloatTarget ();
		if (floatList.Count > 0) {

			this.BattleAgent.ChangeTargets(floatList);
			isComboAttack=true;
		}


		lastPosition = this.gameObject.transform.position;

		GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [10].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
		baseBullet = bullet.AddComponent<BaseBullet> (); 
		baseBullet.BattleAgent = this.BattleAgent;
		baseBullet.transform.position = MapUtil.GetHitPointWorld (this.BattleAgent.Targets [0]);

		//baseBullet去目标所在行 屏幕中间播放

		StartCoroutine (MoveToSide ());


		AttackMessage message = new AttackMessage (BattleAgent, this.BattleAgent.Targets, 1);

		if (isComboAttack) {
			message.ComboCount = 3;
		}


		baseBullet.AttachMiddle (message);

		this.BattleAgent.ChangeTargets(this.lastTargets);
		isComboAttack=false;
	}

	IEnumerator MoveToSide ()
	{
		float y = this.BattleAgent.Targets [0].MapPos.y;
		float x = Mathf.Round (MapUtil.MAX_COL / 2.0f);

		float tox = 1;

		if (this.BattleAgent.Targets [0].MapPos.x >= x) {
			tox = MapUtil.MAX_COL - 1;
		}
		
		this.gameObject.transform.position = MapUtil.GetInstance.MapToWorld (tox, y);
		
		Animator animator = this.GetComponent<Animator> ();
		animator.enabled = false;

		yield return new WaitForSeconds (0.3f);

		animator.enabled = true;
		this.gameObject.transform.position = lastPosition;

	}


}
