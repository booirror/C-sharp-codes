using System;
class Cooler
{
    public Cooler(float temperature)
    {
        Temperature = temperature;
    }

    public float Temperature
    {
        get;
        set;
    }

    public void OnTemperatureChanged(float newTemperature)
    {
        if (newTemperature > Temperature)
        {
            System.Console.WriteLine("Cooler on");
        }
        else
        {
            System.Console.WriteLine("Cooler off");
        }
    }
}

class Heater
{
    public Heater(float temperature)
    {
        Temperature = temperature;
    }

    public float Temperature
    {
        get;
        set;
    }

    public void OnTemperatureChanged(float newTemperature)
    {
        if (newTemperature > Temperature)
        {
            System.Console.WriteLine("Heater off");
        }
        else
        {
            System.Console.WriteLine("Heater on");
        }
    }
}

public class Thermostat
{
    public Action<float> OnTemperatureChange { get; set; }

    public float CurrentTemperature
    {
        get { return _CurrentTemperature; }
        set
        {
            if (value != CurrentTemperature)
            {
                _CurrentTemperature = value;
            }
        }
    }
    private float _CurrentTemperature;
}