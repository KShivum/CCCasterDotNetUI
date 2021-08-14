using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Configuration;
using System.Collections.Specialized;
using System.Threading;

namespace CCCaster_Program_Test.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif



            TextBox data = this.FindControl<TextBox>("Data");
            string casterDir = ConfigurationManager.AppSettings.Get("cccasterLocation");

            try
            {
                using (Process cccaster = new Process())
                {
                    cccaster.StartInfo.UseShellExecute = false;
                    cccaster.StartInfo.WorkingDirectory =  casterDir;
                    cccaster.StartInfo.FileName = casterDir + "/cccaster.v3.0.exe";
                    cccaster.StartInfo.Arguments = "%s -n -t %s";
                    //Temp showing windows
                    //cccaster.StartInfo.CreateNoWindow = true;
                    //cccaster.StartInfo.RedirectStandardOutput = true;
                    cccaster.Start();
                    cccaster.WaitForInputIdle();
                    Thread.Sleep(4000);
                    string? line = cccaster.StandardOutput.ReadLine();
                    data.Text = line;
                }
            }
            catch (Exception e)
            {
                data.Text =  e.Message;
            }
        }

        public void OnPointerEnterNavigation(object sender, PointerEventArgs e)
        {
            TabControl navBar = (TabControl)sender;
            navBar.Opacity = 100;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
