using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.UI.Coroutines
{
	public class SwitchTask : ICoroutineInstruction
	{
		public int TaskID { get; }

		public SwitchTask(int taskID) { TaskID = taskID; }

		public bool ShouldWait()
		{
			return false;
		}

		public void Update() { }
	}
}
