using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Managers
{
    public class SceneManager
    {
        private List<Scene> _scenes;
        private int _currentsceneNumber;
        private Scene _previousscene;

        public SceneManager()
        {
            reset();
        }

        public void addscene(Scene scene)
        {
            _scenes.Add(scene);

            Console.WriteLine("Added scene: " + scene + " As number: " + _scenes.Count);

            if (_scenes.Count == 1)
            {
                scene.onLoad();
            }
        }

        public void SetScene(Scene scene)
        {
            SetScene(scene.name);
        }

        public void SetScene(string scene)
        {
            gotoScene(findScene(scene));
        }

        private void gotoScene(int nextscene)
        {
            if (_currentsceneNumber != nextscene)
            {
                _scenes[_currentsceneNumber].onLeave();
                _previousscene = _scenes[_currentsceneNumber];

                _currentsceneNumber = nextscene;

                if (!_scenes[_currentsceneNumber].loaded)
                {
                    _scenes[_currentsceneNumber].onLoad();
                }
            }
        }

        private int findScene(string scene)
        {
            for (int i = 0; i < _scenes.Count(); i++)
            {
                if (_scenes[i].name == scene)
                {
                    return i;
                }
            }
            Console.WriteLine("Could not find scene: " + scene);
            return _currentsceneNumber;
        }

        public void GotoNextscene()
        {
            if (_currentsceneNumber < _scenes.Count - 1)
            {
                gotoScene(_currentsceneNumber + 1);
            }
        }

        public void GotoPreviousscene()
        {
            if (_previousscene != null)
            {
                SetScene(_previousscene);
                _previousscene = null;
            }
        }

        public Scene GetCurrentscene()
        {
            return _scenes[_currentsceneNumber];
        }

        public void Reloadscene()
        {
            _scenes[_currentsceneNumber].onLeave();
            _scenes[_currentsceneNumber].onLoad();
        }

        public void reset()
        {
            _scenes = new List<Scene>();
            _currentsceneNumber = 0;
        }
    }
}
