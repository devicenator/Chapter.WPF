// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Windows;

// ReSharper disable once CheckNamespace

namespace Chapter.WPF;

/// <summary>
///     The drop item collection to attach on a control using the <see cref="Drop.DropItemsProperty" />.
/// </summary>
/// <example>
///     <code lang="XAML">
/// <![CDATA[
/// <ListBox AllowDrop="True">
///     <dragNDrop:Drop.DropItems>
///         <dragNDrop:DropItemCollection>
///             <dragNDrop:DropItem DropType="File" DragDropEffect="Copy" Command="{Binding DropFileCommand}" />
///             <dragNDrop:DropItem DropType="Folder" DragDropEffect="Copy" Command="{Binding DropFolderCommand}" />
///         </dragNDrop:DropItemCollection>
///     </dragNDrop:Drop.DropItems>
/// </ListBox>
/// ]]>
/// </code>
/// </example>
public sealed class DropItemCollection : FreezableCollection<DropItem>
{
}