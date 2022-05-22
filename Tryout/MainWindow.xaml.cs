// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Diagnostics;
using SniffCore.Windows;

namespace Tryout;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        var watcher = new KeyboardWatcher();
        watcher.AddCallback(Callback);
        watcher.Begin();
    }

    private void Callback(KeyStateChangedArgs obj)
    {
        Debug.WriteLine(obj.State);
    }
}