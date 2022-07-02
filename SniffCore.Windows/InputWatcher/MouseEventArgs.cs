// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows;

/// <summary>
///     The event parameter after mouse events got received.
/// </summary>
public class MouseEventArgs
{
    internal MouseEventArgs(ModifierKeys modifiers)
    {
        Modifiers = modifiers;
    }

    /// <summary>
    ///     Gets the pressed modifiers.
    /// </summary>
    public ModifierKeys Modifiers { get; }
}