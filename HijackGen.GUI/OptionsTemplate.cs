using System.Windows.Forms;

namespace HijackGen.GUI
{
    public partial class OptionsTemplate : Form
    {
        public OptionsTemplate()
        {
            InitializeComponent();
        }

        public OperationResult Result { get; protected set; }

        protected string SavePath { get; set; }
    }
}
