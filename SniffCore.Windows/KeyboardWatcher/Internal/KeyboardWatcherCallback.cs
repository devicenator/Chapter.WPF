// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Collections.Generic;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    internal class KeyboardWatcherCallback
    {
        internal KeyboardWatchToken Token { get; set; }

        internal Action<KeyStateChangedArgs> Callback { get; set; }
        internal IEnumerable<Key> Keys { get; set; }
        internal IEnumerable<KeyState> KeyStates { get; set; }
    }
}