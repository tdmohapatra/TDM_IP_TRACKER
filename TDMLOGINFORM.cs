using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class TDMLOGINFORM : Form
    {
 
        public TDMLOGINFORM()
        {
            InitializeComponent();
            ApplyTheme();
            SetupAnimations();
            // Display today's password format in the UI (for demo purposes)
            lblPasswordHint.Text = $"Today's password format: KHULJASIMSIM{DateTime.Now:ddMMyyyy}";
            lblPasswordHint.Visible = true;

            // Set KeyPreview to true to capture Enter key
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(TDMLOGINFORM_KeyDown);
        }

  

        private void TDMLOGINFORM_Load(object sender, EventArgs e)
        {

        }
    }
}
