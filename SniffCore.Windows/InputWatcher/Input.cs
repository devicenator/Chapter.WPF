// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <summary>
    ///     The base class for the mouse or keyboard inputs used in the <see cref="IInputWatcher" />.
    /// </summary>
    public abstract class Input
    {
        internal abstract void Handle(WH hookType, IntPtr wParam, IntPtr lParam);
    }
}