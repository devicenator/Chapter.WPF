// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Windows;
using System.Windows.Forms;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <summary>
    ///     Represents a separator line in the <see cref="TrayContextMenu" />.
    /// </summary>
    public sealed class TrayContextSeparator : TrayContextItem
    {
        /// <inheritdoc />
        public override ToolStripItem GetToolStripItem()
        {
            return new ToolStripSeparator();
        }

        /// <inheritdoc />
        protected override Freezable CreateInstanceCore()
        {
            return this;
        }
    }
}