using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum LightColor
{
    Red,
    Yellow,
    Green
}

class TrafficLight
{
    public static void WhatInfo(LightColor color)
    {
        switch (color)
        {
            case LightColor.Red:
                Console.WriteLine(LightColor.Red.ToString());
                Console.WriteLine("Stop");
                break;
            case LightColor.Yellow:
                Console.WriteLine("warning");
                break;
            case LightColor.Green:
                Console.WriteLine("Go");
                break;
            default:
                break;
        }
    }
}

