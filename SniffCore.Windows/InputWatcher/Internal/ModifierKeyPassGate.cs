// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows;

internal class ModifierKeyPassGate
{
    private readonly ModifierKeys _modifierKeys;

    public ModifierKeyPassGate(ModifierKeys modifierKeys)
    {
        _modifierKeys = modifierKeys;
    }

    public bool Pass()
    {
        if (_modifierKeys == ModifierKeys.None)
            return true;

        return Keyboard.Modifiers == _modifierKeys;
    }
}