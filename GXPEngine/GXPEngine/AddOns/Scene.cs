using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Scene : Canvas
    {
        private bool _loaded;

        public bool loaded
        {
            get { return _loaded; }
        }

        public Scene(string sceneName) : base(960, 540, false)
        {
            name = sceneName;
            x = 0;
            y = 0;
        }

        public Scene(string sceneName, string backgroundImageFile) : base(backgroundImageFile, false)
        {
            name = sceneName;
            x = 0;
            y = 0;
        }

        public virtual void Update()
        {
        }

        public virtual void onLoad()
        {
            _loaded = true;
            game.LateAddChildAt(this, 1);
        }

        public virtual void onLeave()
        {
            _loaded = false;

            foreach (GameObject other in GetChildren(false))
            {
                other.LateDestroy();
            }

            Remove();
        }

        public string getsceneName()
        { 
            return name;
        }
    }
}
