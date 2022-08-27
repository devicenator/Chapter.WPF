// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

// ReSharper disable once CheckNamespace

namespace Chapter.WPF;

/// <summary>
///     The command args containing the data sent with the <see cref="DropItem.Command" />.
/// </summary>
public sealed class ItemDroppedArgs
{
    internal ItemDroppedArgs(string item, object parameter)
    {
        Item = item;
        Parameter = parameter;
    }

    /// <summary>
    ///     Gets the dropped item.
    /// </summary>
    public string Item { get; }

    /// <summary>
    ///     Gets the command parameter.
    /// </summary>
    public object Parameter { get; }
}