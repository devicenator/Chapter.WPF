// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows;

/// <summary>
///     Represents a press state.
/// </summary>
public enum KeyPressState
{
    /// <summary>
    ///     The key was pressed.
    /// </summary>
    Down,

    /// <summary>
    ///     The key was released.
    /// </summary>
    Up
}