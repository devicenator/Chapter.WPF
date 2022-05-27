// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SniffCore.Windows;
using KeyboardEventArgs = SniffCore.Windows.KeyboardEventArgs;

namespace Tryout;

public partial class MainWindow
{
    private readonly InputWatcher _watcher;
    private readonly SubscribeToken _subscribeToken;

    public MainWindow()
    {
        InitializeComponent();

        Loaded += OnLoaded;

        _watcher = new InputWatcher();
        _subscribeToken = _watcher.Observe(new KeyboardInput(Key.A, OnKeyPress));

        //MouseAction
        //ModifierKeys
    }

    private void OnKeyPress(KeyboardEventArgs e)
    {
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _watcher.Start();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        _watcher.Dispose();
        base.OnClosing(e);
    }
}