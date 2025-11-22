# Copyright Subatomix Research Inc.
# SPDX-License-Identifier: MIT

#Requires -Version 7
#Requires -PSEdition Core
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues.Clear()
Set-StrictMode -Version 3.0

# Read all .ps1 files
Join-Path $PSScriptRoot Commands *.ps1 -Resolve | ForEach-Object { . $_ }
