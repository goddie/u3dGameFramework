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
	/// 地图位置
	/// </summary>
	private Vector2 mapPos;

	public Vector2 MapPos {
		get {
			return mapPos;
		}
	}

	private AttackMessage attackMessage;

	/// <summary>
	/// 技能列表
	/// </summary>
	private Dictionary<CooldownType,SkillData> skillDict = new Dictionary<CooldownType, SkillData> ();


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
		addEventListener (SoldierEvent.BATTLE_MESSAGE, HandleMessage);
		addEventListener (SoldierEvent.HIT, HitHandler);
	}


	public void HandleMessage (CEvent e)
	{
		this.attackMessage = (AttackMessage)e.data;

		SkillData skill = SkillData.testData [this.attackMessage.SkillId];



		if (skill.Range < Vector2.Distance (this.attackMessage.Sender.mapPos, 
		                                    this.attackMessage.Targets [0].mapPos)) {


			PathToTarget (skill.Range);

			return;
		}


		//大招 Id大于20000
		if (attackMessage.SkillId > 20000) {
			baseSoldier.OnUlt ();
		} else {
			baseSoldier.OnAttack ();
		}
		//Debug.Log ("battleMessageHandler");
	}

	/// <summary>
	/// 被击中
	/// </summary>
	/// <param name="e">E.</param>
	private void HitHandler (CEvent e)
	{
		//Debug.Log ("HitHandler");
		AttackMessage attackMessage = (AttackMessage)e.data;
		baseSprite.HitEffect (attackMessage.Sender);
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
	/// 寻找最适合的攻击站位
	/// 站位受技能射程影响
	/// </summary>
	/// <param name="range">Range.</param>
	public void PathToTarget (int range)
	{


		Vector2 targetPos = targets [0].mapPos;
		List<Vector2> result = new List<Vector2> ();
//		AStarRoute asr = new AStarRoute (MapUtil.GetInstance.GetMapMatrix (), 
//		                                 (int)mapPos.x, (int)mapPos.y, 
//		                                 (int)targetPos.x, (int)targetPos.y);
//		try {
//			result = asr.getResult ();
//		} catch (Exception ex) {
//			Debug.Log (ex.StackTrace);
//		}
//		result = asr.getResult ();




//		if (rs == null) {
//			baseSoldier.OnIdle ();
//			return;
//		}

		//近战
		if (skillDict [CooldownType.Attack].Range == 2) {
			Vector2 rs = MapUtil.GetInstance.GetAttackPos (this, targets [0], range);
			result.Add (rs);
		} else {
			
			Vector2 rs = MapUtil.GetInstance.GetRangeAttackPos (this, targets [0], range);
			result.Add (rs);
		}


		baseSoldier.OnWalk ();


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
		args.Add ("speed", 3f);  

		args.Add ("position", paths [0]);
		//移动的整体时间。如果与speed共存那么优先speed  
		//args.Add ("time", 5f);  
		//是否先从原始位置走到路径中第一个点的位置  
		//args.Add ("movetopath", true);  
		//延迟执行时间  
		//args.Add ("delay", 0.1f);  
		//移动的过程中面朝一个点  
		//args.Add ("looktarget", Vector3.zero);  
		//三个循环类型 none loop pingPong (一般 循环 来回)   
		//args.Add ("loopType", "none");  
		//是否让模型始终面朝当面目标的方向  
		//如果你发现你的模型在寻路的时候时钟都是一个方向那么一定要打开这个  
		//args.Add ("orienttopath", true);  
		
		//让模型开始寻路     
		iTween.MoveTo (this.gameObject, args);  
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
		AddTimer (CooldownType.Attack, 2.0f, new TimerEventHandler (AttackHandler));
		AddTimer (CooldownType.Ult, 6.0f, new TimerEventHandler (UltHandler));
	}

	private void AddTimer (CooldownType type, float second, TimerEventHandler action)
	{
		CooldownTimer t = TimerManager.SharedInstance.CreateTimer (second, action);
		timerDict.Add (type, t);

		if (this.baseSoldier.GetType () == typeof(ODSoldier)) {
			t.Start ();
		}
		if (this.baseSoldier.GetType () == typeof(HMSoldier)) {
			t.Start ();
		}

		if (this.baseSoldier.GetType () == typeof(LESoldier)) {
			t.Start ();
		}

		if (this.gameObject.name == "hf2") {
			t.Start ();
		}
		if (this.gameObject.name == "hf3") {
			t.Start ();
		}
		if (this.gameObject.name == "hf1") {
			t.Start ();
		}

		//t.Start ();
	}


	/// <summary>
	/// 攻击CD时调用
	/// 放在TimerManager 的Update中
	/// </summary>
	protected void AttackHandler ()
	{
		if (!isReady) {
			return;
		}
		//		if (!baseSoldier.IsIdle ()) {
//			return;
//		}

		AttackMessage message = new AttackMessage (this, this.targets, skillDict [CooldownType.Attack].Id);
		this.dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
		//Debug.Log ("attackHandler");
	}

	/// <summary>
	/// 大招CD时调用
	/// </summary>
	protected void UltHandler ()
	{
		if (!isReady) {
			return;
		}
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
	protected void PositonHandler ()
	{
		if (!isReady) {
			return;
		}


		mapPos = MapUtil.GetInstance.WorldToMap (gameObject.transform.position);
		baseSprite.FaceToTarget ();
		CheckGuardRange ();
		//Debug.Log (mapPos);
	}


	/// <summary>
	/// 检测警示范围内是否有敌人
	/// </summary>
	protected void CheckGuardRange ()
	{
		//近战不警戒
		if (skillDict [CooldownType.Attack].Range == 2) {
			return;
		}

		List<BattleAgent> enemy = BattleManager.SharedInstance.GetEnemyList ();

		for (int i = 0; i < enemy.Count; i++) {

			if (!enemy [i].Targets.Contains (this)) {
				continue;
			}

			//进入警戒范围
			if (Vector2.Distance (enemy [i].MapPos, this.mapPos) <= this.character.GuardRange) {

				PathToTarget (skillDict [CooldownType.Attack].Range);
			}

		}

	}

	
	public void AddTimerDemo (float[] timeList)
	{
		AddTimer (CooldownType.Attack, timeList [0], new TimerEventHandler (AttackHandler));
		AddTimer (CooldownType.Ult, timeList [1], new TimerEventHandler (UltHandler));
		AddTimer (CooldownType.Position, 0.1f, new TimerEventHandler (PositonHandler));
	}


	public void AddSkillDemo (CooldownType type, SkillData data)
	{
		skillDict.Add (type, data);
	}

}