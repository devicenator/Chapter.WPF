﻿// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Windows;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows;

/// <summary>
///     Allows to attach drop items to a control to enable files and folders drag and drop to an ICommand.
/// </summary>
/// <example>
///     <code lang="XAML">
/// <![CDATA[
/// <ListBox AllowDrop="True">
///     <dragNDrop:Drop.DropItem>
///         <dragNDrop:DropItem DropType="Files" DragDropEffect="Copy" Command="{Binding DropFilesCommand}" />
///     </dragNDrop:Drop.DropItem>
///     
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
public static class Drop
{
    /// <summary>
    ///     Identifies the <see cref="GetDropItems(DependencyObject)" />
    ///     <see cref="SetDropItems(DependencyObject, DropItemCollection)" /> attached property.
    /// </summary>
    public static readonly DependencyProperty DropItemsProperty =
        DependencyProperty.RegisterAttached("DropItems", typeof(DropItemCollection), typeof(Drop), new PropertyMetadata(OnDropItemsChanged));

    /// <summary>
    ///     Identifies the <see cref="GetDropItem(DependencyObject)" /> <see cref="SetDropItem(DependencyObject, DropItem)" />
    ///     attached property.
    /// </summary>
    public static readonly DependencyProperty DropItemProperty =
        DependencyProperty.RegisterAttached("DropItem", typeof(DropItem), typeof(Drop), new PropertyMetadata(OnDropItemChanged));

    /// <summary>
    ///     Gets the drop item collection containing all the drop items for a control.
    /// </summary>
    /// <param name="obj">The element from which the property value is read.</param>
    /// <returns>The drop item collection containing all the drop items for a control.</returns>
    public static DropItemCollection GetDropItems(DependencyObject obj)
    {
        return (DropItemCollection)obj.GetValue(DropItemsProperty);
    }

    /// <summary>
    ///     Sets the drop item collection containing all the drop items for a control.
    /// </summary>
    /// <param name="obj">The element from which the property value is set to.</param>
    /// <param name="value">The drop item collection containing all the drop items for a control.</param>
    public static void SetDropItems(DependencyObject obj, DropItemCollection value)
    {
        obj.SetValue(DropItemsProperty, value);
    }

    /// <summary>
    ///     Gets the drop item for a control.
    /// </summary>
    /// <param name="obj">The element from which the property value is read.</param>
    /// <returns>The drop item for a control.</returns>
    public static DropItem GetDropItem(DependencyObject obj)
    {
        return (DropItem)obj.GetValue(DropItemProperty);
    }

    /// <summary>
    ///     Sets the drop item for a control.
    /// </summary>
    /// <param name="obj">The element from which the property value is set to.</param>
    /// <param name="value">The drop item for a control.</param>
    public static void SetDropItem(DependencyObject obj, DropItem value)
    {
        obj.SetValue(DropItemProperty, value);
    }

    private static void OnDropItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = d as UIElement;

        if (e.OldValue != null)
            foreach (var dropItem in (DropItemCollection)e.OldValue)
                dropItem.Detach();

        if (e.NewValue != null)
            foreach (var dropItem in (DropItemCollection)e.NewValue)
                dropItem.Attach(control);
    }

    private static void OnDropItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = d as UIElement;

        ((DropItem)e.OldValue)?.Detach();
        ((DropItem)e.NewValue)?.Attach(control);
    }
}