﻿// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter.WPF;

/// <summary>
///     Creates a filter and callback for native mouse events.
/// </summary>
public class MouseInput : Input
{
    private readonly Action<MouseEventArgs> _callback;
    private readonly ModifierKeyPassGate _modifierKeyPassGate;
    private readonly MouseKeyPassGate _mouseKeyPassGate;

    /// <summary>
    ///     Creates a new MouseInput.
    /// </summary>
    /// <param name="mouseAction">The mouse action.</param>
    /// <param name="callback">The callback.</param>
    public MouseInput(MouseAction mouseAction, Action<MouseEventArgs> callback)
    {
        _mouseKeyPassGate = new MouseKeyPassGate(mouseAction);
        _modifierKeyPassGate = new ModifierKeyPassGate(ModifierKeys.None);
        _callback = callback;
    }

    /// <summary>
    ///     Creates a new MouseInput.
    /// </summary>
    /// <param name="mouseAction">The mouse action.</param>
    /// <param name="modifierKeys">The modifier keys pressed while the mouse action happened.</param>
    /// <param name="callback">The callback.</param>
    public MouseInput(MouseAction mouseAction, ModifierKeys modifierKeys, Action<MouseEventArgs> callback)
    {
        _mouseKeyPassGate = new MouseKeyPassGate(mouseAction);
        _modifierKeyPassGate = new ModifierKeyPassGate(modifierKeys);
        _callback = callback;
    }

    internal override void Handle(WH hookType, IntPtr wParam, IntPtr lParam)
    {
        if (wParam == (IntPtr)WM.MOUSEMOVE)
            return;

        if (hookType == WH.MOUSE_LL &&
            _mouseKeyPassGate.Pass(wParam, lParam) &&
            _modifierKeyPassGate.Pass())
        {
            var args = new MouseEventArgs(Keyboard.Modifiers);
            _callback(args);
        }
    }
}