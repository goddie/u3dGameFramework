using UnityEngine;
using System.Collections;

public class GlobalConfig
{

	private static GlobalConfig instance;
	
	private GlobalConfig ()
	{
	}
	
	public static GlobalConfig GetInstance {
		get {
			if (instance == null) {
				
				instance = new GlobalConfig ();
			}
			return instance;
		}
	}


	public void InitGlobalSetting ()
	{
		Application.targetFrameRate = 30;
	}


	public const float cameraFar = 10.0f;
}
