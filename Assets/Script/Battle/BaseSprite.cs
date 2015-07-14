using System;
using UnityEngine;

/// <summary>
/// 控制动画播放的精灵
/// </summary>
using System.Collections;
using UnityEngine.UI;


public class BaseSprite : MonoBehaviour
{

	private Animator animator;
	private AnimatorStateInfo currentBaseStage;
	private StateId stateId;
	private BattleAgent battleAgent;

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
	/// 动画状态切换
	/// </summary>
	/// <param name="state">State.</param>
	public void ToggleState (StateId stateId)
	{
		//animator.Play("hf_action_attack",0,1.0f);
		//SetBool (stateId, true);
		StartCoroutine( PlayOneShot(stateId) );
	
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
	/// 场景坐标
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public void SetStagePosition (float x, float y)
	{
		//Debug.Log ("Sprite SetStagePosition");
		battleAgent.GameObject.transform.Translate (x, y, 0);
	}


	/// <summary>
	/// 地图坐标
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public void SetMapPosition (float x, float y)
	{
		
	}

	/// <summary>
	/// 被击中效果
	/// </summary>
	/// <param name="battleAgent">攻击方</param>
	public void HitEffect (BattleAgent attacker)
	{
		float direct = Mathf.Sign( gameObject.transform.position.x - attacker.GameObject.transform.position.x);


		StartCoroutine(BackToIdle());

		GameObject go = battleAgent.GameObject;
		Vector3 pos = go.transform.position;
		go.transform.Translate (3*direct, 0, 0);
		Hashtable args = new Hashtable();
		args.Add("easeType", iTween.EaseType.easeOutQuart);
		args.Add("x",pos.x);
		args.Add("time",0.1f);
		iTween.MoveTo(go,args);

	}
 
	/// <summary>
	/// 恢复受击前状态
	/// </summary>
	/// <returns>The to idle.</returns>
	private IEnumerator BackToIdle ()
	{
		GameObject go = battleAgent.GameObject;
		Image img = go.GetComponent<Image>();
		Color c = img.color;
		Color r = new Color(1,120.0f/255.0f,120.0f/255.0f,1f);
		img.color = r;

		yield return new WaitForSeconds(0.1f);
		
		img.color = Color.white;
	}


}