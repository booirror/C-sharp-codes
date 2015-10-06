using System;
using System.Diagnostics;
using System.Reflection;



public class CommandLineHandler
{
    public static void Parse(string[] args, object commandLine)
    {
        string err;
        if (!TryParse(args, commandLine, out err))
        {
            throw new ApplicationException(err);
        }
    }

    public static bool TryParse(string[] args, object commandLine, out string errmsg)
    {
        bool sucess = false;
        errmsg = null;
        foreach (string arg in args)
        {
            string option;
            if (arg[0] == '/' || arg[0] == '-')
            {
                string[] optionPart = arg.Split(new char[] { ':' }, 2);
                option = optionPart[0].Remove(0, 1);
                PropertyInfo property = commandLine.GetType().GetProperty(option, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (property != null)
                {
                    if (property.PropertyType == typeof(bool))
                    {
                        property.SetValue(commandLine, true, null);
                        sucess = true;
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(commandLine, optionPart[1], null);
                        sucess = true;
                    }
                    else if (property.PropertyType.IsEnum)
                    {
                        try
                        {
                            property.SetValue(commandLine, Enum.Parse(typeof(ProcessPriorityClass), optionPart[1], true), null);
                            sucess = true;
                        }
                        catch (ArgumentException)
                        {
                            sucess = false;
                            errmsg = string.Format("The option '{0}' is invalid for '{1}'", optionPart[1], option);
                        }
                    }
                    else
                    {
                        sucess = false;
                        errmsg = string.Format("Data type '{0}' on {1} is not"
                            +" supported", property.PropertyType.ToString(), commandLine.GetType().ToString());
                    }
                }
                else
                {
                    sucess = false;
                    errmsg = string.Format("Option '{0}' is not supported", option);
                }
            }
        }
        return sucess;
    }
}
