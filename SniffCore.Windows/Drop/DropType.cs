// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows;

/// <summary>
///     Items allowed to drop within <see cref="DropItem" />.
/// </summary>
public enum DropType
{
    /// <summary>
    ///     The user can drop single files.
    /// </summary>
    File,

    /// <summary>
    ///     The user can drop multiple files.
    /// </summary>
    Files,

    /// <summary>
    ///     The user can drop single folders.
    /// </summary>
    Folder,

    /// <summary>
    ///     The user can drop multiple folders.
    /// </summary>
    Folders,

    /// <summary>
    ///     The user can drop multiple files and folders.
    /// </summary>
    FilesAndFolders
}