using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Rhoa
{
    public partial class Rhoa : Form
    {         
        public Rhoa()
        {
            SendKeys.SendWait("^c");
            Load += new EventHandler(Window_Load);
            var startingPoint = new System.Drawing.Point(MousePosition.X, MousePosition.Y);
            Top = startingPoint.Y;
            Left = startingPoint.X;
            InitializeComponent();
        }
       
        void Window_Load(object sender, EventArgs e)
        {
            string url = "http://poe.trade";
            
            ControlGenerator ControlGenerator = new ControlGenerator(this, url);    
            bool success = ControlGenerator.GenerateAllControls();
            if (!success)
                return;

            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;
            this.Text = "Rhoa";
            this.Show();
            this.Focus();
            this.TopMost = true;
        }
    }
}

