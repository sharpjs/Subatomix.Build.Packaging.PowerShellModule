// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT
// ^ Remove this header when using this file as a template in other projects.

using System.Management.Automation;

namespace Subatomix.ExampleModule;

[Cmdlet(VerbsDiagnostic.Test, "ModulePackaging")]
public class TestModulePackagingCmdlet : PSCmdlet
{
    protected override void ProcessRecord()
    {
        WriteObject(new ExampleObject());
        WriteObject("It works!");
    }
}
