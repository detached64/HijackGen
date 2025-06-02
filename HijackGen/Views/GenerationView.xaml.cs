using CommunityToolkit.Mvvm.Messaging;
using HijackGen.Messengers;
using System.Windows;

namespace HijackGen.Views
{
    public partial class GenerationView : Window
    {
        public GenerationView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<CloseWindowMessage>(this, (_, _) => Close());
        }
    }
}
