﻿using FOG.Handlers;

namespace FOG.Modules.PrinterManager
{
    public class LocalPrinter : Printer
    {
        public LocalPrinter(string name, string file, string port, string ip, string model, bool defaulted)
        {
            Name = name;
            Port = port;
            File = file;
            Model = model;
            Default = defaulted;
            IP = ip;
            LogName = "LocalPrinter";
        }

        public override void Add(PrintManagerBridge instance)
        {
            instance.Add(this);
        }
    }
}
