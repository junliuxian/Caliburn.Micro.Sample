using System.Windows;
using System.Windows.Controls;

namespace WPF.MEF.Helpers
{
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper), new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            string password = (string)e.NewValue;

            if (passwordBox != null && passwordBox.Password != password)
            {
                passwordBox.Password = password;
            }
        }

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false, OnAttachPropertyChanged));

        public static bool GetAttach(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachProperty, value);
        }

        private static void OnAttachPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            bool attach = (bool)e.NewValue;

            if (attach)
            {
                passwordBox.PasswordChanged += OnPasswordChanged;
            }
            else
            {
                passwordBox.PasswordChanged -= OnPasswordChanged;
            }
        }

        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            string password = GetPassword(passwordBox);

            if (passwordBox != null && passwordBox.Password != password)
            {
                SetPassword(passwordBox, passwordBox.Password);
            }
        }
    }
}
