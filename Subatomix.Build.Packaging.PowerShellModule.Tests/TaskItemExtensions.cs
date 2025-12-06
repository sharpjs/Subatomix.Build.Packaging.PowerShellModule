// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

namespace Subatomix.Build.Packaging.PowerShellModule;

internal static class TaskItemExtensions
{
    extension (ITaskItem item)
    {
        public ITaskItem WithMetadata(string name, string value)
        {
            item.SetMetadata(name, value);
            return item;
        }
    }
}
