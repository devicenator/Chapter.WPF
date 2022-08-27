// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Windows;
using System.Windows.Forms;

// ReSharper disable once CheckNamespace

namespace Chapter.WPF;

/// <summary>
///     Represents an item in the <see cref="TrayContextMenu" />.
/// </summary>
public abstract class TrayContextItem : Freezable
{
    /// <summary>
    ///     Creates the windows forms tool strip item.
    /// </summary>
    /// <returns>The windows forms tool strip item.</returns>
    public abstract ToolStripItem GetToolStripItem();
}