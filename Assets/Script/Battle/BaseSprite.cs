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

//	private List<Character> testDB = new List<Character> (){
//		new Character(200,"落位黄光",100,3,"Prefabs/down",0),
//		new Character(201,"受击火花",100,3,"Prefabs/flash",0)
//	};


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

		if (battleAgent.Targets == null) {
			return;
		}
	
		Vector2 pos1 = MapUtil.GetInstance.MapToScreen (battleAgent.MapPos);
		Vector2 pos2 = MapUtil.GetInstance.MapToScreen (battleAgent.Targets [0].MapPos);

		float dx = pos1.x - pos2.x;
		float dy = pos1.y - pos2.y;

		float ang = Mathf.Atan2 (dy, dx);
		float ang2 = ang * (180 / Mathf.PI);



		if (ang2 > 0 && ang2 < 90) {
			
			DirectTo (1);
		}
		
		
		if (ang2 < 0 && ang2 > -90) {
			DirectTo (1);
		}
		
		if (ang2 > 90 && ang2 < 180) {
			DirectTo (2);
		}
		
		
		if (ang2 < -90 && ang2 > -180) {
			DirectTo (2);
		}

//		if (ang2 == 180 || ang2 == -180) {
//			if (pos2.x > pos1.x) {
//				DirectTo(1);
//			}
//		}
	



//		if (ang2<0 && ang2 > -90 || ang2>0 && ang2<90) {
//
//			DirectTo (1);
//		}
//
//		
//		if (ang2>-180 && ang2 < -90 || ang2>90 && ang2<180) {
//			
//			DirectTo (2);
//		}

	 


//		if (this.gameObject.GetComponent<ODSoldier> () != null) {
//			Debug.Log (ang2 + ", " + DateTime.Now.Millisecond);
//			Debug.DrawLine (battleAgent.GameObject.transform.position, battleAgent.Targets [0].GameObject.transform.position, Color.red);
//		}


		 
	}

	/// <summary>
	/// 转向方向
	/// </summary>
	/// <param name="dir">1=左 2=右</param>
	public void DirectTo (int dir)
	{
		//向左
		if (dir == 1) {

			if (battleAgent.BaseSprite.FaceTo == dir) {
				return;
			}

			battleAgent.BaseSprite.FaceTo = dir;
			Vector3 theScale = transform.localScale;
			theScale.x = 1;
			transform.localScale = theScale;
		}

		if (dir == 2) {
			if (battleAgent.BaseSprite.FaceTo == dir) {
				return;
			}

			battleAgent.BaseSprite.FaceTo = dir;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
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

//		GameObject go = battleAgent.GameObject;
//		Vector3 pos = go.transform.position;
//		Vector3 local = go.transform.localPosition;
		//go.transform.localPosition = new Vector3 (local.x - 3, local.y, local.z);



		//go.transform.position = new Vector3(go.transform.position.x - 3 ,go.transform.position.y,go.transform.position.z);
//		Hashtable args = new Hashtable ();
//		args.Add ("easeType", iTween.EaseType.easeOutQuart);
//		args.Add ("x", pos.x);
//		args.Add ("time", 0.1f);
//		iTween.MoveTo (go, args);

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

		yield return new WaitForSeconds (0.15f);
		
		img.color = Color.white;
		//go.transform.position = pos;
	}


	/// <summary>
	/// 添加落位动画
	/// </summary>
	public void AddDownEffect ()
	{
		GameObject downPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [5].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject down = StageManager.SharedInstance.AddToStage (parent, downPrefab);

		Image img = down.GetComponent<Image> ();
		img.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);

		BaseEffect baseEffect = down.AddComponent<BaseEffect> ();

		//Vector3 pos = MapUtil.GetInstance.MapToWorld (mapPos.x, mapPos.y);

		baseEffect.transform.position = gameObject.transform.position;
		baseEffect.transform.localPosition = new Vector3 (baseEffect.transform.localPosition.x, baseEffect.transform.localPosition.y + 300, baseEffect.transform.localPosition.z);

		AttackMessage message = new AttackMessage (battleAgent, battleAgent.Targets, 1);
		
		baseEffect.PlayOnAgent (message);
	}

	/// <summary>
	/// 受击火花特效
	/// </summary>
	public void AddFlashEffect ()
	{
		GameObject prefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [6].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject go = StageManager.SharedInstance.AddToStage (parent, prefab);
		
		BaseEffect baseEffect = go.AddComponent<BaseEffect> (); 
		//baseEffect.transform.position  = gameObject.transform.position;

		//Vector3 pos = MapUtil.RelativeMovePosition (battleAgent.BaseSprite.HitPoint, battleAgent.GameObject.transform);
		//baseEffect.transform.position = new Vector3 (pos.x, pos.y, battleAgent.GameObject.transform.position.z);

		baseEffect.transform.position = MapUtil.GetHitPointWorld (battleAgent);

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