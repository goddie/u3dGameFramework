using System;
using UnityEngine;

/// <summary>
/// 控制动画播放的精灵
/// </summary>
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BaseSprite : BaseAnim
{

	private List<Character> testDB = new List<Character> (){
		new Character(200,"落位黄光",100,3,"Prefabs/down"),
		new Character(201,"受击火花",100,3,"Prefabs/flash")
	};


	/// <summary>
	/// 声音配置
	/// </summary>
	private Dictionary<StateId,String> soundDict = new Dictionary<StateId, string> ();
	
	public Dictionary<StateId, String> SoundDict {
		get {
			return soundDict;
		}
	}

	/// <summary>
	/// 初始化默认声音
	/// 从配置文件中读取
	/// </summary>
	public virtual void InitSound ()
	{
		soundDict.Add (StateId.Attack, "attack_od");
		soundDict.Add (StateId.Ult, "ult_od");
		soundDict.Add (StateId.Dead, "dead_od");
	}

	/// <summary>
	/// 让BaseSolider 可以加入声音，测试用。
	/// </summary>
	/// <param name="stateId">State identifier.</param>
	/// <param name="soundName">Sound name.</param>
	public void AddSound (StateId stateId, String soundName)
	{
//		if (soundDict.ContainsKey (stateId)) {
//			soundDict.Remove (stateId);
//		}

		soundDict.Add (stateId, soundName);
	}




	private AnimatorStateInfo currentBaseStage;
	private StateId stateId;
	private BattleAgent battleAgent;


	private bool isSpellCooldown;
	private bool isAttackCooldown;

	/// <summary>
	/// 脸朝向0向左，1向右  
	/// 素材默认朝向
	/// </summary>
	/// <value>The face to.</value>
	public int FaceTo {
		get;
		set;
	}

	/// <summary>
	/// 受击点
	/// 箭就击中里飞
	/// </summary>

	public Vector3 HitPoint {
		get;
		set;
	}

	/// <summary>
	/// 攻击点
	/// 箭就从这里飞出
	/// </summary>
	public Vector3 AttackPoint {
		get;
		set;
	}



	public BattleAgent BattleAgent {
		get {
			return battleAgent;
		}
		set {
			battleAgent = value;
			animator = battleAgent.GameObject.GetComponent<Animator> ();
			currentBaseStage = animator.GetCurrentAnimatorStateInfo (0);
		}
	}

 
 	
//	public AgentSprite ()
//	{
//		addEventListener (SoldierEvent.ATTACK, SoldierAttackHandler);
//		addEventListener (SoldierEvent.CALLBACK, CallbackHandler);
//	}


	/// <summary>
	/// Faces to target.
	/// </summary>
	public void FaceToTarget ()
	{
		if (this.battleAgent.Targets == null) {
			return;
		}

		int sign = (int)Mathf.Sign (this.battleAgent.Targets [0].GameObject.transform.position.x - this.gameObject.transform.position.x);

		Vector3 rotation = gameObject.transform.rotation.eulerAngles;
		rotation.x = rotation.z = 0;

		if ((sign > 0 && this.FaceTo == 0) || (sign > 0 && this.FaceTo == 0) || (sign > 0 && this.FaceTo == 0) || (sign > 0 && this.FaceTo == 0)) {

			rotation.y *= -1;
			gameObject.transform.eulerAngles = - rotation;
			FaceTo = 1;
		}

		if (sign < 0 && this.FaceTo == 1) {
			
			rotation.y *= -1;
			gameObject.transform.eulerAngles = - rotation;
			FaceTo = 1;
		}

	}
	
	
	/// <summary>
	/// 动画状态切换
	/// </summary>
	/// <param name="state">State.</param>
	public void ToggleState (StateId stateId)
	{
 
		//animator.Play("hf_action_attack",0,1.0f);
		//SetBool (stateId, true);
		StartCoroutine (PlayOneShot (stateId));
	}



	/// <summary>
	/// 场景坐标
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public void SetLocalPosition (float x, float y)
	{
		//Debug.Log ("Sprite SetStagePosition");
		//battleAgent.GameObject.transform.Translate (x, y, 0);
		battleAgent.GameObject.transform.localPosition = new Vector3 (x, y, 0);
	}


	/// <summary>
	/// 地图坐标
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public void SetMapPosition (float x, float y)
	{
		Vector2 screenPos = MapUtil.GetInstance.MapToScreen (new Vector2 (x, y));

		Vector3 screenPos3 = new Vector3 (screenPos.x, screenPos.y, GlobalConfig.cameraFar);

		//battleAgent.GameObject.transform.position = screenPos;

		//screenPoint.z = 10.0f; //distance of the plane from the camera
		transform.position = Camera.main.ScreenToWorldPoint (screenPos3);

		battleAgent.SetMapPosition (x, y);

	}

	/// <summary>
	/// 被击中效果
	/// </summary>
	/// <param name="battleAgent">攻击方</param>
	public void HitEffect (BattleAgent attacker)
	{
		//Debug.Log ("HitEffect");

		float direct = Mathf.Sign (gameObject.transform.localPosition.x - attacker.GameObject.transform.localPosition.x);
		
		StartCoroutine (BackToIdle ());

		GameObject go = battleAgent.GameObject;
		Vector3 pos = go.transform.position;
		Vector3 local = go.transform.localPosition;
		go.transform.localPosition = new Vector3 (local.x - 3, local.y, local.z);
		//go.transform.position = new Vector3(go.transform.position.x - 3 ,go.transform.position.y,go.transform.position.z);
		Hashtable args = new Hashtable ();
		args.Add ("easeType", iTween.EaseType.easeOutQuart);
		args.Add ("x", pos.x);
		args.Add ("time", 0.1f);
		iTween.MoveTo (go, args);

		AddFlashEffect ();

	}
 
	/// <summary>
	/// 恢复受击前状态
	/// </summary>
	/// <returns>The to idle.</returns>
	private IEnumerator BackToIdle ()
	{
		GameObject go = battleAgent.GameObject;
		Image img = go.GetComponent<Image> ();
		Color c = img.color;
		Color r = new Color (1, 120.0f / 255.0f, 120.0f / 255.0f, 1f);
		img.color = r;

		yield return new WaitForSeconds (0.1f);
		
		img.color = Color.white;
	}


	/// <summary>
	/// 添加落位动画
	/// </summary>
	public void AddDownEffect ()
	{
		GameObject downPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [0].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject down = StageManager.SharedInstance.AddToStage (parent, downPrefab);

		BaseEffect baseEffect = down.AddComponent<BaseEffect> (); 
		baseEffect.transform.position = gameObject.transform.position;

		AttackMessage message = new AttackMessage (battleAgent, battleAgent.Targets, 1);
		
		baseEffect.PlayOnAgent (message);
	}

	/// <summary>
	/// 火花特效
	/// </summary>
	public void AddFlashEffect ()
	{
		GameObject prefab = ResourceManager.GetInstance.LoadPrefab (testDB [1].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject go = StageManager.SharedInstance.AddToStage (parent, prefab);
		
		BaseEffect baseEffect = go.AddComponent<BaseEffect> (); 
		//baseEffect.transform.position  = gameObject.transform.position;

		Vector3 pos = MapUtil.RelativeMovePosition (battleAgent.BaseSprite.HitPoint, battleAgent.GameObject.transform);
		baseEffect.transform.position = new Vector3 (pos.x, pos.y, battleAgent.GameObject.transform.position.z);


		AttackMessage message = new AttackMessage (battleAgent, battleAgent.Targets, 1);
		
		baseEffect.PlayOnAgent (message);
	}


	/// <summary>
	/// 播放状态声音
	/// </summary>
	/// <param name="stateId">State identifier.</param>
	public void PlaySound (StateId stateId)
	{
		//Debug.Log("PlaySound:"+stateId);
		if (!soundDict.ContainsKey (stateId)) {
			Debug.Log ("No Sound:" + stateId);
			return;	
		}
		
		String soundName = soundDict [stateId];
		AudioManager.SharedInstance.FMODEvent (soundName, 0.5f);
	}


 
}