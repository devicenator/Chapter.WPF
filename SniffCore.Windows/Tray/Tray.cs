// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <summary>
    ///     Brings possibilities for an easy work with the tray icon.
    /// </summary>
    /// <example>
    ///     <code lang="XAML">
    /// <![CDATA[
    /// <Window sniffCore:Tray.MinimizeToTray="D:\MyApp\Icon.ico">
    /// </Window>
    /// 
    /// <Window sniffCore:Tray.MinimizeToTray="D:\MyApp\MyApp.exe">
    /// </Window>
    /// 
    /// <Window sniffCore:Tray.MinimizeToTray="Icon.ico">
    /// </Window>
    /// 
    /// <Window sniffCore:Tray.MinimizeToTray="MyApp.exe">
    /// </Window>
    /// ]]>
    /// </code>
    /// </example>
    public sealed class Tray
    {
        /// <summary>
        ///     Identifies the <see cref="GetMinimizeToTray(DependencyObject)" />
        ///     <see cref="SetMinimizeToTray(DependencyObject, string)" /> attached property.
        /// </summary>
        public static readonly DependencyProperty MinimizeToTrayProperty =
            DependencyProperty.RegisterAttached("MinimizeToTray", typeof(string), typeof(Tray), new PropertyMetadata(OnMinimizeToTrayChanged));

        private static readonly DependencyProperty BehaviorProperty =
            DependencyProperty.RegisterAttached("Behavior", typeof(Tray), typeof(Tray), new PropertyMetadata(null));

        private readonly NotifyIcon _icon;

        private readonly Window _window;
        private WindowState _originalState;

        private Tray(Window window, string icon)
        {
            _window = window;
            _window.Loaded += OnWindowLoaded;
            _window.Closed += OnWindowClosed;
            _icon = new NotifyIcon();
            _icon.Icon = new Icon(icon);
            _icon.Click += OnIconClick;

            _originalState = _window.WindowState == WindowState.Minimized ? WindowState.Normal : _window.WindowState;
            _window.StateChanged += OnWindowStateChanged;
        }

        /// <summary>
        ///     Gets the path to the icon to show in the tray.
        /// </summary>
        /// <param name="obj">The element from which the property value is read.</param>
        /// <returns>The path to the icon to show in the tray.</returns>
        public static string GetMinimizeToTray(DependencyObject obj)
        {
            return (string) obj.GetValue(MinimizeToTrayProperty);
        }

        /// <summary>
        ///     Sets the path to the icon to show in the tray.
        ///     If the file does not exist, the Tray tries to find it by the file name in the application root.
        /// </summary>
        /// <param name="obj">The element from which the property value is set to.</param>
        /// <param name="value">The path to the icon to show in the tray.</param>
        public static void SetMinimizeToTray(DependencyObject obj, string value)
        {
            obj.SetValue(MinimizeToTrayProperty, value);
        }

        private static void OnMinimizeToTrayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window window))
                throw new InvalidOperationException("The Tray.MinimizeToTray can be attached to a window only.");

            if (e.OldValue != null)
                DisposeOldTray(window);
            if (e.NewValue != null)
                CreateNewTray(window, e.NewValue.ToString());
        }

        private static void DisposeOldTray(Window window)
        {
            var tray = GetBehavior(window);
            if (tray != null)
            {
                tray.Dispose();
                SetBehavior(window, null);
            }
        }

        private static void CreateNewTray(Window window, string icon)
        {
            var behavior = CreateNew(window, icon);
            SetBehavior(window, behavior);
        }

        private static Tray GetBehavior(DependencyObject obj)
        {
            return (Tray) obj.GetValue(BehaviorProperty);
        }

        private static void SetBehavior(DependencyObject obj, Tray value)
        {
            obj.SetValue(BehaviorProperty, value);
        }

        private static Tray CreateNew(Window window, string icon)
        {
            if (File.Exists(icon))
                return new Tray(window, icon);

            var location = Assembly.GetEntryAssembly()?.Location;
            var otherIcon = Path.Combine(Path.GetDirectoryName(location)!, icon);
            return File.Exists(otherIcon) ? new Tray(window, otherIcon) : null;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _icon.Visible = true;
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            _icon.Visible = false;
            _icon.Dispose();
        }

        private void OnWindowStateChanged(object sender, EventArgs e)
        {
            if (_window.WindowState != WindowState.Minimized)
                _originalState = _window.WindowState;
            else
                _window.ShowInTaskbar = false;
        }

        private void OnIconClick(object sender, EventArgs e)
        {
            if (_window.WindowState == WindowState.Minimized)
            {
                _window.ShowInTaskbar = true;
                _window.WindowState = _originalState;
            }

            _window.Activate();
        }

        private void Dispose()
        {
            _icon.Dispose();
        }
    }
}