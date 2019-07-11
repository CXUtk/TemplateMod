using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateMod.UI.Coroutines
{
	public class Coroutine
	{
		private IEnumerator<ICoroutineInstruction> currentTask;

		public Coroutine(IEnumerator<ICoroutineInstruction> task)
		{
			if (task == null) return;
			currentTask = task;
			currentTask.MoveNext();
		}

		public void Update()
		{
			if (currentTask == null) return;
			RunCoroutine();
		}

		private void RunCoroutine()
		{
			var task = currentTask;
			if (task == null)
				return;
			var ins = task.Current;

			ins.Update();
			if (ins.ShouldWait())
				return;
			task.MoveNext();
		}
	}
}
