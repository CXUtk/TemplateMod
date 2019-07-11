using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod.UI.Coroutines
{
	public class WaitForFrames : ICoroutineInstruction
	{
		private uint _counter;
		private readonly int _frames;
		public WaitForFrames(int frames)
		{
			_frames = frames;
			_counter = 0;
		}
		public bool ShouldWait()
		{
			return _counter <= _frames;
		}
		public void Update()
		{
			++_counter;
		}
	}
}
