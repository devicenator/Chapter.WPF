// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <summary>
    ///     Provides a callback to native windows events.
    /// </summary>
    public class WindowHooks
    {
        private static Proc _proc;
        private Action<int, IntPtr, IntPtr> _callback;
        private IntPtr _hookId;

        /// <summary>
        ///     Creates a new WindowHooks.
        /// </summary>
        public WindowHooks()
        {
            _proc = HookCallback; // Unmanaged callbacks has to be kept alive
            _hookId = IntPtr.Zero;
        }

        /// <summary>
        ///     Hooks a callback into the window event message queue.
        /// </summary>
        /// <param name="process">The process what main module to use.</param>
        /// <param name="callback">The callback executed if a windows message event arrives.</param>
        public void HookIn(Process process, Action<int, IntPtr, IntPtr> callback)
        {
            _callback = callback;

            if (_hookId != IntPtr.Zero)
                return;

            using var module = process.MainModule;
            _hookId = SetWindowsHookEx((int) WH.KEYBOARD_LL, _proc, GetModuleHandle(module?.ModuleName), 0);
        }

        /// <summary>
        ///     Removes the hook.
        /// </summary>
        public void HookOut()
        {
            if (_hookId == IntPtr.Zero)
                return;

            UnhookWindowsHookEx(_hookId);
            _hookId = IntPtr.Zero;
        }

        private IntPtr HookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return CallNextHookEx(_hookId, code, wParam, lParam);

            _callback(code, wParam, lParam);

            return CallNextHookEx(_hookId, code, wParam, lParam);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int hookId, Proc callbackFunction, IntPtr moduleHandle, uint threadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hookId);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hookId, int code, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string moduleName);

        private delegate IntPtr Proc(int code, IntPtr wParam, IntPtr lParam);
    }
}