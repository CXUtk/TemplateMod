using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.UI.Coroutines
{
	public class SkipFrame : ICoroutineInstruction
	{
		public SkipFrame() { }

		public bool ShouldWait()
		{
			return false;
		}

		public void Update() { }
	}
}
