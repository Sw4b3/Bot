﻿using Bot.Core.Handlers;
using Bot.Core.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot.Core.Desktop
{
    static class Program
    {
        private static Container container;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            Application.Run(container.GetInstance<MainForm>());
        }

        private static void Bootstrap()
        {
            container = new Container();

            container.Register<MainForm>();
            container.Register<ISpeechController, SpeechController>();
            container.Register<INaturalLanguageProcessor, NaturalLanguageProcessor>();
            container.Register<IRecogntionController, RecogntionController>();
            container.Register<ILanguageProcessor, LanguageProcessor>();
            container.Register<IModuleController, ModuleController>();
            container.Register<IPOSHandler, POSHandler>();

            //container.Verify();
        }
    }
}
