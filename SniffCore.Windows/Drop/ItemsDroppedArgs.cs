// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <summary>
    ///     The command args containing the data sent with the <see cref="DropItem.Command" />.
    /// </summary>
    public sealed class ItemsDroppedArgs
    {
        internal ItemsDroppedArgs(string[] items, object parameter)
        {
            Items = items;
            Parameter = parameter;
        }

        /// <summary>
        ///     Gets the dropped items.
        /// </summary>
        public string[] Items { get; }

        /// <summary>
        ///     Gets the command parameter.
        /// </summary>
        public object Parameter { get; }
    }
}