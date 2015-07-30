using System;

/// <summary>
/// 动画播工具
/// </summary>
using System.Collections;
using UnityEngine;


public class BaseAnim : MonoBehaviour
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
	/// 战斗代理
	/// </summary>
	public BattleAgent BattleAgent {
		get;
		set;
	}


	void Start ()
	{
		
		//Debug.Log("BaseBullet Start()");
	}
	
	
	void Awake ()
	{
		animator = gameObject.GetComponent<Animator> ();
	}

  

	/// <summary>
	/// 移除动画
	/// </summary>
	protected void RemoveAnimator ()
	{
		animator.enabled = false;
		Destroy (gameObject);
	}


	/// <summary>
	/// 停止动画
	/// </summary>
	protected void StopAnimator()
	{
		animator.enabled = false;
	}


	/// <summary>
	/// 淡出动画
	/// </summary>
	protected void FadeOutAnimator()
	{
		StartCoroutine(FadeOut(1.0f));
	}


	/// <summary>
	/// 淡出动画
	/// </summary>
	/// <param name="time">Time.</param>
	protected void FadeOutAnimator(float time)
	{
		StartCoroutine(FadeOut(time));
	}


	private IEnumerator FadeOut(float time)
	{
		animator.enabled = false;
		Hashtable args = new Hashtable ();
		args.Add ("time", time);
		args.Add ("alpha", 0);
		args.Add ("oncomplete", "MaskFadeComplete");
		args.Add ("oncompletetarget", this.gameObject);
		args.Add ("ignoretimescale", true);
		//Image img = blackMask.GetComponent<Image>();
		iTween.FadeTo (this.gameObject, args);

		yield return new WaitForSeconds(1.0f);

		Destroy(gameObject);
	}


	/// <summary>
	/// 播放一次动画
	/// </summary>
	/// <returns>The one shot.</returns>
	/// <param name="stateId">State identifier.</param>
	protected IEnumerator PlayOneShot (StateId stateId)
	{
		SetBool (stateId, true);
		//yield return null;
		yield return new WaitForSeconds(0.1f);
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


 
