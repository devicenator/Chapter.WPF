// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows;

internal class KeyPassGate
{
    public KeyPassGate(Key key, KeyPressState keyPressState)
    {
        Key = key;
        KeyPressState = keyPressState;
    }

    public Key Key { get; }
    public KeyPressState KeyPressState { get; }

    public bool Pass(IntPtr wParam, IntPtr lParam)
    {
        var key = KeyInterop.KeyFromVirtualKey(Marshal.ReadInt32(lParam));
        if (key != Key)
            return false;

        return KeyPressState switch
        {
            KeyPressState.Down => wParam == (IntPtr)WM.KEYDOWN || wParam == (IntPtr)WM.SYSKEYDOWN,
            KeyPressState.Up => wParam == (IntPtr)WM.KEYUP || wParam == (IntPtr)WM.SYSKEYUP,
            _ => false
        };
    }
}