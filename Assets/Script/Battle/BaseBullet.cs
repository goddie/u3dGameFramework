using System;
using UnityEngine;

/// <summary>
/// 子弹基类
/// </summary>
using System.Collections;


public class BaseBullet : BaseAnim
{
	/// <summary>
	/// 子弹速度
	/// </summary>
	/// <value>The speed.</value>
	public float Speed {
		get;
		set;
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
			RemoveAnimator ();
		}
		 

		if (keyId == KeyEventId.AttackOn) {
			Transform t1 = gameObject.transform;
			Transform t2 = this.attackMessage.Targets [0].GameObject.transform;
			float dis = Vector2.Distance (new Vector2 (t1.localPosition.x, t1.localPosition.y), 
			                              new Vector2 (t2.localPosition.x, t2.localPosition.y));
			attackMessage.Targets [0].dispatchEvent (SoldierEvent.HIT, attackMessage);

			attackMessage.Sender.BaseSoldier.OnAttackEnd ();

			RemoveAnimator ();
		}
	}



	/// <summary>
	/// 在目标点出现
	/// </summary>
	/// <param name="attackMessage">Attack message.</param>
	public void BornToTarget (AttackMessage attackMessage)
	{
		this.attackMessage = attackMessage;
		Transform t2 = attackMessage.Targets [0].GameObject.transform;
		//gameObject.transform.position = t2.position;

		Vector3 pos = MapUtil.RelativeMovePosition (this.BattleAgent.BaseSprite.HitPoint, t2);
		gameObject.transform.position = new Vector3 (pos.x, pos.y, t2.position.z);
		
	}



	/// <summary>
	/// 飞向目标
	/// </summary>
	public void FlyToTarget (AttackMessage attackMessage)
	{
		this.attackMessage = attackMessage;

		Transform t1 = attackMessage.Sender.GameObject.transform;
		Transform t2 = attackMessage.Targets [0].GameObject.transform;

		float dis = Mathf.Abs (t1.position.x - t2.position.x);
		float time = dis / Speed;

		Vector3 hit = attackMessage.Targets [0].BaseSprite.HitPoint;
 
		Vector3 targetScreen = MapUtil.RelativeMovePosition (hit, t2);
		
		//t2.localPosition = target;

		Hashtable args = new Hashtable ();
		args.Add ("easeType", iTween.EaseType.linear);
		args.Add ("x", targetScreen.x);
		args.Add ("y", targetScreen.y);
 
		args.Add ("time", 0.15f);

		//args.Add ("time", 15f);
		args.Add ("oncomplete", "OnComplete");
		args.Add ("oncompletetarget", gameObject);
		//args.Add ("oncompleteparams", this.attackMessage);

		iTween.MoveTo (gameObject, args);

		//Debug.Log ("FlyToTarget:" + targetScreen);
	}

	/// <summary>
	/// 沿着方向飞出屏幕
	/// </summary>
	/// <param name="attackMessage">Attack message.</param>
	public void FlyOutOfStage (AttackMessage attackMessage)
	{
		this.attackMessage = attackMessage;

		float direct = Mathf.Sign (gameObject.transform.position.x - attackMessage.Sender.GameObject.transform.position.x);
		
		Transform t1 = attackMessage.Sender.GameObject.transform;
		Transform t2 = attackMessage.Targets [0].GameObject.transform;


		//Vector2 targetPost = new Vector2(t2.position.);

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
		//Debug.Log("StateId.Hit");
		StartCoroutine (PlayOneShot (StateId.Hit));
	}


	
}