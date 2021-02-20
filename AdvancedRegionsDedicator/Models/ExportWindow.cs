using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvRegionsDesigner.Models
{
    public partial class ExportWindow : Form
    {
        public ExportWindow()
        {
            InitializeComponent();
        }
        public ExportWindow(string data)
        {
            InitializeComponent();
            txtData.Text = data;
        }

        private void ExportWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
