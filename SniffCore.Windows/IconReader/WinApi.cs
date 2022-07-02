// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows;

internal static class WinApi
{
    private const int MaxPath = 256;

    public const uint SHGFI_ICON = 0x000000100;
    public const uint SHGFI_LARGEICON = 0x000000000;
    public const uint SHGFI_SMALLICON = 0x000000001;
    public const uint SHGFI_USEFILEATTRIBUTE = 0x000000010;
    public const uint FileAttributeNormal = 0x00000080;

    [DllImport("shell32.dll")]
    internal static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

    [DllImport("User32.dll")]
    internal static extern int DestroyIcon(IntPtr hIcon);

    [StructLayout(LayoutKind.Sequential)]
    internal struct SHFILEINFO
    {
        private const int Namesize = 80;
        public readonly IntPtr hIcon;
        private readonly int iIcon;
        private readonly uint dwAttributes;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxPath)]
        private readonly string szDisplayName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Namesize)]
        private readonly string szTypeName;
    }
}