using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Controls
{
    public partial class TextBoxWatermark : UserControl
    {
        public TextBoxWatermark()
        {
            InitializeComponent();
        }

        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxWatermark), new FrameworkPropertyMetadata()
            {
                DefaultValue = string.Empty,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged,
                
            });

        public string Watermark { get { return (string)GetValue(WatermarkProperty); } set { SetValue(WatermarkProperty, value); } }
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(TextBoxWatermark), new PropertyMetadata(string.Empty));

        public Brush WatermarkForeground { get { return (Brush)GetValue(WatermarkForegroundProperty); } set { SetValue(WatermarkForegroundProperty, value); } }
        public static readonly DependencyProperty WatermarkForegroundProperty =
            DependencyProperty.Register("WatermarkForeground", typeof(Brush), typeof(TextBoxWatermark), new PropertyMetadata(Brushes.LightGray));

        public Thickness WatermarkIconPadding { get { return (Thickness)GetValue(WatermarkIconPaddingProperty); } set { SetValue(WatermarkIconPaddingProperty, value); } }
        public static readonly DependencyProperty WatermarkIconPaddingProperty =
            DependencyProperty.Register("WatermarkIconPadding", typeof(Thickness), typeof(TextBoxWatermark), new PropertyMetadata(new Thickness(10, 2, 0, 0)));

        public Thickness WatermarkPadding { get { return (Thickness)GetValue(WatermarkPaddingProperty); } set { SetValue(WatermarkPaddingProperty, value); } }
        public static readonly DependencyProperty WatermarkPaddingProperty =
            DependencyProperty.Register("WatermarkPadding", typeof(Thickness), typeof(TextBoxWatermark), new PropertyMetadata(new Thickness(5, 2, 5, 0)));

        public Thickness TextBoxPadding { get { return (Thickness)GetValue(TextBoxPaddingProperty); } set { SetValue(TextBoxPaddingProperty, value); } }
        public static readonly DependencyProperty TextBoxPaddingProperty =
            DependencyProperty.Register("TextBoxPadding", typeof(Thickness), typeof(TextBoxWatermark), new PropertyMetadata(new Thickness(8, 3, 3, 3)));

        public string Icon { get { return (string)GetValue(IconProperty); } set { SetValue(IconProperty, value); } }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(TextBoxWatermark), new PropertyMetadata(string.Empty));

        public Brush WatermarkIconForeground { get { return (Brush)GetValue(WatermarkIconForegroundProperty); } set { SetValue(WatermarkIconForegroundProperty, value); } }
        public static readonly DependencyProperty WatermarkIconForegroundProperty =
            DependencyProperty.Register("WatermarkIconForeground", typeof(Brush), typeof(TextBoxWatermark), new PropertyMetadata(Brushes.Orange));

        public Visibility IconVisibility { get { return (Visibility)GetValue(IconVisibilityProperty); } set { SetValue(IconVisibilityProperty, value); } }
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(TextBoxWatermark), new PropertyMetadata(Visibility.Visible));

        public TextWrapping TextWrapping { get { return (TextWrapping)GetValue(TextWrappingProperty); } set { SetValue(TextWrappingProperty, value); } }
        public static readonly DependencyProperty TextWrappingProperty =
            DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(TextBoxWatermark), new PropertyMetadata(TextWrapping.NoWrap));

        public bool AcceptsReturn { get { return (bool)GetValue(AcceptsReturnProperty); } set { SetValue(AcceptsReturnProperty, value); } }
        public static readonly DependencyProperty AcceptsReturnProperty =
            DependencyProperty.Register("AcceptsReturn", typeof(bool), typeof(TextBoxWatermark), new PropertyMetadata(false));

        public bool SetFocusOnLoad { get { return (bool)GetValue(SetFocusOnLoadProperty); } set { SetValue(SetFocusOnLoadProperty, value); } }
        public static readonly DependencyProperty SetFocusOnLoadProperty =
            DependencyProperty.Register("SetFocusOnLoad", typeof(bool), typeof(TextBoxWatermark), new PropertyMetadata(false));

        public bool SelectAllOnFocus { get { return (bool)GetValue(SelectAllOnFocusProperty); } set { SetValue(SelectAllOnFocusProperty, value); } }
        public static readonly DependencyProperty SelectAllOnFocusProperty =
            DependencyProperty.Register("SelectAllOnFocus", typeof(bool), typeof(TextBoxWatermark), new PropertyMetadata(false));


        private void UC_Loaded(object sender, RoutedEventArgs e)
        {
            if (SetFocusOnLoad)
            {
                SearchTermTextBox.Focus();
                SearchTermTextBox.SelectAll();
            }
        }

        private void UC_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SelectAllOnFocus)
            {
                SearchTermTextBox.SelectAll();
            }
        }
    }
}
