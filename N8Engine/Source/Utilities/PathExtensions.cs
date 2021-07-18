using System;
using System.IO;

namespace N8Engine.Utilities
{
	public static class PathExtensions
	{
		public static String Combine(this String input, in String single)
			=> Path.Combine(input, single);

		public static String Combine(this String input, params String[] multiple)
		{
			String[] __strings = new String[multiple.Length + 1];

			__strings[0] = input;
			for (Int32 __index = 0; __index < multiple.Length; __index++)
			{
				__strings[__index + 1] = multiple[__index];
			}

			return Path.Combine(__strings);
		}
	}
}