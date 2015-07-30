using System;
using UnityEngine;

/// <summary>
/// 子弹基类
/// </summary>
using System.Collections;


public class BaseBullet : BaseAnim
{

	/// <summary>
	/// 武器朝向
	/// </summary>
	public FaceTo FaceTo {
		get;
		set;
	}

	/// <summary>
	/// 子弹速度
	/// </summary>
	/// <value>The speed.</value>
	public float Speed {
		get;
		set;
	}

	/// <summary>
	/// 需要跟随的目标
	/// </summary>
	private GameObject targetToFollow;

	/// <summary>
	/// 跟随时间
	/// </summary>
	private float timeToFollow;


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

			if (this.attackMessage.ComboCount > 0) {

				attackMessage.Targets [0].dispatchEvent (SoldierEvent.COMBO_HIT, attackMessage);
				attackMessage.Sender.BaseSoldier.OnAttackEnd ();
				RemoveAnimator ();
				
			}else
			{
				attackMessage.Targets [0].dispatchEvent (SoldierEvent.HIT, attackMessage);
				attackMessage.Sender.BaseSoldier.OnAttackEnd ();
				RemoveAnimator ();
			}
		}
	}



	/// <summary>
	/// 在目标点出现
	/// </summary>
	/// <param name="attackMessage">Attack message.</param>
	public void AttachTarget (AttackMessage attackMessage)
	{
		this.attackMessage = attackMessage;
		Transform t2 = attackMessage.Targets [0].GameObject.transform;
		//gameObject.transform.position = t2.position;

		Vector3 pos = MapUtil.GetHitPointWorld(attackMessage.Targets[0]);
		gameObject.transform.position = new Vector3 (pos.x, pos.y, t2.position.z);
		
	}

	/// <summary>
	/// Attachs the middle.
	/// </summary>
	/// <param name="attackMessage">Attack message.</param>
	public void AttachMiddle(AttackMessage attackMessage)
	{
		this.attackMessage = attackMessage;

		float y = attackMessage.Targets[0].MapPos.y;
		float x = Mathf.Round( MapUtil.MAX_COL / 2.0f);


		gameObject.transform.position = MapUtil.GetInstance.MapToWorld(x,y-1);

	}


	/// <summary>
	/// 跟随目标
	/// </summary>
	/// <param name="attackMessage">Attack message.</param>
	/// <param name="time">Time.</param>
	public void FollowTarget(AttackMessage attackMessage,float time)
	{
		targetToFollow = this.attackMessage.Targets[0].GameObject;

		this.attackMessage = attackMessage;
		Transform t2 = attackMessage.Targets [0].GameObject.transform;
		//gameObject.transform.position = t2.position;
		
		Vector3 pos = MapUtil.GetHitPointWorld(attackMessage.Targets[0]);
		gameObject.transform.position = pos;

		InvokeRepeating("FollowTarget", 1.0f, 0.1f);
	}


	private void FollowTarget()
	{
		gameObject.transform.position = targetToFollow.transform.position;

	}

	/// <summary>
	/// 指定时间内飞到目的地
	/// </summary>
	/// <param name="attackMessage">Attack message.</param>
	/// <param name="time">Time.</param>
	public void FlyToTarget (AttackMessage attackMessage,float time)
	{
		this.attackMessage = attackMessage;
		Vector3 hit  = MapUtil.GetHitPointWorld( attackMessage.Targets [0]);
		Hashtable args = new Hashtable ();
		args.Add ("easeType", iTween.EaseType.linear);
		args.Add ("x", hit.x);
		args.Add ("y", hit.y);
		args.Add ("time", time);
		args.Add ("oncomplete", "OnComplete");
		args.Add ("oncompletetarget", gameObject);
		iTween.MoveTo (gameObject, args);
		//Debug.Log ("FlyToTarget:" + targetScreen);
	}


	/// <summary>
	/// 指定时间内飞到目的地脚下
	/// </summary>
	/// <param name="attackMessage">Attack message.</param>
	/// <param name="time">Time.</param>
	public void FlyToTargetRoot (AttackMessage attackMessage,float time)
	{
		this.attackMessage = attackMessage;
		Vector3 hit  = attackMessage.Targets [0].GameObject.transform.position;
		Hashtable args = new Hashtable ();
		args.Add ("easeType", iTween.EaseType.linear);
		args.Add ("x", hit.x);
		args.Add ("y", hit.y);
		args.Add ("time", time);
		args.Add ("oncomplete", "OnComplete");
		args.Add ("oncompletetarget", gameObject);
		iTween.MoveTo (gameObject, args);
		
		//Debug.Log ("FlyToTarget:" + targetScreen);
	}

	/// <summary>
	/// 飞向目标
	/// </summary>
	public void FlyToTarget (AttackMessage attackMessage)
	{
		this.attackMessage = attackMessage;

		//Debug.Log(MapUtil.GetInstance.WorldToMap(attackMessage.Targets[0].GameObject.transform.position));

//		Transform t1 = attackMessage.Sender.GameObject.transform;
//		Transform t2 = attackMessage.Targets [0].GameObject.transform;
//
//		float dis = Mathf.Abs (t1.position.x - t2.position.x);
//		float time = dis / Speed;

		//Vector3 hit = attackMessage.Targets [0].BaseSprite.HitPoint;
 		
		//Debug.Log( attackMessage.Targets [0].MapPos);
		//Debug.Log( attackMessage.Targets [0].GameObject.transform.position);

		Vector3 hit  = MapUtil.GetHitPointWorld( attackMessage.Targets [0]);
		//Vector3 hit = attackMessage.Targets [0].GameObject.transform.position;
	
		//Vector3 targetScreen = MapUtil.RelativeMovePosition (hit, t2);
		
		//t2.localPosition = target;

		Hashtable args = new Hashtable ();
		args.Add ("easeType", iTween.EaseType.linear);
		args.Add ("x", hit.x);
		args.Add ("y", hit.y);
		args.Add ("speed",80f);
		//args.Add ("time", 0.15f);

		//args.Add ("time", 15f);
		args.Add ("oncomplete", "OnComplete");
		args.Add ("oncompletetarget", gameObject);
		//args.Add ("oncompleteparams", this.attackMessage);

		iTween.MoveTo (gameObject, args);

		//Debug.Log ("FlyToTarget:" + targetScreen);
	}








	/// <summary>
	/// 飞向目标然后出屏幕
	/// </summary>
	public void FlyToTargetOutScreen (AttackMessage attackMessage)
	{
		this.attackMessage = attackMessage;
		Vector3 hit  = MapUtil.GetHitPointWorld( attackMessage.Targets [0]);
		Hashtable args = new Hashtable ();
		args.Add ("easeType", iTween.EaseType.linear);
		args.Add ("x", hit.x);
		args.Add ("y", hit.y);
		args.Add ("speed",80f);
		//args.Add ("time", 0.15f);
		
		//args.Add ("time", 15f);
		args.Add ("oncomplete", "OnCompleteOutScreen");
		args.Add ("oncompletetarget", gameObject);
		//args.Add ("oncompleteparams", this.attackMessage);
		
		iTween.MoveTo (gameObject, args);
		
		//Debug.Log ("FlyToTarget:" + targetScreen);
	}

	/// <summary>
	/// 达到目的地之后飞出屏幕
	/// </summary>
	private void OnCompleteOutScreen()
	{
		StartCoroutine (PlayOneShot (StateId.Hit));


		Vector3 hit = Vector3.zero;

		Vector3 outPoint = MapUtil.GetInstance.GetOutPoint(attackMessage.Sender.GameObject.transform.position,
		                                       attackMessage.Targets[0].GameObject.transform.position);

		Hashtable args = new Hashtable ();
		args.Add ("easeType", iTween.EaseType.linear);
		args.Add ("x", hit.x);
		args.Add ("y", hit.y);
		args.Add ("speed",80f);
		//args.Add ("time", 0.15f);
		
		//args.Add ("time", 15f);
		args.Add ("oncomplete", "OnCompleteOutScreen");
		args.Add ("oncompletetarget", gameObject);
		//args.Add ("oncompleteparams", this.attackMessage);
		
		iTween.MoveTo (gameObject, args);
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