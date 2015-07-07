using System;

public class EventCenter : EventDispatcherBase
{
	private static EventCenter instance;

	private EventCenter()
	{
		
	}

	public static EventCenter GetInstance {
		get {
			if (instance == null) {
				 
				instance =new EventCenter();
			}
			return instance;
		}
	}
}
