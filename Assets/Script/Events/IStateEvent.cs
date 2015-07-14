using System;


/// <summary>
/// 动画事件
/// BaseSoldier实现此接口
/// </summary>
public interface IStateEvent
{
	/// <summary>
	/// 触发关键事件
	/// </summary>
	/// <param name="keyId">Key identifier.</param>
	void TriggerKeyEvent(KeyEventId keyId);

}

