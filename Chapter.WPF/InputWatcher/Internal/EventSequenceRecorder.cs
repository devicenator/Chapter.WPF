// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace

namespace Chapter.WPF;

internal class EventSequenceRecorder
{
    private readonly List<WM> _happened;
    private readonly Stopwatch _stopwatch;
    private List<WM> _sequence;

    public EventSequenceRecorder()
    {
        _stopwatch = new Stopwatch();
        _happened = new List<WM>();
    }

    [DllImport("user32.dll")]
    private static extern uint GetDoubleClickTime();

    public void Sequence(params WM[] events)
    {
        _sequence = events.ToList();
    }

    public bool Pass(WM @event)
    {
        _happened.Add(@event);
        if (_happened.Count == 1)
        {
            _stopwatch.Restart();
            return false;
        }

        var entries = _happened.ToArray();
        if (_happened.Count == _sequence.Count)
        {
            _stopwatch.Stop();
            _happened.Clear();
        }

        return _sequence.SequenceEqual(entries) && _stopwatch.Elapsed.TotalMilliseconds <= GetDoubleClickTime();
    }
}