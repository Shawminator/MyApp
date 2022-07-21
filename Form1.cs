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
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/Shawminator/MyApp"))
                {
                    var updates = await mgr.Result.CheckForUpdate();
                    if (updates.ReleasesToApply.Any())
                    {
                        MessageBox.Show("Updates found. Applying updates And restarting app");
                        var release = await mgr.Result.UpdateApp();
                        UpdateManager.RestartApp();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Sonething is wrong");
            }
        }
    }
}
