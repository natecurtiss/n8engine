using System;
using System.Collections.Generic;

namespace N8Engine.Inputs
{
    internal static class ConsoleKeyExtensions
    {
        public static Key[] AsKeys(this in ConsoleKeyInfo consoleKeyInfo)
        {
            List<Key> __keys = new();
            Key __key = consoleKeyInfo.Key.AsKey();
            __keys.Add(__key);
            List<Key> __modifiers = consoleKeyInfo.Modifiers.AsKeys();
            __modifiers.ForEach(modifier => __keys.Add(modifier));
            return __keys.ToArray();
        }

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
            else
            {
                if (__keyName == "Escape") __keyName = "Esc";
                return (Key) Enum.Parse(typeof(Key), __keyName);
            }
        }

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

        private static List<Key> AsKeys(this ConsoleModifiers consoleModifier)
        {
            List<Key> __keys = new();
            if (consoleModifier.HasFlag(ConsoleModifiers.Alt)) __keys.Add(Key.Alt);
            if (consoleModifier.HasFlag(ConsoleModifiers.Control)) __keys.Add(Key.Ctrl);
            if (consoleModifier.HasFlag(ConsoleModifiers.Shift)) __keys.Add(Key.Shift);
            return __keys;
        }
    }
}