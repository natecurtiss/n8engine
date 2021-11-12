using System;

namespace N8Engine.SceneManagement
{
    // TODO: throw exceptions.
    public sealed class SceneManager : IModule
    {
        readonly Scene[] _scenes;
        int _current;
        
        Type IModule.Type => GetType();

        public SceneManager(Scene[] scenes) => _scenes = scenes;

        void IModule.Initialize()
        {
            for (var i = 0; i < _scenes.Length; i++)
                _scenes[i].UpdateIndex(i);
            _scenes[0].Load();
        }
        void IModule.Update(Time time) { }

        public void Load(int index)
        {
            _current = index;
            _scenes[_current].Load();
        }

        public void Load(string name)
        {
            for (var i = 0; i < _scenes.Length; i++)
                if (_scenes[i].Name == name) Load(i);
        }

        public void LoadCurrent() => Load(_current);
        public void LoadNext() => Load(_current + 1);
        public void LoadPrevious() => Load(_current - 1);
    }
}