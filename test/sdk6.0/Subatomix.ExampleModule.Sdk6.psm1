# Copyright Subatomix Research Inc.
# SPDX-License-Identifier: MIT-0
# ^ Remove this header when using this file as a template in other projects.

#Requires -Version 7
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues.Clear()
Set-StrictMode -Version 3.0

# Read all .ps1 files
Join-Path $PSScriptRoot Commands *.ps1 -Resolve | ForEach-Object { . $_ }
