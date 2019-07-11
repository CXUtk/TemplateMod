using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.UI.Coroutines
{
	public class WaitWhile : ICoroutineInstruction
	{
		private Func<bool> _condition;
		public WaitWhile(Func<bool> condition)
		{
			_condition = condition;
		}
		public bool ShouldWait()
		{
			return _condition();
		}
		public void Update()
		{
		}
	}
}
