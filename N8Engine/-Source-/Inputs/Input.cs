using System;
using System.Collections.Generic;
using N8Engine.External;
using N8Engine.External.User;
using N8Engine.Internal;

namespace N8Engine.Inputs
{
    /// <summary>
    /// Just like <see cref="Application">Application,</see>
    /// I suggest going to your search engine of choice to find out what "input" is in the context of a game engine if you do not already know.
    /// </summary>
    /// <seealso cref="Key"/>
    public sealed class Input
    {
        enum KeyState
        {
            IsPressed, 
            IsReleased, 
            WasJustPressed, 
            WasJustReleased
        }

        readonly Dictionary<Key, KeyState> _keyStates = new();
        readonly IEnumerable<Key> _allKeys = Enum.GetValues<Key>();
        readonly IInternalEvents _internalEvents;
        
        internal Input(IInternalEvents internalEvents)
        {
            _internalEvents = internalEvents;
            foreach (var key in _allKeys) 
                _keyStates.Add(key, KeyState.IsReleased);
            _internalEvents.OnInternalPreUpdate += EveryFrame;
        }

        ~Input() => _internalEvents.OnInternalPreUpdate -= EveryFrame;

        /// <summary>
        /// True if the <see cref="Key"/> is currently not being pressed (the opposite of <see cref="IsPressed">IsPressed.</see>)
        /// </summary>
        public bool IsReleased(Key key) => _keyStates[key] == KeyState.IsReleased;
        
        /// <summary>
        /// True if the <see cref="Key"/> is currently being pressed (the opposite of <see cref="IsReleased">IsReleased.</see>)
        /// </summary>
        public bool IsPressed(Key key) => _keyStates[key] == KeyState.IsPressed;
        
        /// <summary>
        /// True if the<see cref="Key"/> was just released in the current frame.
        /// </summary>
        public bool WasJustReleased(Key key) => _keyStates[key] == KeyState.WasJustReleased;
        
        /// <summary>
        /// True if the <see cref="Key"/> was just pressed in the current frame.
        /// </summary>
        public bool WasJustPressed(Key key) => _keyStates[key] == KeyState.WasJustPressed;

        void EveryFrame()
        {
            foreach (var key in _allKeys)
            {
                var previousKeyState = _keyStates[key];
                var newKeyState = KeyState.IsReleased;
                var isKeyDownNow = UserInput.IsKeyDown(key);
                newKeyState = previousKeyState switch
                {
                    KeyState.IsPressed => isKeyDownNow ? KeyState.IsPressed : KeyState.WasJustReleased,
                    KeyState.IsReleased => isKeyDownNow ? KeyState.WasJustPressed : KeyState.IsReleased,
                    KeyState.WasJustPressed => isKeyDownNow ? KeyState.IsPressed : KeyState.WasJustReleased,
                    KeyState.WasJustReleased => isKeyDownNow ? KeyState.WasJustPressed : KeyState.IsReleased,
                    var _ => newKeyState
                };
                _keyStates[key] = newKeyState;
            }
        }
    }
}