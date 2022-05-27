// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Collections.Generic;
using System.Diagnostics;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <inheritdoc />
    public class InputWatcher : IInputWatcher
    {
        private readonly Dictionary<SubscribeToken, Input> _callbacks;
        private readonly WindowHooks _windowHooks;

        /// <summary>
        ///     Creates a new InputWatcher.
        /// </summary>
        public InputWatcher()
        {
            _callbacks = new Dictionary<SubscribeToken, Input>();
            _windowHooks = new WindowHooks();
        }

        /// <inheritdoc />
        public SubscribeToken Observe(Input input)
        {
            var token = new SubscribeToken();
            token.Disposed += OnTokenDisposed;
            _callbacks.Add(token, input);
            return token;
        }

        /// <inheritdoc />
        public void Start()
        {
            using var process = Process.GetCurrentProcess();
            _windowHooks.HookIn(process, EventReceived);
        }

        /// <inheritdoc />
        public void Stop()
        {
            _windowHooks.HookOut();
        }

        /// <summary>
        ///     Stops the input watching and frees all callbacks.
        /// </summary>
        public void Dispose()
        {
            Stop();
            _callbacks.Clear();
        }

        private void OnTokenDisposed(object sender, EventArgs e)
        {
            var token = (SubscribeToken) sender;
            token.Disposed -= OnTokenDisposed;
            _callbacks.Remove(token);
        }

        private void EventReceived(int code, IntPtr wParam, IntPtr lParam)
        {
            foreach (var callback in _callbacks)
                callback.Value.Handle(wParam, lParam);
        }
    }
}