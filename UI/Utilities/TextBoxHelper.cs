using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace UI.Utilities;

public static class TextBoxHelper
{
    // Placeholder текст
    public static string GetPlaceholder(DependencyObject obj) => 
        (string)obj.GetValue(PlaceholderProperty);

    public static void SetPlaceholder(DependencyObject obj, string value) => 
        obj.SetValue(PlaceholderProperty, value);

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.RegisterAttached(
            "Placeholder",
            typeof(string),
            typeof(TextBoxHelper),
            new FrameworkPropertyMetadata(null, OnPlaceholderChanged));

    // Цвет плейсхолдера (по умолчанию LightGray)
    public static Brush GetPlaceholderForeground(DependencyObject obj) => 
        (Brush)obj.GetValue(PlaceholderForegroundProperty);

    public static void SetPlaceholderForeground(DependencyObject obj, Brush value) => 
        obj.SetValue(PlaceholderForegroundProperty, value);

    public static readonly DependencyProperty PlaceholderForegroundProperty =
        DependencyProperty.RegisterAttached(
            "PlaceholderForeground",
            typeof(Brush),
            typeof(TextBoxHelper),
            new FrameworkPropertyMetadata(Brushes.LightGray, OnPlaceholderChanged));

    private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextBox tb)
        {
            tb.TextChanged -= TextBox_TextChanged;
            tb.TextChanged += TextBox_TextChanged;

            if (!tb.IsLoaded)
            {
                tb.Loaded -= TextBox_Loaded;
                tb.Loaded += TextBox_Loaded;
            }

            if (GetOrCreateAdorner(tb, out PlaceholderAdorner adorner))
                adorner.InvalidateVisual();
        }
    }

    private static void TextBox_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox tb)
        {
            tb.Loaded -= TextBox_Loaded;
            GetOrCreateAdorner(tb, out _);
        }
    }

    private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox tb && GetOrCreateAdorner(tb, out PlaceholderAdorner adorner))
        {
            adorner.Visibility = string.IsNullOrEmpty(tb.Text) ? Visibility.Visible : Visibility.Hidden;
        }
    }

    private static bool GetOrCreateAdorner(TextBox tb, out PlaceholderAdorner adorner)
    {
        var layer = AdornerLayer.GetAdornerLayer(tb);
        if (layer == null)
        {
            adorner = null;
            return false;
        }

        adorner = layer.GetAdorners(tb)?.OfType<PlaceholderAdorner>().FirstOrDefault();
        if (adorner == null)
        {
            adorner = new PlaceholderAdorner(tb);
            layer.Add(adorner);
        }
        return true;
    }
}


public class PlaceholderAdorner : Adorner
{
    public PlaceholderAdorner(TextBox textBox) : base(textBox)
    {
        IsHitTestVisible = false;
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        var textBox = (TextBox)AdornedElement;
        var placeholder = TextBoxHelper.GetPlaceholder(textBox);

        if (string.IsNullOrEmpty(placeholder) || !string.IsNullOrEmpty(textBox.Text))
            return;

        var foreground = TextBoxHelper.GetPlaceholderForeground(textBox) ?? Brushes.LightGray;

        var formattedText = new FormattedText(
            placeholder,
            System.Globalization.CultureInfo.CurrentCulture,
            textBox.FlowDirection,
            new Typeface(textBox.FontFamily, textBox.FontStyle, textBox.FontWeight, textBox.FontStretch),
            textBox.FontSize,
            foreground,
            VisualTreeHelper.GetDpi(textBox).PixelsPerDip);

        formattedText.MaxTextWidth = textBox.ActualWidth - textBox.Padding.Left - textBox.Padding.Right - 8;
        formattedText.MaxTextHeight = textBox.ActualHeight;

        var padding = textBox.Padding;
        drawingContext.DrawText(formattedText, new Point(padding.Left+4, padding.Top));
    }
}