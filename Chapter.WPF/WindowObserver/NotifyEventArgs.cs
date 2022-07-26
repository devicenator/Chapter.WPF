﻿// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Windows;

// ReSharper disable once CheckNamespace

namespace Chapter.WPF;

/// <summary>
///     Holds the data passed when a specific WinAPI message has appear. This is used in the <see cref="WindowObserver" />.
/// </summary>
public sealed class NotifyEventArgs : EventArgs
{
    internal NotifyEventArgs(Window observedWindow, int messageId)
    {
        ObservedWindow = observedWindow;
        MessageId = messageId;
    }

    /// <summary>
    ///     Gets the window which has raised the specific WinAPI message.
    /// </summary>
    public Window ObservedWindow { get; }

    /// <summary>
    ///     Gets the appeared WinAPI message. See <see cref="WindowMessages" />.
    /// </summary>
    public int MessageId { get; }
}