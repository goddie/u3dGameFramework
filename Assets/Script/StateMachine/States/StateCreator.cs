using System;
using UnityEngine;
using System.Reflection;

public class StateCreator
{

	private static StateCreator instance;

	private StateCreator ()
	{
	}

	public static StateCreator GetInstance {
		get {
			if (instance == null) {

				instance = new StateCreator ();
			}
			return instance;
		}
	}
		

	/// <summary>
	/// 创建状态
	/// 后面优化 存放到一个List缓存
	/// </summary>
	/// <returns>The state.</returns>
	/// <param name="stateId">State identifier.</param>
	public BaseState CreateState(StateId stateId)
	{
		string enumName = Enum.GetName (typeof(StateId), stateId) + "State";
			
		Assembly assembly = Assembly.GetExecutingAssembly();
		BaseState obj = (BaseState)assembly.CreateInstance(enumName);

		if (obj != null) {
			//Debug.Log ("reflect "+obj.ToString());
			return obj;
		}

//		if (stateId == StateId.Idle) {
//		
//			return new IdleState ();
//		}



		return new IdleState ();
	}

}