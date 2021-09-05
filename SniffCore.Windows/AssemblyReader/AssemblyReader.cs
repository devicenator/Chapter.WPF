//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.IO;
using System.Reflection;

// ReSharper disable once CheckNamespace

namespace SniffCore.Windows
{
    /// <summary>
    ///     Reads information about the executable.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public class ViewModel
    /// {
    ///     public string GetVersion()
    ///     {
    ///         var version = AssemblyReader.GetExeVersion();
    ///         return $"v{version.ToString(2)}";
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public static class AssemblyReader
    {
        /// <summary>
        ///     Reads the location of the executable.
        /// </summary>
        /// <returns>The location of the executable.</returns>
        public static string GetExeLocation()
        {
            var assembly = Assembly.GetEntryAssembly();
            return Path.GetDirectoryName(assembly.Location);
        }

        /// <summary>
        ///     Reads the version of the executable.
        /// </summary>
        /// <returns>The version of the executable.</returns>
        public static Version GetExeVersion()
        {
            var assembly = Assembly.GetEntryAssembly();
            var assemblyName = assembly.GetName();
            return assemblyName.Version;
        }
    }
}