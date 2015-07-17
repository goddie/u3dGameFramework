using System;

/// <summary>
/// 动画播工具
/// </summary>
using System.Collections;
using UnityEngine;


public class BasePlayer : MonoBehaviour
{
	/// <summary>
	/// 动画控制
	/// </summary>
	protected Animator animator;

	/// <summary>
	/// 战斗消息
	/// </summary>
	protected AttackMessage attackMessage;

	/// <summary>
	/// 特效的主人
	/// </summary>
	protected BattleAgent battleAgent;

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


	protected void RemoveAnimator()
	{
		animator.enabled = false;
		Destroy(gameObject);
	}

	protected Boolean IsState (StateId stateId)
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
	protected IEnumerator PlayOneShot (StateId stateId)
	{
		SetBool (stateId, true);
		yield return null;
		SetBool (stateId, false);
	}
	
	protected void SetBool (StateId stateId, Boolean value)
	{
		string enumName = Enum.GetName (typeof(StateId), stateId);
		string stateName = enumName.Replace ("State", "").ToLower ();
		//Debug.Log ("SetBool:" + stateName);
		animator.SetBool (stateName, value);
	}

}


 
