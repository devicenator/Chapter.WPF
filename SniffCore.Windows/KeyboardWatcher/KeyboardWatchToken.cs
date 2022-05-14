// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <summary>
    ///     Represents a token which stands for a callback in the <see cref="KeyboardWatcher" />.
    /// </summary>
    public class KeyboardWatchToken : IEquatable<KeyboardWatchToken>
    {
        private Guid _guid;

        internal KeyboardWatchToken()
        {
            _guid = Guid.NewGuid();
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise false.</returns>
        public bool Equals(KeyboardWatchToken other)
        {
            return _guid.Equals(other._guid);
        }
    }
}