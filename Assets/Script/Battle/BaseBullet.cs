using System;
using UnityEngine;

/// <summary>
/// 子弹基类
/// </summary>
using System.Collections;


public class BaseBullet : MonoBehaviour ,IStateEvent
{

	private Animator animator;

	/// <summary>
	/// 攻击信息
	/// </summary>
	private AttackMessage attackMessage;


	/// <summary>
	/// 子弹速度
	/// </summary>
	/// <value>The speed.</value>
	public float Speed {
		get;
		set;
	}

	/// <summary>
	/// 子弹的主人
	/// </summary>
	private BattleAgent battleAgent;

	public BattleAgent BattleAgent {
		get {
			return battleAgent;
		}
		set {
			battleAgent = value;
		}
	}

	void Start()
	{

		//Debug.Log("BaseBullet Start()");
	}


	void Awake ()
	{

		animator = gameObject.GetComponent<Animator> ();

	}
	

	/// <summary>
	/// 动画事件触发
	/// </summary>
	/// <param name="keyId">Key identifier.</param>
	public void TriggerKeyEvent (KeyEventId keyId)
	{
		//IsState (StateId.Idle);

		if (keyId == KeyEventId.StateEnd) {

			//battleAgent.dispatchEvent();
		}


		if (keyId == KeyEventId.AttackOn) {

			Debug.Log("AttackOn:"+ this.attackMessage.Targets[0].ToString());

			Transform t1 = gameObject.transform;
			Transform t2 = attackMessage.Targets[0].GameObject.transform;
			
			float dis = Vector2.Distance (new Vector2 (t1.position.x, t1.position.y), 
			                              new Vector2 (t2.position.x, t2.position.y));
			//Debug.Log("OnComplete"+dis);
			
			
			if (dis <= 0) {
				attackMessage.Targets[0].dispatchEvent (SoldierEvent.HIT, attackMessage);
			}

			animator.enabled = false;

			//gameObject.SetActive(false);

			Destroy(gameObject);

		}
	}

	public Boolean IsState (StateId stateId)
	{
		AnimatorStateInfo aif = animator.GetCurrentAnimatorStateInfo (0);
		//Debug.Log (aif.ToString ());
		return false;
	}


	/// <summary>
	/// 播放一次动画
	/// </summary>
	/// <returns>The one shot.</returns>
	/// <param name="stateId">State identifier.</param>
	private IEnumerator PlayOneShot (StateId stateId)
	{
		SetBool(stateId,true);
		yield return null;
		SetBool(stateId,false);
	}


	void SetBool (StateId stateId, Boolean value)
	{
		string enumName = Enum.GetName (typeof(StateId), stateId);
		string stateName = enumName.Replace ("State", "").ToLower ();
		//Debug.Log ("SetBool:" + stateName);
		animator.SetBool (stateName, value);
	}

	/// <summary>
	/// 飞向目标
	/// </summary>
	public void FlyToTarget (AttackMessage attackMessage)
	{
		this.attackMessage = attackMessage;

		Transform t1 = attackMessage.Sender.GameObject.transform;
		Transform t2 = this.attackMessage.Targets[0].GameObject.transform;

		float dis = Mathf.Abs (t1.position.x - t2.position.x);
		float time = dis / Speed;

		Hashtable args = new Hashtable ();
		args.Add ("easeType", iTween.EaseType.linear);
		args.Add ("x", t2.position.x);
		args.Add ("y", t2.position.y);
		args.Add ("time", 0.3f);
		args.Add ("oncomplete", "OnComplete");
		args.Add ("oncompletetarget", gameObject);
		//args.Add ("oncompleteparams", this.attackMessage);

		iTween.MoveTo (gameObject, args);
	}

	/// <summary>
	/// 沿着方向飞出屏幕
	/// </summary>
	/// <param name="attackMessage">Attack message.</param>
	public void FlyOutOfStage(AttackMessage attackMessage)
	{
		
		float direct = Mathf.Sign( gameObject.transform.position.x - attackMessage.Sender.GameObject.transform.position.x);

		this.attackMessage = attackMessage;
		
		Transform t1 = attackMessage.Sender.GameObject.transform;
		Transform t2 = this.attackMessage.Targets[0].GameObject.transform;
		
		float dis = Mathf.Abs (t1.position.x - t2.position.x);
		float time = dis / Speed;
		
		Hashtable args = new Hashtable ();
		args.Add ("easeType", iTween.EaseType.linear);
		args.Add ("x", t2.position.x);
		args.Add ("y", t2.position.y);
		args.Add ("time", 0.3f);
		args.Add ("oncomplete", "OnComplete");
		args.Add ("oncompletetarget", gameObject);
		//args.Add ("oncompleteparams", this.attackMessage);
		
		iTween.MoveTo (gameObject, args);
	}


	/// <summary>
	/// 达到目标
	/// </summary>
	/// <param name="attackMessage">Attack message.</param>
	private void OnComplete ()
	{
		StartCoroutine(PlayOneShot(StateId.Hit));
	}


	
}