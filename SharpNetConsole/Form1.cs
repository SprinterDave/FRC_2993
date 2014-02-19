using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.Sockets;

namespace SharpNetConsole
{
    public partial class Form1 : Form
    {
        #region DLL Imports
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();
        [DllImport("kernel32")]
        static extern bool SetConsoleCtrlHandler(HandlerRoutine HandlerRoutine, bool Add);
        delegate bool HandlerRoutine(uint dwControlType);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        internal const UInt32 SC_CLOSE        =0xF060;
        internal const UInt32 MF_BYCOMMAND    =0x00000000;
        #endregion

        #region Member Variables
        /// <summary>
        /// Program version
        /// </summary>
        private Version version = null;
        /// <summary>
        /// The IP Address want to monitor
        /// </summary>
        private IPAddress address = null;
        /// <summary>
        /// The port we want to monitor
        /// </summary>
        private int port = -1;
        /// <summary>
        /// The UDPClient object we'll use for listening
        /// </summary>
        private UdpClient client = null;
        /// <summary>
        /// Indicates if we've started listening
        /// </summary>
        private bool started = false;
        /// <summary>
        /// Indicates if our listening is paused
        /// </summary>
        private bool paused = false;
        #endregion

        #region Member Functions
        /// <summary>
        /// Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            try
            {
                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch (Exception)
            {
                version = new Version(0, 0, 0, 0);
            }
            Text += String.Format( " - (v{0})", version.ToString());


            // Load prior settings
            tbIPAddress.Text = Properties.Settings.Default.address;
            tbPort.Text = Properties.Settings.Default.port.ToString();

            // Fake text changed event up update status labels
            tbIPAddress_TextChanged(tbIPAddress, null);
            tbPort_TextChanged(tbPort, null);
        }

        /// <summary>
        /// Handler for IP address text box changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbIPAddress_TextChanged(object sender, EventArgs e)
        {
            String str = (sender as TextBox).Text;
            if (!IPAddress.TryParse(str, out address))
            {
                labelIPAddressValidInvalid.Text = "Invalid IP Address";
                labelIPAddressValidInvalid.ForeColor = Color.Red;
                address = null;
            }
            else
            {
                labelIPAddressValidInvalid.Text = "Valid IP Address";
                labelIPAddressValidInvalid.ForeColor = Color.Green;
            }
            labelIPAddressValidInvalid.Visible = true;
        }

        /// <summary>
        /// Handler for port text box changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPort_TextChanged(object sender, EventArgs e)
        {
            String str = (sender as TextBox).Text;
            if (!int.TryParse(str, out port) || port < 0 || port > 65535)
            {
                labelPortValidInvalid.Text = "Invalid Port";
                labelPortValidInvalid.ForeColor = Color.Red;
                port = -1;
            }
            else
            {
                labelPortValidInvalid.Text = "Valid Port";
                labelPortValidInvalid.ForeColor = Color.Green;
            }
            labelPortValidInvalid.Visible = true;
        }

        /// <summary>
        /// Start/Stop button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bStartStop_Click(object sender, EventArgs e)
        {
            if (address == null || port == -1)
                return;

            if( started )
                Stop();
            else
                Start();
            started = !started;
        }

        /// <summary>
        /// CTRL-key handler to register with Console
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool ConsoleCtrlHandler(uint type)
        {
            // Return true will cause prior handlers to not be run
            // which will effectively ignore CTRL-C and CTRL-BREAK
            return true;
        }

        /// <summary>
        /// Allocate a console and configure it
        /// </summary>
        private void StartConsole()
        {
            AllocConsole();
            // Set CTRL handler
            SetConsoleCtrlHandler( ConsoleCtrlHandler, true );
            // Remove the 'X' button
            IntPtr hwnd = GetConsoleWindow();
            if (hwnd != null)
            {
                IntPtr hMenu = GetSystemMenu(hwnd, false);
               if (hMenu != null)
                   DeleteMenu(hMenu, SC_CLOSE, MF_BYCOMMAND);
            }
            Console.Title = "Initializing...";
        }

        /// <summary>
        /// Start monitoring UDP broadcasts
        /// </summary>
        private void Start()
        {
            // Start console
            StartConsole();

            // Create a new UDP client
            client = new UdpClient(port);
            //client.ExclusiveAddressUse = false;

            // Start our asynchronous listening
            StartListening();
            // Modify control values appropriately
            {
                bStartStop.Text = "Stop";
                Console.Title = ConsoleTitle();
                tbIPAddress.Enabled = false;
                tbPort.Enabled = false;
                bPauseResume.Visible = true;
            }
            // Unsuspend
            SetPaused(false);
        }

        /// <summary>
        /// Stop monitoring UDP broadcasts
        /// </summary>
        private void Stop()
        {
            // Do nothing if we're not started as we can get called
            // during form close
            if (!started)
                return;
            // Modify control values appropriately
            {
                tbIPAddress.Enabled = true;
                tbPort.Enabled = true;
                bStartStop.Text = "Start";
                bPauseResume.Visible = false;
            }
            // Unsuspend
            SetPaused(false);
            // Close the UDP client
            if (client != null)
            {
                lock (client)
                    client.Close();
            }
            // Free the console
            FreeConsole();
        }

        /// <summary>
        /// Start our asynchronous listening
        /// </summary>
        private void StartListening()
        {
            try
            {
                client.BeginReceive(Receive, this);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Handler for asynchronous UDPClient receives
        /// </summary>
        /// <param name="ar"></param>
        private static void Receive(IAsyncResult ar)
        {
            Form1 f1 = ar.AsyncState as Form1;
            byte[] bytes = null;
            IPEndPoint senderIpEndPoint = null;
            lock( f1.client )
            {
                try
                {
                    // Get the data from the client
                    senderIpEndPoint = new IPEndPoint(0, 0);
                    bytes = f1.client.EndReceive(ar, ref senderIpEndPoint);    
                }
                catch( Exception )
                {
                    return;
                }
            }
            if (f1.address.Equals( IPAddress.Any) || senderIpEndPoint.Address.Equals( f1.address ) )
            {
                // Convert the data to a string (assuming ASCII encoding)
                string message = Encoding.ASCII.GetString(bytes);
                if (!f1.checkBox1.Checked)
                    while (message.Contains("\a"))
                    {
                        message = message.Remove(message.IndexOf("\a"), 1);
                    }
                // Write the message to the console if we're not paused
                if (!f1.paused)
                {
                    Console.Write(message);
                }
            }
            // Kick off another asynchronous listen
            f1.StartListening();
        }

        /// <summary>
        /// Return a string to be used for the console title
        /// </summary>
        /// <returns></returns>
        private string ConsoleTitle()
        {
            if( !address.Equals( IPAddress.Any ) )
                return String.Format("Monitoring UDP Broadcasts from {0}:{1}", address.ToString(), port.ToString());
            else
                return String.Format("Monitoring UDP Broadcasts from port {0} (any IP address)", port.ToString());
        }

        /// <summary>
        /// Pause/Resume button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bPauseResume_Click(object sender, EventArgs e)
        {
            if (!started)
                return;

            SetPaused(!paused);
        }

        /// <summary>
        /// Pause/Resume
        /// </summary>
        /// <param name="state"></param>
        private void SetPaused( bool state )
        {
            paused = state;
            if (paused)
                bPauseResume.Text = "Resume";
            else
                bPauseResume.Text = "Pause";
            Console.Title = ConsoleTitle() + (paused ? " - PAUSED" : "");
        }

        /// <summary>
        /// Form closing handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop any ongoing receives
            Stop();
            // Save current settings if they are valid
            if (address != null )
                Properties.Settings.Default.address = address.ToString();
            if (port != -1 )
                Properties.Settings.Default.port = port;
            Properties.Settings.Default.Save();
        }
        #endregion
    }
}
