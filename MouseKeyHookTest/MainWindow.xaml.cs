using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace MouseKeyHookTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IKeyboardMouseEvents _keyboardMouseEvents;
        private bool _suppressMoveEvents = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            _keyboardMouseEvents = Hook.GlobalEvents();
            if (_keyboardMouseEvents != null)
            {
                _keyboardMouseEvents.MouseDownExt += OnMouseDownExt;
                _keyboardMouseEvents.MouseUpExt += OnMouseUpExt;
                _keyboardMouseEvents.MouseWheelExt += OnMouseWheelExt;
                _keyboardMouseEvents.MouseMoveExt += OnMouseMoveExt;
            }
        }

        private void Window_Closed(object sender, EventArgs eventArgs)
        {
            if (_keyboardMouseEvents != null)
            {
                _keyboardMouseEvents.MouseDownExt -= OnMouseDownExt;
                _keyboardMouseEvents.MouseUpExt -= OnMouseUpExt;
                _keyboardMouseEvents.MouseWheelExt -= OnMouseWheelExt;
                _keyboardMouseEvents.MouseMoveExt -= OnMouseMoveExt;
                _keyboardMouseEvents.Dispose();
                _keyboardMouseEvents = null;
            }
        }

        private void OnMouseMoveExt(object sender, MouseEventExtArgs eventArgs)
        {
            eventArgs.Handled = _suppressMoveEvents;
        }

        private void OnMouseWheelExt(object sender, MouseEventExtArgs eventArgs)
        {
            eventArgs.Handled = _suppressMoveEvents;
        }

        private void OnMouseUpExt(object sender, MouseEventExtArgs eventArgs)
        {
            if (eventArgs.IsMouseButtonUp && eventArgs.Button == MouseButtons.Left)
            {
                _suppressMoveEvents = false;
            }
        }

        private void OnMouseDownExt(object sender, MouseEventExtArgs eventArgs)
        {
            if (eventArgs.IsMouseButtonDown && eventArgs.Button == MouseButtons.Left)
            {
                _suppressMoveEvents = true;
            }
        }
    }
}
