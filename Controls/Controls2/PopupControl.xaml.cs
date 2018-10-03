using System.Windows;
using System.Windows.Controls;

namespace Controls.Controls2
{
    public partial class PopupControl : UserControl
    {
        public PopupControl()
        {
            InitializeComponent();
        }

        public bool Visible { get { return (bool)GetValue(VisibleProperty); } set { SetValue(VisibleProperty, value); } }
        public static readonly DependencyProperty VisibleProperty =
            DependencyProperty.Register("Visible", typeof(bool), typeof(PopupControl), new PropertyMetadata(true));
        public FrameworkElement PopupContent { get { return (FrameworkElement)GetValue(PopupContentProperty); } set { SetValue(PopupContentProperty, value); } }
        public static readonly DependencyProperty PopupContentProperty =
            DependencyProperty.Register("PopupContent", typeof(FrameworkElement), typeof(PopupControl), new PropertyMetadata(null));

    }
}
