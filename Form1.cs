using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
            
            InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
            
            base.OnLoad(e);

            Assembly assembly = Assembly.GetExecutingAssembly();

			locationTextBox.Text = assembly.Location;
            versionTextBox.Text = assembly.GetName().Version.ToString(3);
		}

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForAppUpdates();
        }
        private static async void CheckForAppUpdates()
        {
            try
            {
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/Shawminator/MyApp/releases/tag/MyAppTestUpdate"))
                {
                    Console.WriteLine("Checking for updates.");
                    if (mgr.Result.IsInstalledApp)
                    {
                        Console.WriteLine($"Current Version: v{mgr.Result.CurrentlyInstalledVersion()}");
                        var updates = await mgr.Result.CheckForUpdate();
                        if (updates.ReleasesToApply.Any())
                        {
                            Console.WriteLine("Updates found. Applying updates.");
                            var release = await mgr.Result.UpdateApp();

                            MessageBox.Show("MyApp Updated, Its will now restart.");

                            Console.WriteLine("Updates applied. Restarting app.");
                            UpdateManager.RestartApp();
                        }
                    }
                }
            }
            catch
            {   //log exception and move on
            }
        }
    }
}
