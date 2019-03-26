using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProject
{
    /// <summary>
    /// Password添加绑定属性
    /// </summary>
    public static class PasswordBoxBindingHelper
    {
        //BindedPassword
        public static readonly DependencyProperty BindedPasswordProperty =
            DependencyProperty.RegisterAttached("BindedPassword",
            typeof(string), typeof(PasswordBoxBindingHelper),
            new FrameworkPropertyMetadata(string.Empty, OnBindedPasswordChanged));

        public static string GetBindedPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(BindedPasswordProperty);
        }
        public static void SetBindedPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BindedPasswordProperty, value);
        }

        private static void OnBindedPasswordChanged(DependencyObject sender,
          DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox == null)
            {
                return;
            }
            passwordBox.PasswordChanged -= PasswordChanged;

            if (!(bool)GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }


        //IsPasswordBindingEnabled
        public static readonly DependencyProperty IsPasswordBindingEnabledProperty =
            DependencyProperty.RegisterAttached("IsPasswordBindingEnabled",
            typeof(bool), typeof(PasswordBoxBindingHelper), new PropertyMetadata(false, IsPasswordBindingEnabled));
        public static void SetIsPasswordBindingEnabled(DependencyObject dp, bool value)
        {
            dp.SetValue(IsPasswordBindingEnabledProperty, value);
        }

        public static bool GetIsPasswordBindingEnabled(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsPasswordBindingEnabledProperty);
        }
        private static void IsPasswordBindingEnabled(DependencyObject sender,
           DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            if (passwordBox == null)
                return;

            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        //IsUpdating
        private static readonly DependencyProperty IsUpdatingProperty =
           DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
           typeof(PasswordBoxBindingHelper));

        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }

        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            SetBindedPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
    }
}

