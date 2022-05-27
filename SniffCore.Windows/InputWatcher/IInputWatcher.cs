// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <summary>
    ///     Listens to native windows events and filters them by the given condition.
    /// </summary>
    public interface IInputWatcher : IDisposable
    {
        /// <summary>
        ///     Registers a callback with filters for the windows messages.
        /// </summary>
        /// <param name="input">The user input to observe.</param>
        /// <returns>The disposable subscribe token to free the callback.</returns>
        SubscribeToken Observe(Input input);

        /// <summary>
        ///     Starts listen to the windows events.
        /// </summary>
        void Start();

        /// <summary>
        ///     Stops listen to the windows events.
        /// </summary>
        void Stop();
    }
}