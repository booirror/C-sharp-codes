using System;
using System.Diagnostics;

public partial class CommandTest
{
    private class CommandLineInfo
    {
        public bool Help { get; set; }
        public string Out { get; set; }

        public ProcessPriorityClass Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        private ProcessPriorityClass _Priority = ProcessPriorityClass.Normal;
    }
}