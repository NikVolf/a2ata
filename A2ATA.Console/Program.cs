using System;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Management.Automation.Runspaces;
using A2ATA.Logics.DataSources;
using Microsoft.PowerShell.Commands;
using Ninject;
using Ninject.Modules;

namespace A2ATA.Console
{

    public class RuntimeinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IContractRegistrator>().ToMethod(x => Program.Storage).InSingletonScope();
            this.Bind<IOfferRegistrator>().ToMethod(x => Program.Storage).InSingletonScope();
            this.Bind<IContractResolver>().ToMethod(x => Program.Storage).InSingletonScope();
            this.Bind<IOfferResolver>().ToMethod(x => Program.Storage).InSingletonScope();
            this.Bind<IActorResolver>().ToMethod(x => Program.Storage).InSingletonScope();
        }
    }

    internal class Program
    {
        internal static FileDataSource Storage = FileDataSource.Load();
        internal static StandardKernel Kernel = new StandardKernel(new RuntimeinjectModule());

        internal static Runspace PowerRunspace;
        internal static PowerShell PowerShell;

        internal static object InstanceLock = new object();

        private static InitialSessionState GetInitialState(Api api)
        {
            var iss = InitialSessionState.Create();
            iss.Variables.Add(new SessionStateVariableEntry("api", api, "Api for datasources"));
            return iss;
        }

        private static void ExecuteHelper(string cmd, object input)
        {
            // Ignore empty command lines.
            if (String.IsNullOrEmpty(cmd))
            {
                return;
            }

            // Create the pipeline object and make it available
            // to the ctrl-C handle through the currentPowerShell instance
            // variable
            lock (InstanceLock)
            {
                PowerShell = PowerShell.Create();
            }

            PowerShell.Runspace = PowerRunspace;


            // Create a pipeline for this execution. Place the result in the 
            // currentPowerShell instance variable so that it is available 
            // to be stopped.
            try
            {
                PowerShell.AddScript(cmd);

                // Now add the default outputter to the end of the pipe and indicate
                // that it should handle both output and errors from the previous
                // commands. This will result in the output being written using the PSHost
                // and PSHostUserInterface classes instead of returning objects to the hosting
                // application.
                PowerShell.AddCommand("out-default");
                PowerShell.Commands.Commands[0].MergeMyResults(PipelineResultTypes.Error, PipelineResultTypes.Output);

                // If there was any input specified, pass it in, otherwise just
                // execute the pipeline.
                if (input != null)
                {
                    PowerShell.Invoke(new object[] {input});
                }
                else
                {
                    PowerShell.Invoke();
                }
            }
            finally
            {
                // Dispose of the pipeline line and set it to null, locked because 
                // currentPowerShell may be accessed by the ctrl-C handler.
                lock (InstanceLock)
                {
                    PowerShell.Dispose();
                    PowerShell = null;
                }
            }
        }

        private static void Main(string[] args)
        {
            var host = new Host();
            var state = GetInitialState(Kernel.Get<Api>());

            var config = RunspaceConfiguration.Create();

            using (PowerRunspace = RunspaceFactory.CreateRunspace(host)) 
            {
                PowerRunspace.Open();
                PowerRunspace.SessionStateProxy.PSVariable.Set("api", Kernel.Get<Api>());
                while (true)
                {
                    try
                    {
                        var cmd = System.Console.ReadLine();
                        if (cmd == "exit")
                            break;
                        ExecuteHelper(cmd, null);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e);
                    }

                }
                PowerRunspace.Close();

            }
        }
    }
}