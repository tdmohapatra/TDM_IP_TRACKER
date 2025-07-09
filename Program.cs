namespace TDM_IP_Tracker
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            Application.Run(new MainForm());
        }


        //static void Main()
        //{
        //    // Initialize application configuration
        //    ApplicationConfiguration.Initialize();

        //    // Load the icon from resources
        //    byte[] iconBytes = Properties.Resources.TDM_NETWORK_APP; // Ensure this matches your resource name
        //    using (MemoryStream ms = new MemoryStream(iconBytes))
        //    {
        //        // Create a new Icon from the byte array and set it as the application's icon
        //        MainForm mainForm = new MainForm
        //        {
        //            Icon = new Icon(ms)
        //        };

        //        // Run the application
        //        Application.Run(mainForm);
        //    }
        //}
    }
}