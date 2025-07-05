using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDM_IP_Tracker
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label lblText = new Label() { Left = 20, Top = 20, Text = text, AutoSize = true };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 340 };

            Button btnOk = new Button() { Text = "OK", Left = 280, Width = 80, Top = 75, DialogResult = DialogResult.OK };
            btnOk.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(lblText);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(btnOk);
            prompt.AcceptButton = btnOk;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
        }
    }

}
