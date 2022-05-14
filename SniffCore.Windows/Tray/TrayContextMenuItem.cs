// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <summary>
    ///     Represents a menu item in the <see cref="TrayContextMenu" />.
    /// </summary>
    public sealed class TrayContextMenuItem : TrayContextItem
    {
        /// <summary>
        ///     Identifies the Command property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(TrayContextMenuItem), new PropertyMetadata(null));

        /// <summary>
        ///     Identifies the CommandParameter property.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(TrayContextMenuItem), new PropertyMetadata(null));

        /// <summary>
        ///     Identifies the Header property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(TrayContextMenuItem), new PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the command to be executed if the user clicks on it.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        ///     Gets or sets the parameter to be passed with the <see cref="Command" />.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        ///     Gets or sets the header text of the menu entry.
        /// </summary>
        public string Header
        {
            get => (string) GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        /// <inheritdoc />
        public override ToolStripItem GetToolStripItem()
        {
            var item = new ToolStripMenuItem(Header, null, OnClick);
            return item;
        }

        /// <summary>
        ///     Raised if the user clicked the menu entry.
        /// </summary>
        public event EventHandler Click;

        private void OnClick(object sender, EventArgs e)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
                Command.Execute(CommandParameter);

            Click?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        protected override Freezable CreateInstanceCore()
        {
            return this;
        }
    }
}