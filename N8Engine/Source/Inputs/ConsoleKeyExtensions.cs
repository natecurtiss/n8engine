using System;
using System.Collections.Generic;

namespace N8Engine
{
    /// <summary>
    /// Extension methods for converting to and from <see cref="ConsoleKeyInfo">ConsoleKeyInfo,</see>
    /// <see cref="ConsoleKey">ConsoleKey,</see> <see cref="ConsoleModifiers">ConsoleModifiers,</see> and
    /// <see cref="Key">Key.</see>
    /// </summary>
    internal static class ConsoleKeyExtensions
    {
        /// <summary>
        /// Returns an array of <see cref="Key">Keys</see> from a <see cref="ConsoleKeyInfo"/> passed in.
        /// </summary>
        /// <param name="consoleKeyInfo"> The <see cref="ConsoleKeyInfo"/> passed in. </param>
        public static Key[] AsKeys(this in ConsoleKeyInfo consoleKeyInfo)
        {
            List<Key> __keys = new();
            Key __key = consoleKeyInfo.Key.AsKey();
            __keys.Add(__key);
            List<Key> __modifiers = consoleKeyInfo.Modifiers.AsKeys();
            __modifiers.ForEach(modifier => __keys.Add(modifier));
            return __keys.ToArray();
        }

        /// <summary>
        /// Returns a <see cref="Key"/> from a <see cref="ConsoleKey">ConsoleKey.</see>
        /// </summary>
        /// <param name="consoleKey"> The <see cref="ConsoleKey"/> passed in. </param>
        private static Key AsKey(this ConsoleKey consoleKey)
        {
            string __keyName = Enum.GetName(typeof(ConsoleKey), consoleKey);
            if (__keyName == null) return Key.None;
            
            bool __isNumberKey = __keyName.StartsWith('D') && __keyName.EndsWithNumber();
            if (__isNumberKey)
            {
                char __lastCharacter = __keyName[^1];
                return __lastCharacter switch
                {
                    '0' => Key.Zero,
                    '1' => Key.One,
                    '2' => Key.Two,
                    '3' => Key.Three,
                    '4' => Key.Four,
                    '5' => Key.Five,
                    '6' => Key.Six,
                    '7' => Key.Seven,
                    '8' => Key.Eight,
                    '9' => Key.Nine,
                    _ => Key.None
                };
            }
            if (__keyName == "Escape") __keyName = "Esc";
            return (Key) Enum.Parse(typeof(Key), __keyName);
        }

        /// <summary>
        /// Returns true if the <see cref="String"/> ends with a numeric character.
        /// </summary>
        /// <param name="str"> The <see cref="String"/> passed in. </param>
        private static bool EndsWithNumber(this string str) =>  
            str.EndsWith('0') || 
            str.EndsWith('1') || 
            str.EndsWith('2') || 
            str.EndsWith('3') || 
            str.EndsWith('4') || 
            str.EndsWith('5') || 
            str.EndsWith('6') || 
            str.EndsWith('7') || 
            str.EndsWith('8') || 
            str.EndsWith('9');

        /// <summary>
        /// Returns a <see cref="List{T}"/> of <see cref="Key">Keys</see> from a
        /// <see cref="ConsoleModifiers">ConsoleModifiers object.</see>
        /// </summary>
        /// <param name="consoleModifiers"> The <see cref="ConsoleModifiers">ConsoleModifiers object</see>
        /// passed in. </param>
        private static List<Key> AsKeys(this ConsoleModifiers consoleModifiers)
        {
            List<Key> __keys = new();
            if (consoleModifiers.HasFlag(ConsoleModifiers.Alt)) __keys.Add(Key.Alt);
            if (consoleModifiers.HasFlag(ConsoleModifiers.Control)) __keys.Add(Key.Ctrl);
            if (consoleModifiers.HasFlag(ConsoleModifiers.Shift)) __keys.Add(Key.Shift);
            return __keys;
        }
    }
}