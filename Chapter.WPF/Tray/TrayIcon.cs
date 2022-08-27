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
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter.WPF;

/// <summary>
///     Represents the application icon in the tray menu.
/// </summary>
/// <example>
///     <code lang="csharp">
/// <![CDATA[
/// public partial class App
/// {
///     private TrayIcon _icon;
/// 
///     protected override void OnStartup(StartupEventArgs e)
///     {
///         _icon = new TrayIcon(@"C:\MyApp\Icon.ico");
///         _icon.Show();
/// 
///         base.OnStartup(e);
///     }
/// 
///     protected override void OnExit(ExitEventArgs e)
///     {
///         _icon.Dispose();
///         base.OnExit(e);
///     }
/// }
/// ]]>
/// </code>
///     <code lang="XAML">
/// <![CDATA[
/// <sniffcore:TrayIcon Icon="MyApp.exe" ClickCommand="{Binding OpenWindowCommand}">
///     <sniffcore:TrayIcon.ContextMenu>
///         <sniffcore:TrayContextMenu>
///             <sniffcore:TrayContextMenuItem Header="Open" Command="{Binding OpenWindowCommand}" />
///             <sniffcore:TrayContextMenuItem Header="Options" Command="{Binding OptionsCommand}" />
///             <sniffcore:TrayContextSeparator />
///             <sniffcore:TrayContextMenuItem Header="Quit" Command="{Binding ShutdownCommand}" />
///         </sniffcore:TrayContextMenu>
///     </sniffcore:TrayIcon.ContextMenu>
/// </sniffcore:TrayIcon>
/// ]]>
/// </code>
/// </example>
public sealed class TrayIcon : FrameworkElement, IDisposable
{
    /// <summary>
    ///     Identifies the Click routed event.
    /// </summary>
    public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
        "Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TrayIcon));

    /// <summary>
    ///     Identifies the DoubleClick routed event.
    /// </summary>
    public static readonly RoutedEvent DoubleClickEvent = EventManager.RegisterRoutedEvent(
        "DoubleClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TrayIcon));

    /// <summary>
    ///     Identifies the ClickCommand property.
    /// </summary>
    public static readonly DependencyProperty ClickCommandProperty =
        DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(TrayIcon), new PropertyMetadata(null));

    /// <summary>
    ///     Identifies the ClickCommandParameter property.
    /// </summary>
    public static readonly DependencyProperty ClickCommandParameterProperty =
        DependencyProperty.Register("ClickCommandParameter", typeof(object), typeof(TrayIcon), new PropertyMetadata(null));

    /// <summary>
    ///     Identifies the ContextMenu property.
    /// </summary>
    public new static readonly DependencyProperty ContextMenuProperty =
        DependencyProperty.Register("ContextMenu", typeof(TrayContextMenu), typeof(TrayIcon), new PropertyMetadata(OnContextMenuChanged));

    /// <summary>
    ///     Identifies the DoubleClickCommand property.
    /// </summary>
    public static readonly DependencyProperty DoubleClickCommandProperty =
        DependencyProperty.Register("DoubleClickCommand", typeof(ICommand), typeof(TrayIcon), new PropertyMetadata(null));

    /// <summary>
    ///     Identifies the DoubleClickCommandParameter property.
    /// </summary>
    public static readonly DependencyProperty DoubleClickCommandParameterProperty =
        DependencyProperty.Register("DoubleClickCommandParameter", typeof(object), typeof(TrayIcon), new PropertyMetadata(null));

    /// <summary>
    ///     Identifies the Icon property.
    /// </summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(string), typeof(TrayIcon), new PropertyMetadata(OnIconChanged));

    private readonly NotifyIcon _icon;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TrayIcon" /> class.
    /// </summary>
    public TrayIcon()
    {
        _icon = new NotifyIcon();
        _icon.Click += OnIconClick;
        _icon.DoubleClick += OnIconDoubleClick;

        Loaded += OnLoaded;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TrayIcon" /> class.
    /// </summary>
    /// <param name="icon">The path to the tray icon.</param>
    public TrayIcon(string icon)
        : this()
    {
        Icon = icon;
    }

    /// <summary>
    ///     Gets or sets the command to be executed if the user clicks the tray icon.
    /// </summary>
    public ICommand ClickCommand
    {
        get => (ICommand)GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }

    /// <summary>
    ///     Gets or sets the parameter to be passed with the <see cref="Command" />.
    /// </summary>
    public object ClickCommandParameter
    {
        get => GetValue(ClickCommandParameterProperty);
        set => SetValue(ClickCommandParameterProperty, value);
    }

    /// <summary>
    ///     Gets or sets the context menu items of the tray icon.
    /// </summary>
    public new TrayContextMenu ContextMenu
    {
        get => (TrayContextMenu)GetValue(ContextMenuProperty);
        set => SetValue(ContextMenuProperty, value);
    }

    /// <summary>
    ///     Gets or sets the command to be executed if the user double clicks the tray icon.
    /// </summary>
    public ICommand DoubleClickCommand
    {
        get => (ICommand)GetValue(DoubleClickCommandProperty);
        set => SetValue(DoubleClickCommandProperty, value);
    }

    /// <summary>
    ///     Gets or sets the parameter to be passed with the <see cref="DoubleClickCommand" />.
    /// </summary>
    public object DoubleClickCommandParameter
    {
        get => GetValue(DoubleClickCommandParameterProperty);
        set => SetValue(DoubleClickCommandParameterProperty, value);
    }

    /// <summary>
    ///     Gets or sets the path to the tray icon.
    ///     If the file does not exist, the Tray tries to find it by the file name in the application root.
    /// </summary>
    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    ///     Disposes the tray icon.
    /// </summary>
    public void Dispose()
    {
        _icon.Dispose();
    }

    /// <summary>
    ///     Shows the tray icon.
    /// </summary>
    public void Show()
    {
        _icon.Visible = true;
    }

    /// <summary>
    ///     Adds or removes the click event handler.
    /// </summary>
    public event RoutedEventHandler Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    /// <summary>
    ///     Adds or removes the double click event handler.
    /// </summary>
    public event RoutedEventHandler DoubleClick
    {
        add => AddHandler(DoubleClickEvent, value);
        remove => RemoveHandler(DoubleClickEvent, value);
    }

    /// <summary>
    ///     Shows a windows notification.
    /// </summary>
    /// <param name="caption">The caption of the notification.</param>
    /// <param name="content">The message shown in the notification.</param>
    /// <param name="icon">The icon shown in the notification.</param>
    public void ShowNotification(string caption, string content, NotificationIcon icon = NotificationIcon.None)
    {
        _icon.ShowBalloonTip(0, caption, content, (ToolTipIcon)icon);
    }

    private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue == null)
            return;

        var control = (TrayIcon)d;

        var icon = e.NewValue.ToString();
        var location = Assembly.GetEntryAssembly()?.Location;
        var otherIcon = Path.Combine(Path.GetDirectoryName(location)!, icon!);
        if (File.Exists(icon))
            control._icon.Icon = new Icon(icon);
        else if (File.Exists(otherIcon))
            control._icon.Icon = new Icon(otherIcon);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this);
        if (window != null)
            window.Closed += OnWindowClosed;
        Show();
    }

    private void OnWindowClosed(object sender, EventArgs e)
    {
        Dispose();
    }

    private void OnIconClick(object sender, EventArgs e)
    {
        if (ClickCommand != null && ClickCommand.CanExecute(ClickCommandParameter))
            ClickCommand.Execute(ClickCommandParameter);
        RaiseEvent(new RoutedEventArgs(ClickEvent));
    }

    private void OnIconDoubleClick(object sender, EventArgs e)
    {
        if (DoubleClickCommand != null && DoubleClickCommand.CanExecute(DoubleClickCommandParameter))
            DoubleClickCommand.Execute(DoubleClickCommandParameter);
        RaiseEvent(new RoutedEventArgs(DoubleClickEvent));
    }

    private static void OnContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var trayIcon = (TrayIcon)d;
        if (trayIcon.ContextMenu == null)
            return;

        trayIcon._icon.ContextMenuStrip = new ContextMenuStrip();
        foreach (var contextMenuItem in trayIcon.ContextMenu)
            trayIcon._icon.ContextMenuStrip.Items.Add(contextMenuItem.GetToolStripItem());
    }
}

/// <summary>
///     Represents the icon shown in the notification.
/// </summary>
public enum NotificationIcon
{
    /// <summary>
    ///     No Icon.
    /// </summary>
    None = 0,

    /// <summary>
    ///     A Information Icon.
    /// </summary>
    Info = 1,

    /// <summary>
    ///     A Warning Icon.
    /// </summary>
    Warning = 2,

    /// <summary>
    ///     A Error Icon.
    /// </summary>
    Error = 3
}