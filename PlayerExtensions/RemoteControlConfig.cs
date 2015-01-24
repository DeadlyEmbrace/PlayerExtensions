using System;
using System.Windows.Forms;

namespace Mpdn.PlayerExtensions
{
    public partial class RemoteControlConfig : RemoteControlConfigBase
    {
        #region Constructor
        public RemoteControlConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region Protected Methods
        protected override void LoadSettings()
        {
            txbPort.Text = Settings.ConnectionPort.ToString();
            txbDefaultDir.Text = Settings.DefaultDirectory;
        }

        protected override void SaveSettings()
        {
            int portNum = Settings.ConnectionPort;
            var portString = txbPort.Text;
            int.TryParse(portString, out portNum);
            Settings.ConnectionPort = portNum;
            if (!String.IsNullOrEmpty(txbDefaultDir.Text))
                Settings.DefaultDirectory = txbDefaultDir.Text;

        }
        #endregion

        #region Private Methods
        private void ValidateTextBox(TextBox txb)
        {
            int value = 0;
            if (!int.TryParse(txb.Text, out value))
            {
                try
                {
                    int cursorIndex = txb.SelectionStart - 1;
                    txb.Text = txb.Text.Remove(cursorIndex, 1);
                    txb.SelectionStart = cursorIndex;
                    txb.SelectionLength = 0;
                }
                catch (Exception)
                {

                }
            }
        }

        private void ValidatePortNumber()
        {
            var portString = txbPort.Text;
            int port = 0;
            int.TryParse(portString, out port);
            if(port < 0 || port > 65535)
            {
                MessageBox.Show("Please enter a port between 1 and 65535", "Invalid Port Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbPort.Text = Settings.ConnectionPort.ToString();
            }
        }

        private void txbPort_KeyUp(object sender, KeyEventArgs e)
        {
            ValidateTextBox(txbPort);
            ValidatePortNumber();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fBrowserDialog = new FolderBrowserDialog();
            fBrowserDialog.Description = "Select Default video directory";
            if (fBrowserDialog.ShowDialog() == DialogResult.OK)
                txbDefaultDir.Text = fBrowserDialog.SelectedPath;
        }

    }

    public class RemoteControlConfigBase : ScriptConfigDialog<RemoteControlSettings>
    { 
    }
}
