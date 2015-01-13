using CockBlock8._1.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Main.Instructions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : CB_Page
    {
        private Slider[] _buttonSliders = new Slider[] { new Slider(), new Slider(), new Slider(), new Slider() };
        private Slider[] _textSliders = new Slider[] { new Slider(), new Slider(), new Slider(), new Slider() };
        private Button _buttonColor_bn, _textColor_bn, _buttonSave_bn, _textSave_bn, _example_bn, _reset_bn;
        private TextBlock _example_tx = new TextBlock();
        private byte[] _ARGB = new byte[4];
        public SettingsPage()
        {
            this.InitializeComponent();
            Init();
        }
        private void Init()
        {
            Settings.DefaultTextBlockProperties("Settings", _Title_tx);
            _Settings_panel.Orientation = Orientation.Vertical;
            _Settings_panel.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            _Settings_panel.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;

            //Set to previous settings
            _ARGB[0] = Settings._DefaultButtonForeground.Color.A;
            _ARGB[1] = Settings._DefaultButtonForeground.Color.R;
            _ARGB[2] = Settings._DefaultButtonForeground.Color.G;
            _ARGB[3] = Settings._DefaultButtonForeground.Color.B;

            // Button
            this._buttonColor_bn = new Button();
            this._buttonSave_bn = new Button();
            this._textColor_bn = new Button();
            this._textSave_bn = new Button();
            this._example_bn = new Button();
            this._reset_bn = new Button();
            Settings.DefaultButtonProperties(_buttonColor_bn, _buttonSave_bn, _textColor_bn, _textSave_bn, _example_bn, _reset_bn);
            this._buttonColor_bn.Content = "Button color";
            this._buttonSave_bn.Content = "Save";
            this._textColor_bn.Content = "Textblock color";
            this._textSave_bn.Content = "Save";
            this._example_bn.Content = "Right Color?";
            this._reset_bn.Content = "Reset";
            MethodToButton(this._buttonColor_bn, ButtonColor_bn_Click);
            MethodToButton(this._textColor_bn, TextColor_bn_Click);
            MethodToButton(this._buttonSave_bn, ButtonSaveColor_bn_Click);
            MethodToButton(this._textSave_bn, TextSaveColor_bn_Click);
            MethodToButton(this._reset_bn, Reset_bn_Click);

            // TextBlock
            Settings.DefaultTextBlockProperties(_example_tx);
            _example_tx.Text = "Right Color?";

            // Slider
            ButtonColorSelection(_buttonSliders);
            TextColorSelection(_textSliders);

            // StackPanel
            StackPanel buttonSP = new StackPanel();
            StackPanel textSP = new StackPanel();
            buttonSP.Children.Add(this._example_bn);
            buttonSP.Children.Add(this._buttonSliders[0]);
            buttonSP.Children.Add(this._buttonSliders[1]);
            buttonSP.Children.Add(this._buttonSliders[2]);
            buttonSP.Children.Add(this._buttonSliders[3]);
            buttonSP.Children.Add(this._buttonSave_bn);

            textSP.Children.Add(this._example_tx);
            textSP.Children.Add(this._textSliders[0]);
            textSP.Children.Add(this._textSliders[1]);
            textSP.Children.Add(this._textSliders[2]);
            textSP.Children.Add(this._textSliders[3]);
            textSP.Children.Add(this._textSave_bn);

            this._Settings_panel.Children.Add(this._buttonColor_bn);
            this._Settings_panel.Children.Add(this._textColor_bn);
            this._Settings_panel.Children.Add(this._reset_bn);
            this.ButtonColorPicker.Content = buttonSP;
            this.TextBlockColorPicker.Content = textSP;
        }
        private void MethodToButton(Button button, Action<object, RoutedEventArgs> method)
        {
            if (method != null)
            { button.Click += new RoutedEventHandler(method); }
        }

        private void ButtonColorSelection(params Slider[] sliders)
        {
            for (int i = 0; i < sliders.Length; i++)
            {
                sliders[i] = new Slider();
                sliders[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                sliders[i].Width = 300;
                sliders[i].ValueChanged += button_ValueChanged;
                sliders[i].UpdateLayout();
            }
        }
        private void TextColorSelection(params Slider[] sliders)
        {
            for (int i = 0; i < sliders.Length; i++)
            {
                sliders[i] = new Slider();
                sliders[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                sliders[i].Width = 300;
                sliders[i].ValueChanged += text_ValueChanged;
            }
        }

        private void button_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            _example_bn.Background = SliderToBrush(_buttonSliders);
            _example_bn.Foreground = SliderToBrush(_buttonSliders);
        }
        private void text_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            _example_tx.Foreground = SliderToBrush(_textSliders);
        }
        private void ButtonColor_bn_Click(object sender, RoutedEventArgs e)
        {
            ButtonFlyoutVisible(true);
            SettingsStackPanelVisibility(false);
        }
        private void TextColor_bn_Click(object sender, RoutedEventArgs e)
        {
            TextFlyoutVisible(true);
            SettingsStackPanelVisibility(false);
        }
        private void ButtonFlyoutVisible(bool result)
        {
            Visibility Vis = Windows.UI.Xaml.Visibility.Collapsed;
            if (result)
                Vis = Windows.UI.Xaml.Visibility.Visible;
            this.ButtonColorPicker.Visibility = Vis;
            SliderVisibility(Vis, _buttonSliders);
        }

        private void TextFlyoutVisible(bool result)
        {
            Visibility Vis = Windows.UI.Xaml.Visibility.Collapsed;
            if (result)
                Vis = Windows.UI.Xaml.Visibility.Visible;
            this.TextBlockColorPicker.Visibility = Vis;
            SliderVisibility(Vis, _textSliders);
        }
        private void SettingsStackPanelVisibility(bool result)
        {
            Visibility Vis = Windows.UI.Xaml.Visibility.Collapsed;
            if (result)
                Vis = Windows.UI.Xaml.Visibility.Visible;
            this._buttonColor_bn.Visibility = Vis;
            this._textColor_bn.Visibility = Vis;
        }
        private void SliderVisibility(Visibility Vis, Slider[] sliders)
        {
            foreach (Slider slider in sliders)
            { slider.Visibility = Vis; }
        }

        private void ButtonSaveColor_bn_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush foreground = SliderToBrush(_buttonSliders);
            SolidColorBrush background = SliderToBrush(_buttonSliders);
            background.Color = Color.FromArgb((byte)background.Color.A, (byte)background.Color.R, (byte)background.Color.G, (byte)background.Color.B);
            Settings._DefaultButtonBackground = background;
            Settings._DefaultButtonForeground = foreground;
            SaveColorSettings(Settings._ButtonBackgroundColorKey, Settings._DefaultButtonBackground.Color);
            SaveColorSettings(Settings._ButtonForegroundColorKey, Settings._DefaultButtonForeground.Color);
            this.ButtonColorPicker.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            SettingsStackPanelVisibility(true);
            UpdateSliders();
            ColorChange();
        }
        private void TextSaveColor_bn_Click(object sender, RoutedEventArgs e)
        {
            Settings._DefaultTextForeground = SliderToBrush(_textSliders);
            SaveColorSettings(Settings._TextBlockForegroundColorKey, Settings._DefaultTextForeground.Color);
            this.TextBlockColorPicker.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            SettingsStackPanelVisibility(true);
            UpdateSliders();
            ColorChange();
        }
        private void Reset_bn_Click(object sender, RoutedEventArgs e)
        {
            SaveColorSettings(Settings._ButtonForegroundColorKey, DefaultResetColors()[0]);
            SaveColorSettings(Settings._ButtonBackgroundColorKey, DefaultResetColors()[1]);
            SaveColorSettings(Settings._TextBlockForegroundColorKey, DefaultResetColors()[2]);
            Settings._DefaultButtonForeground = ColorToBrush((int[])ApplicationData.Current.LocalSettings.Values[Settings._ButtonForegroundColorKey]);
            Settings._DefaultButtonBackground = ColorToBrush((int[])ApplicationData.Current.LocalSettings.Values[Settings._ButtonBackgroundColorKey]);
            Settings._DefaultTextForeground = ColorToBrush((int[])ApplicationData.Current.LocalSettings.Values[Settings._TextBlockForegroundColorKey]);
            UpdateSliders();
            ColorChange();
        }

        private SolidColorBrush SliderToBrush(Slider[] sliders)
        {
            byte A = (byte)(((double)255 / 100) * sliders[0].Value);
            byte R = (byte)(((double)255 / 100) * sliders[1].Value);
            byte G = (byte)(((double)255 / 100) * sliders[2].Value);
            byte B = (byte)(((double)255 / 100) * sliders[3].Value);
            return new SolidColorBrush(Color.FromArgb(A, R, G, B));
        }
        private SolidColorBrush ColorToBrush(int[] values)
        {
            byte A = (byte)values[0];
            byte R = (byte)values[1];
            byte G = (byte)values[2];
            byte B = (byte)values[3];
            return new SolidColorBrush(Color.FromArgb(A, R, G, B));
        }
        private void SaveColorSettings(string key, SolidColorBrush brush)
        { SaveColorSettings(key, brush.Color); }
        private void SaveColorSettings(string key, Color color)
        {
            ApplicationData.Current.LocalSettings.Values.Remove(key);
            ApplicationData.Current.LocalSettings.Values[key] = new int[] { color.A, color.R, color.G, color.B };
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateSliders();
        }
        private void UpdateSliders()
        {
            Color color = Settings._DefaultButtonBackground.Color;
            SettingsSliders(_buttonSliders, color);
            color = Settings._DefaultTextForeground.Color;
            SettingsSliders(_textSliders, color);
        }
        private void SettingsSliders(Slider[] sliders, Color color)
        {
            if (color.A != 0)
                sliders[0].Value = (int)(((double)color.A / 255) * 100);
            if (color.R != 0)
                sliders[1].Value = (int)(((double)color.R / 255) * 100);
            if (color.G != 0)
                sliders[2].Value = (int)(((double)color.G / 255) * 100);
            if (color.B != 0)
                sliders[3].Value = (int)(((double)color.B / 255) * 100);
        }
        internal override Button[] GetButtons()
        { return new Button[] { this._buttonColor_bn, this._buttonSave_bn, this._textColor_bn, this._textSave_bn, this._example_bn, this._reset_bn }; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { this._example_tx, this._Title_tx }; }
        private SolidColorBrush[] DefaultResetColors()
        { return new SolidColorBrush[] { new SolidColorBrush(Colors.Gray), new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.White) }; }
    }
}