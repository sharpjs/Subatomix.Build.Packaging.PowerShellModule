// Copyright Subatomix Research Inc.
// SPDX-License-Identifier: MIT

// The assembly under test contains nullability attribute polyfills that would
// conflict with the attributes provided by the test suite's target framework.
// Use an extern alias to avoid brining the polyfills into scope.
extern alias Subject;
global using Subject::Subatomix.Build.Packaging.PowerShellModule;

[assembly: Parallelizable(ParallelScope.All)]
