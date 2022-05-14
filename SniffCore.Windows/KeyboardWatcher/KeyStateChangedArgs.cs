// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <summary>
    ///     Holds all information which was happen to a key received by the <see cref="KeyboardWatcher" />.
    /// </summary>
    public class KeyStateChangedArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyStateChangedArgs" /> class.
        /// </summary>
        /// <param name="key">The key which state has been changed.</param>
        /// <param name="state">The actual state of the key.</param>
        public KeyStateChangedArgs(Key key, KeyState state)
        {
            Key = key;
            State = state;
        }

        /// <summary>
        ///     Gets the key which state has been changed.
        /// </summary>
        public Key Key { get; }

        /// <summary>
        ///     Gets the actual state of the key.
        /// </summary>
        public KeyState State { get; }
    }
}