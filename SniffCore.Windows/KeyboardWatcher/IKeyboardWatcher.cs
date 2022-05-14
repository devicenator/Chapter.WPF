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
    /// <summary>
    ///     Brings possibilities to listen for keyboard events even the current application has not the focus. Its the so
    ///     called System Keyboard Hooks.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public class MainViewModel : ObservableObject, IDisposable
    /// {
    ///     private IKeyboardWatcher _watcher;
    /// 
    ///     public MainViewModel(IKeyboardWatcher watcher)
    ///     {
    ///         _watcher = watcher;
    /// 
    ///         _watcher.AddCallback(Key.F10, KeyState.Pressed, KeyPressed);
    /// 
    ///         _watcher.Begin();
    ///     }
    /// 
    ///     private void KeyPressed(KeyStateChangedArgs e)
    ///     {
    ///         // User Pressed the F10 key
    ///     }
    ///
    ///     public Dispose()
    ///     {
    ///         _watcher.Dispose();
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public interface IKeyboardWatcher : IDisposable
    {
        /// <summary>
        ///     Occurs when the user pressed or released key.
        /// </summary>
        event EventHandler<KeyStateChangedArgs> KeyStateChanged;

        /// <summary>
        ///     Registers a callback which got called no matter which keys were pressed or released.
        /// </summary>
        /// <param name="callback">The callback to be called.</param>
        /// <returns>A token which represents the current callback to be used for <see cref="RemoveCallback" />.</returns>
        KeyboardWatchToken AddCallback(Action<KeyStateChangedArgs> callback);

        /// <summary>
        ///     Registers a callback which got called no matter which keys came into the specific state.
        /// </summary>
        /// <param name="state">The key state which has to be watched for.</param>
        /// <param name="callback">The callback to be called.</param>
        /// <returns>A token which represents the current callback to be used for <see cref="RemoveCallback" />.</returns>
        KeyboardWatchToken AddCallback(KeyState state, Action<KeyStateChangedArgs> callback);

        /// <summary>
        ///     Registers a callback which got called no matter which keys came into the specific states.
        /// </summary>
        /// <param name="states">The key states which has to be watched for.</param>
        /// <param name="callback">The callback to be called.</param>
        /// <returns>A token which represents the current callback to be used for <see cref="RemoveCallback" />.</returns>
        KeyboardWatchToken AddCallback(IEnumerable<KeyState> states, Action<KeyStateChangedArgs> callback);

        /// <summary>
        ///     Registers a callback which got called if the specific key changed its state.
        /// </summary>
        /// <param name="key">The key which has to be watched for.</param>
        /// <param name="callback">The callback to be called.</param>
        /// <returns>A token which represents the current callback to be used for <see cref="RemoveCallback" />.</returns>
        KeyboardWatchToken AddCallback(Key key, Action<KeyStateChangedArgs> callback);

        /// <summary>
        ///     Registers a callback which got called if one of the given key changed their state.
        /// </summary>
        /// <param name="keys">The keys which has to be watched for.</param>
        /// <param name="callback">The callback to be called.</param>
        /// <returns>A token which represents the current callback to be used for <see cref="RemoveCallback" />.</returns>
        KeyboardWatchToken AddCallback(IEnumerable<Key> keys, Action<KeyStateChangedArgs> callback);

        /// <summary>
        ///     Registers a callback which got called if the specific key changed its state to the given one.
        /// </summary>
        /// <param name="key">The key which has to be watched for.</param>
        /// <param name="state">The key state which has to be watched for.</param>
        /// <param name="callback">The callback to be called.</param>
        /// <returns>A token which represents the current callback to be used for <see cref="RemoveCallback" />.</returns>
        KeyboardWatchToken AddCallback(Key key, KeyState state, Action<KeyStateChangedArgs> callback);

        /// <summary>
        ///     Registers a callback which got called if the specific key changed its state to one of the given states.
        /// </summary>
        /// <param name="key">The key which has to be watched for.</param>
        /// <param name="states">The key states which has to be watched for.</param>
        /// <param name="callback">The callback to be called.</param>
        /// <returns>A token which represents the current callback to be used for <see cref="RemoveCallback" />.</returns>
        KeyboardWatchToken AddCallback(Key key, IEnumerable<KeyState> states, Action<KeyStateChangedArgs> callback);

        /// <summary>
        ///     Registers a callback which got called if one of the given keys changed their state to the specific one.
        /// </summary>
        /// <param name="keys">The keys which has to be watched for.</param>
        /// <param name="state">The key state which has to be watched for.</param>
        /// <param name="callback">The callback to be called.</param>
        /// <returns>A token which represents the current callback to be used for <see cref="RemoveCallback" />.</returns>
        KeyboardWatchToken AddCallback(IEnumerable<Key> keys, KeyState state, Action<KeyStateChangedArgs> callback);

        /// <summary>
        ///     Registers a callback which got called if one of the given keys changed their state to one of the given states.
        /// </summary>
        /// <param name="keys">The keys which has to be watched for.</param>
        /// <param name="states">The key states which has to be watched for.</param>
        /// <param name="callback">The callback to be called.</param>
        /// <returns>A token which represents the current callback to be used for <see cref="RemoveCallback" />.</returns>
        KeyboardWatchToken AddCallback(IEnumerable<Key> keys, IEnumerable<KeyState> states, Action<KeyStateChangedArgs> callback);

        /// <summary>
        ///     Removes a registered callbacks by the token.
        /// </summary>
        /// <param name="token">The token of the registered callback.</param>
        /// <remarks>If the token is not known anymore nothing happen.</remarks>
        void RemoveCallback(KeyboardWatchToken token);

        /// <summary>
        ///     Removes all known callbacks.
        /// </summary>
        void ClearCallbacks();

        /// <summary>
        ///     Begin watching for system wide keyboard events.
        /// </summary>
        void Begin();
    }
}