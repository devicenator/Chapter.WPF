// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <inheritdoc />
    public class KeyboardWatcher : IKeyboardWatcher
    {
        private static KeyboardProc _proc;

        private readonly Dictionary<KeyboardWatchToken, KeyboardWatcherCallback> _callbacks;
        private IntPtr _hookId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyboardWatcher" /> class.
        /// </summary>
        public KeyboardWatcher()
        {
            _callbacks = new Dictionary<KeyboardWatchToken, KeyboardWatcherCallback>();
            _proc = HookCallback; // Unmanaged callbacks has to be kept alive
            _hookId = IntPtr.Zero;
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            if (_hookId == IntPtr.Zero)
                return;

            UnhookWindowsHookEx(_hookId);
            _hookId = IntPtr.Zero;
        }

        /// <inheritdoc />
        public event EventHandler<KeyStateChangedArgs> KeyStateChanged;

        /// <inheritdoc />
        public KeyboardWatchToken AddCallback(Action<KeyStateChangedArgs> callback)
        {
            return AddCallback(new Key[0], new[] {KeyState.Pressed, KeyState.Released}, callback);
        }

        /// <inheritdoc />
        public KeyboardWatchToken AddCallback(KeyState state, Action<KeyStateChangedArgs> callback)
        {
            return AddCallback(new Key[0], new[] {state}, callback);
        }

        /// <inheritdoc />
        public KeyboardWatchToken AddCallback(IEnumerable<KeyState> states, Action<KeyStateChangedArgs> callback)
        {
            return AddCallback(new Key[0], states, callback);
        }

        /// <inheritdoc />
        public KeyboardWatchToken AddCallback(Key key, Action<KeyStateChangedArgs> callback)
        {
            return AddCallback(new[] {key}, new[] {KeyState.Pressed, KeyState.Released}, callback);
        }

        /// <inheritdoc />
        public KeyboardWatchToken AddCallback(IEnumerable<Key> keys, Action<KeyStateChangedArgs> callback)
        {
            return AddCallback(keys, new[] {KeyState.Pressed, KeyState.Released}, callback);
        }

        /// <inheritdoc />
        public KeyboardWatchToken AddCallback(Key key, KeyState state, Action<KeyStateChangedArgs> callback)
        {
            return AddCallback(new[] {key}, new[] {state}, callback);
        }

        /// <inheritdoc />
        public KeyboardWatchToken AddCallback(Key key, IEnumerable<KeyState> states, Action<KeyStateChangedArgs> callback)
        {
            return AddCallback(new[] {key}, states, callback);
        }

        /// <inheritdoc />
        public KeyboardWatchToken AddCallback(IEnumerable<Key> keys, KeyState state, Action<KeyStateChangedArgs> callback)
        {
            return AddCallback(keys, new[] {state}, callback);
        }

        /// <inheritdoc />
        public KeyboardWatchToken AddCallback(IEnumerable<Key> keys, IEnumerable<KeyState> states, Action<KeyStateChangedArgs> callback)
        {
            var token = new KeyboardWatchToken();
            var callbackItem = new KeyboardWatcherCallback
            {
                Callback = callback,
                Keys = keys,
                KeyStates = states,
                Token = token
            };
            _callbacks[token] = callbackItem;
            return token;
        }

        /// <inheritdoc />
        public void RemoveCallback(KeyboardWatchToken token)
        {
            if (_callbacks.ContainsKey(token))
                _callbacks.Remove(token);
        }

        /// <inheritdoc />
        public void ClearCallbacks()
        {
            _callbacks.Clear();
        }

        /// <inheritdoc />
        public void Begin()
        {
            _hookId = SetHook();
        }

        private void OnKeyStateChanged(Key key, KeyState state)
        {
            KeyStateChanged?.Invoke(this, new KeyStateChangedArgs(key, state));
        }

        private void NotifyCallbacks(IntPtr keyCode, KeyState state)
        {
            var key = KeyInterop.KeyFromVirtualKey(Marshal.ReadInt32(keyCode));

            var callbacks = _callbacks.Values.Where(c => (c.Keys.Contains(key) || !c.Keys.Any()) && c.KeyStates.Contains(state)).ToList();
            foreach (var callback in callbacks)
                callback.Callback(new KeyStateChangedArgs(key, state));

            OnKeyStateChanged(key, state);
        }

        private IntPtr SetHook()
        {
            using var process = Process.GetCurrentProcess();
            using var module = process.MainModule;
            return SetWindowsHookEx((int) HookType.WH_KEYBOARD_LL, _proc, GetModuleHandle(module?.ModuleName), 0);
        }

        private IntPtr HookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return CallNextHookEx(_hookId, code, wParam, lParam);

            if (wParam == (IntPtr) KeyboardMessages.WM_KEYDOWN || wParam == (IntPtr) KeyboardMessages.WM_SYSKEYDOWN)
                NotifyCallbacks(lParam, KeyState.Pressed);
            else if (wParam == (IntPtr) KeyboardMessages.WM_KEYUP || wParam == (IntPtr) KeyboardMessages.WM_SYSKEYUP)
                NotifyCallbacks(lParam, KeyState.Released);

            return CallNextHookEx(_hookId, code, wParam, lParam);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int hookId, KeyboardProc callbackFunction, IntPtr moduleHandle, uint threadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hookId);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hookId, int code, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string moduleName);

        private delegate IntPtr KeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    }
}