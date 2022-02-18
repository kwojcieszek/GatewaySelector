namespace GatewaySelector
{
    public static class MainClass
    {
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                new Start().SetGateway();
                return;
            }
            else if (args.Length == 1 && args[0] == "-w")
            {
                var app = new GatewaySelector.App();

                app.InitializeComponent();

                app.Run();
            }
        }
    }
}
