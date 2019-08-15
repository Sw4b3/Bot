using Bot.Core.Handlers;
using Bot.Core.Interfaces;
using Bot.Services;
using Bot.Services.Interfaces;
using SimpleInjector;
using System;
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
            container.Register<IBotLogicController, BotLogicController>();
            container.Register<IModuleController, ModuleController>();
            container.Register<IPartsOfSpeechHandler, PartsOfSpeechHandler>();
            container.Register<IApplicationService, ApplicationService>();
            container.Register<IDateTimeService, DateTimeService>();
            container.Register<IInternetService, InternetService>();

            //container.Verify();
        }
    }
}
