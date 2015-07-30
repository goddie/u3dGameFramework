using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 代理控制器
/// 可以接收分发事件
/// </summary>
using System.Collections;


public class BattleAgent : EventDispatcherBase
{
	/// <summary>
	/// 普通攻击CD
	/// </summary>
	private bool isAttackCD = true;

	/// <summary>
	/// 大招CD
	/// </summary>
	private bool isUltCD = false;


	/// <summary>
	/// 是否可以寻路
	/// </summary>
	private bool isCanPath = true;

	/// <summary>
	/// 地图位置
	/// </summary>
	private Vector2 mapPos;

	public Vector2 MapPos {
		get {
			return mapPos;
		}
	}


	/// <summary>
	/// 更改目标
	/// </summary>
	/// <param name="newTargets">New targets.</param>
	public void ChangeTargets (List<BattleAgent> newTargets)
	{
		if (newTargets.Count > 0) {
			this.targets = newTargets;
		}

	}

	private AttackMessage attackMessage;

	/// <summary>
	/// 技能列表
	/// </summary>
	private Dictionary<CooldownType,SkillData> skillDict = new Dictionary<CooldownType, SkillData> ();

	public Dictionary<CooldownType, SkillData> SkillDict {
		get {
			return skillDict;
		}
	}


	/// <summary>
	/// 冷却列表
	/// </summary>
	private Dictionary<CooldownType,CooldownTimer> timerDict = new Dictionary<CooldownType, CooldownTimer> ();
	private bool isReady;

	public bool IsReady {
		get {
			return isReady;
		}
		set {
			isReady = value;
		}
	}

	public BattleAgent (BaseSoldier baseSoldier, Character character)
	{
		this.BaseSoldier = baseSoldier;

		this.gameObject = baseSoldier.gameObject;
		this.BaseSprite = baseSoldier.gameObject.AddComponent<BaseSprite> ();
		this.Character = character;

		this.baseSoldier.AddSoundDemo ();
		//AddTimer ();

		AddEventListeners ();
	}

	private void AddEventListeners ()
	{
		addEventListener (SoldierEvent.BATTLE_MESSAGE, BattleMessageHandler);
		addEventListener (SoldierEvent.HIT_FLOAT, FloatHandler); 
		addEventListener (SoldierEvent.HIT, HitHandler);
		addEventListener (SoldierEvent.COMBO_HIT, ComboHitHandler);
		addEventListener (SoldierEvent.SURPRISE, SurpriseHandler);
	}




	/// <summary>
	/// 被击中
	/// </summary>
	/// <param name="e">E.</param>
	private void HitHandler (CEvent e)
	{
		//Debug.Log ("HitHandler");
		AttackMessage am = (AttackMessage)e.data;
		baseSprite.HitEffect (am);
	}

	/// <summary>
	/// combo连击特效
	/// </summary>
	/// <param name="e">E.</param>
	private void ComboHitHandler(CEvent e)
	{
		AttackMessage am = (AttackMessage)e.data;
		baseSprite.ComboHitEffect (am);
	}


	private void SurpriseHandler(CEvent e)
	{
		baseSoldier.OnSurprise();
	}

	/// <summary>
	/// 目标被浮空
	/// </summary>
	/// <param name="e">E.</param>
	private void FloatHandler (CEvent e)
	{
		AttackMessage am = (AttackMessage)e.data;
		baseSprite.HitEffect (am);
		baseSoldier.OnFloat ();
	}

	private GameObject gameObject;
	
	public GameObject GameObject {
		get {
			return gameObject;
		}
	}
	
	
	/// <summary>
	/// 兵种
	/// </summary>
	private BaseSoldier baseSoldier;
	
	public BaseSoldier BaseSoldier {
		get {
			return baseSoldier;
		}
		set {
			baseSoldier = value;
			baseSoldier.BattleAgent = this;
		}
	}
	
	
	/// <summary>
	/// 动作相关
	/// </summary>
	private BaseSprite baseSprite;
	
	public BaseSprite BaseSprite {
		get {
			return baseSprite;
		}
		set {
			baseSprite = value;
			baseSprite.BattleAgent = this;
		}
	}
	
	/// <summary>
	/// 战斗目标
	/// </summary>
	private List<BattleAgent> targets;
	
	public List<BattleAgent> Targets {
		get {
			return targets;
		}
	}	
	
	
	
	/// <summary>
	/// 角色属性
	/// </summary>
	private Character character;
	
	public Character Character {
		get {
			return character;
		}
		set {
			character = value;
			character.BattleAgent = this;
			this.baseSprite.HitPoint = character.HitPoint;
			this.baseSprite.AttackPoint = character.AttackPoint;
		}
	}

	/// <summary>
	/// 处理战斗信息
	/// </summary>
	/// <param name="e">E.</param>
	public void BattleMessageHandler (CEvent e)
	{
		this.attackMessage = (AttackMessage)e.data;
		
		SkillData skill = SkillData.testData [this.attackMessage.SkillId];
		
		


		//大招 Id大于20000 没有距离限制
		if (attackMessage.SkillId > 20000) {

			baseSoldier.OnUlt ();
		} else {

			//射程之外
			if (skill.Range < Vector2.Distance (this.attackMessage.Sender.mapPos, 
			                                    this.attackMessage.Targets [0].mapPos)) {
				PathToTarget (skill.Range);
				return;
			}

			//普通攻击
			baseSoldier.OnAttack ();
		}
		//Debug.Log ("battleMessageHandler");
	}

	

	/// <summary>
	/// 寻找最适合的攻击站位
	/// 站位受技能射程影响
	/// </summary>
	/// <param name="range">技能射程</param>
	public void PathToTarget (int range)
	{

		baseSoldier.OnWalk ();
		isCanPath = false;


		Vector2 targetPos = targets [0].mapPos;
		List<Vector2> result = new List<Vector2> ();

		//近战策略
		if (skillDict [CooldownType.Attack].Range == SkillData.MELEE) {
			Vector2 rs = MapUtil.GetInstance.GetAttackPos (this, targets [0], range);
			result.Add (rs);
		} 

		//中程策略
		if (skillDict [CooldownType.Attack].Range == SkillData.BOSS_MELEE) {
			Vector2 rs = MapUtil.GetInstance.GetAttackPosSameRow (this, targets [0], range);
			result.Add (rs);
		} 


		//远程策略
		if (skillDict [CooldownType.Attack].Range == SkillData.RANGE) {
			
			Vector2 rs = MapUtil.GetInstance.GetRangeAttackPos (this, targets [0], range);
			result.Add (rs);
		}





		List<Vector3> paths = new List<Vector3> ();

		for (int i = 0; i < result.Count; i++) {
			
			Vector3 v = MapUtil.GetInstance.MapToWorld (result [i].x, result [i].y);
			paths.Add (v);

		}

		//MapUtil.GetInstance.DrawMapPoint (result.ToArray ());

		//return;

		Hashtable args = new Hashtable ();  
		//设置路径的点  
		// args.Add ("path", paths.ToArray ());  
		//设置类型为线性，线性效果会好一些。  
		args.Add ("easeType", iTween.EaseType.linear);  
		//设置寻路的速度  
		args.Add ("speed", this.character.Speed);  
		args.Add ("position", paths [0]);
		args.Add ("oncomplete", "OnWalkEnd");
		args.Add ("oncompletetarget", this.gameObject);
		//让模型开始寻路     
		iTween.MoveTo (this.gameObject, args);




	}

	public void FinishAttack ()
	{
		isCanPath = true;
	}

	/// <summary>
	/// 完成移动
	/// </summary>
	public void FinishMove ()
	{
		baseSoldier.OnIdle ();
		isCanPath = true;
		//Debug.Log("FinishMove");
	}

	/// <summary>
	/// 设置地图位置
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public void SetMapPosition (float x, float y)
	{
		mapPos = new Vector2 (x, y);
	}
	
	public void RemoveFromStage ()
	{
		clearEvents ();
	}

	/// <summary>
	/// 新增目标
	/// </summary>
	/// <param name="target">Target.</param>
	public void AddTarget (BattleAgent target)
	{
		if (this.targets == null) {
			this.targets = new List<BattleAgent> ();
		}
		this.targets.Add (target);
	}

	/// <summary>
	/// 计时器初始化
	/// </summary>
	private void AddTimer ()
	{
		AddTimer (CooldownType.Attack, 2.0f, new TimerEventHandler (AttackCDTimer));
		AddTimer (CooldownType.Ult, 6.0f, new TimerEventHandler (UltCDTimer));
		AddTimer (CooldownType.Update, 0.1f, new TimerEventHandler (UpdateHandler));
	}

	private void AddTimer (CooldownType type, float second, TimerEventHandler action)
	{
		CooldownTimer t = TimerManager.SharedInstance.CreateTimer (second, action);
		timerDict.Add (type, t);


		String name = this.gameObject.name;
		Type tt = this.baseSoldier.GetType ();

//		if (tt == typeof(ODSoldier) || 
//		    tt == typeof(HMSoldier) ||
//		    tt == typeof(LESoldier) || 
//		    tt == typeof(MXSoldier)) {
//			t.Start();
//		}
//
//		if (name=="hf1"||name=="hf2"||name=="hf3"||name=="hf1") {
//			
//		}
//
//		 
//		if (this.gameObject.name == "hf2") {
//			t.Start ();
//		}
//		if (this.gameObject.name == "hf3") {
//			t.Start ();
//		}
//		if (this.gameObject.name == "hf1") {
//			t.Start ();
//		}

		t.Start ();
	}


	/// <summary>
	/// 攻击CD时调用
	/// 放在TimerManager 的Update中
	/// </summary>
	protected void AttackCDTimer ()
	{
		if (!isReady) {
			return;
		}
		isAttackCD = true;
		//Debug.Log ("attackHandler");
	}

	/// <summary>
	/// 大招CD时调用
	/// </summary>
	protected void UltCDTimer ()
	{
		if (!isReady) {
			return;
		}
		isUltCD = true;
		//		if (this.Targets == null || this.Targets.Count == 0) {
//			return;
//		}
//		
//		AttackMessage message = new AttackMessage (this, this.targets, 1, CooldownType.Ult);
//		this.dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
	}


	/// <summary>
	/// 0.1秒执行一次计算路径和更新坐标
	/// </summary>
	protected void UpdateHandler ()
	{
		baseSprite.FaceToTarget ();

		if (!isReady) {
			//Debug.Log("Not ready");
			return;
		}

		if (isAttackCD) {
			AttackMessage message = new AttackMessage (this, this.targets, skillDict [CooldownType.Attack].Id);
			this.dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
			isAttackCD = false;
		}

		if (isUltCD) {

			//boss自动放技能
			if (this.baseSoldier.GetType () == typeof(AMSoldier)) {
				AttackMessage message = new AttackMessage (this, this.targets, skillDict [CooldownType.Ult].Id);
				this.dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
				isUltCD = false;
			}

		}


		UpdateMapPosition ();
		CheckGuardRange ();

		//Debug.Log (mapPos);
	}


	/// <summary>
	/// 更新地图坐标
	/// </summary>
	private void UpdateMapPosition ()
	{
		mapPos = MapUtil.GetInstance.WorldToMap (gameObject.transform.position);
	}


	/// <summary>
	/// 检测警示范围内是否有敌人
	/// </summary>
	protected void CheckGuardRange ()
	{
		//近战不警戒
		if (skillDict [CooldownType.Attack].Range == SkillData.MELEE) {
			return;
		}


		//已经移动过一次
		if (!isCanPath) {
			return;
		}

		List<BattleAgent> npcList = BattleManager.SharedInstance.GetEnemyList ();

		for (int i = 0; i < npcList.Count; i++) {

			if (!npcList [i].Targets.Contains (this)) {
				continue;
			}

			//进入警戒范围
			if (Vector2.Distance (npcList [i].MapPos, this.mapPos) <= this.character.GuardRange) {

				PathToTarget (skillDict [CooldownType.Attack].Range);
			}

		}

	}
	
	public void AddTimerDemo (float[] timeList)
	{
		AddTimer (CooldownType.Attack, timeList [0], new TimerEventHandler (AttackCDTimer));
		AddTimer (CooldownType.Ult, timeList [1], new TimerEventHandler (UltCDTimer));
		AddTimer (CooldownType.Update, 0.1f, new TimerEventHandler (UpdateHandler));
	}

	/// <summary>
	/// 普通攻击技能
	/// 攻击距离
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="data">Data.</param>
	public void AddSkillDemo (CooldownType type, SkillData data)
	{
		skillDict.Add (type, data);
	}



}