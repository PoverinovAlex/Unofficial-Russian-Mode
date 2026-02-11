using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unoficial_Russian_Mode
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private Button btnHello;
        private IntPtr baseAddress;

        public MainForm()
        {
            //window settings
            this.Text = "Здорова, ГЕЙмеры";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Blue;

            //generation button
            btnHello = new Button();
            btnHello.Text = "Click me!";
            btnHello.Size = new Size(100, 50);
            btnHello.Location = new Point(145, 50);
            btnHello.BackColor = Color.Red;

            //Add event for button
            btnHello.MouseUp += btnHelloClick;

            //Add button to window
            this.Controls.Add(btnHello);
        }

        public IntPtr GetIntPtr()
        {
            if (baseAddress == IntPtr.Zero)
            {
                MessageBox.Show("Сначала базовый адрес должен быть получен");
                return IntPtr.Zero;
            }
            return baseAddress;
        }

        //generation event for btnHello
        private void btnHelloClick(object sender, MouseEventArgs e)
        {
            //find process address by name
            var processes = Process.GetProcessesByName("Stronghold2");
            if (processes.Length == 0)
            {
                MessageBox.Show("Cannot find process Stronghold 2");
            }
            else
            {
                {
                    foreach (var process in processes)
                    {
                        // get ID process
                        int processId = process.Id;

                        // get module (exe file)
                        ProcessModule mainModule = process.MainModule;

                        // get base address
                        baseAddress = mainModule.BaseAddress;

                        MessageBox.Show($"Process ID: {processId}\n" +
                                      $"Process Name: {process.ProcessName}\n" +
                                      $"Base Address: 0x{baseAddress.ToString("X")}\n" +
                                      $"Module Name: {mainModule.ModuleName}");

                    }
                }
            }
        }
    }

}
