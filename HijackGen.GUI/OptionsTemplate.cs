using System;
using System.Windows.Forms;

namespace HijackGen.GUI
{
    public partial class OptionsTemplate : Form
    {
        public OptionsTemplate()
        {
            InitializeComponent();
        }

        protected string SavePath { get; set; }

        public OperationResult Result { get; protected set; }

        public Exception Exception { get; protected set; }
    }
}
