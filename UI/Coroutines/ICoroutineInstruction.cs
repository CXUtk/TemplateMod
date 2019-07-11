using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateMod.UI.Coroutines
{
	/// <summary>
	/// 表示协程操作指令的接口
	/// </summary>
	public interface ICoroutineInstruction
	{
		void Update();
		bool ShouldWait();
	}

}
