<#
.SYNOPSIS
    Invokes various build commands.

.DESCRIPTION
    This script is similar to a makefile.

    Copyright Subatomix Research Inc.
    SPDX-License-Identifier: MIT
#>
[CmdletBinding(DefaultParameterSetName="Test")]
param (
    # Clean.
    [Parameter(Mandatory, ParameterSetName="Clean")]
    [switch] $Clean
,
    # Build.
    [Parameter(Mandatory, ParameterSetName="Build")]
    [switch] $Build
,
    # The configuration to build: Debug or Release.  The default is Debug.
    [Parameter(ParameterSetName="Build")]
    [ValidateSet("Debug", "Release")]
    [string] $Configuration = "Debug"
)

#Requires -Version 5
$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

# http://patorjk.com/software/taag/#p=display&f=Slant
Write-Host -ForegroundColor Cyan @'

    ____ Subatomix.Build.Packaging_____ __         ____
   / __ \____ _      _____  _____/ ___// /_  ___  / / /
  / /_/ / __ \ | /| / / _ \/ ___/\__ \/ __ \/ _ \/ / / 
 / ____/ /_/ / |/ |/ /  __/ /   ___/ / / / /  __/ / /  
/_/    \____/|__/|__/\___/_/   /____/_/ /_/\___/_/_/   
        /  |/  /___  ____/ /_  __/ /__                 
       / /|_/ / __ \/ __  / / / / / _ \                
      / /  / / /_/ / /_/ / /_/ / /  __/                
     /_/  /_/\____/\__,_/\__,_/_/\___/                 
'@

function Main {
    if ($Clean) {
        Invoke-Clean
        return
    }

    if (!$NoBuild) {
        Invoke-Build
    }
}

function Invoke-Clean {
    Write-Phase "Clean"
    Invoke-Git -Arguments @(
        "clean", "-fxd",      # Delete all untracked files in directory tree
        "-e", "*.suo",        # Keep Visual Studio <  2015 local options
        "-e", "*.user",       # Keep Visual Studio <  2015 local options
        "-e", ".vs/"          # Keep Visual Studio >= 2015 local options
    )
}

function Invoke-Build {
    Write-Phase "Build"
    Invoke-DotNet build --configuration:$Configuration
}

function Invoke-DotNet {
    param (
        [Parameter(Mandatory, ValueFromRemainingArguments)]
        [string[]] $Arguments
    )
    & dotnet $Arguments
    if ($LASTEXITCODE -ne 0) { throw "dotnet exited with an error." }
}

function Invoke-Git {
    param (
        [Parameter(Mandatory, ValueFromRemainingArguments)]
        [string[]] $Arguments
    )
    & git $Arguments
    if ($LASTEXITCODE -ne 0) { throw "git exited with an error." }
}

function Write-Phase {
    param (
        [Parameter(Mandatory)]
        [string] $Name
    )
    Write-Host "`n===== $Name =====`n" -ForegroundColor Cyan
}

# Invoke Main
try {
    Push-Location $PSScriptRoot
    Main
}
finally {
    Pop-Location
}
