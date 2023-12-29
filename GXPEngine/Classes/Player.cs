using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine;

public class Player : GameObject
{
    private AnimationSprite _body;

    int bWidth
    {
        get { return _body.width; }
        set { _body.width = value; }
    }

    int bHeight
    {
        get { return _body.height; }
        set { _body.height = value; }
    }

    public Player() : base(true)
    {
    }

    protected override Collider createCollider()
    {
        return new BoxCollider(_body);
    }

    public void Update()
    {
    }
}