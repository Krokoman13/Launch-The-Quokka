using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Timer : GameObject
{
    public float maxSeconds;
    float miniSeconds;
    bool active;
    public bool done;

    public float seconds
    {
        get { return miniSeconds/1000; }
    }

    public Timer() : base()
    {
        Start();
    }

    public Timer(float setMaxSeconds) : base()
    {
        maxSeconds = setMaxSeconds;
        Start();
    }

    public void Start()
    {
        done = false;
        miniSeconds = 0;
        active = true;
    }

    public void Start(float maxSecs)
    {
        done = false;
        miniSeconds = 0;
        maxSeconds = maxSecs;
        active = true;
    }

    public void Stop()
    {
        miniSeconds = 0;
        active = false;
    }

    public void Update()
    {
        if (active)
        {
            miniSeconds += Time.deltaTime;

            if (seconds >= maxSeconds && maxSeconds != -1)
            {
                Stop();
                done = true;
            }
        }
    }
}

