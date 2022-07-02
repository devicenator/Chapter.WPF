// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows;

internal class MouseKeyPassGate
{
    private readonly EventSequenceRecorder _recorder;

    public MouseKeyPassGate(MouseAction mouseAction)
    {
        MouseAction = mouseAction;
        _recorder = new EventSequenceRecorder();
        switch (MouseAction)
        {
            case MouseAction.LeftDoubleClick:
                _recorder.Sequence(WM.LBUTTONDOWN, WM.LBUTTONUP, WM.LBUTTONDOWN, WM.LBUTTONUP);
                break;
            case MouseAction.RightDoubleClick:
                _recorder.Sequence(WM.RBUTTONDOWN, WM.RBUTTONUP, WM.RBUTTONDOWN, WM.RBUTTONUP);
                break;
            case MouseAction.MiddleDoubleClick:
                _recorder.Sequence(WM.MBUTTONDOWN, WM.MBUTTONUP, WM.MBUTTONDOWN, WM.MBUTTONUP);
                break;
        }
    }

    public MouseAction MouseAction { get; }

    public bool Pass(IntPtr wParam, IntPtr lParam)
    {
        switch (MouseAction)
        {
            case MouseAction.LeftClick:
                return wParam == (IntPtr)WM.LBUTTONUP;
            case MouseAction.RightClick:
                return wParam == (IntPtr)WM.RBUTTONUP;
            case MouseAction.MiddleClick:
            case MouseAction.WheelClick:
                return wParam == (IntPtr)WM.MBUTTONUP;
            case MouseAction.LeftDoubleClick when wParam == (IntPtr)WM.LBUTTONDOWN:
                return _recorder.Pass(WM.LBUTTONDOWN);
            case MouseAction.LeftDoubleClick when wParam == (IntPtr)WM.LBUTTONUP:
                return _recorder.Pass(WM.LBUTTONUP);
            case MouseAction.RightDoubleClick when wParam == (IntPtr)WM.RBUTTONDOWN:
                return _recorder.Pass(WM.RBUTTONDOWN);
            case MouseAction.RightDoubleClick when wParam == (IntPtr)WM.RBUTTONUP:
                return _recorder.Pass(WM.RBUTTONUP);
            case MouseAction.MiddleDoubleClick when wParam == (IntPtr)WM.MBUTTONDOWN:
                return _recorder.Pass(WM.MBUTTONDOWN);
            case MouseAction.MiddleDoubleClick when wParam == (IntPtr)WM.MBUTTONUP:
                return _recorder.Pass(WM.MBUTTONUP);
            default:
                return false;
        }
    }
}