#Requires -Version 7
#Requires -PSEdition Core
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues.Clear()
Set-StrictMode -Version Latest

# Read all .ps1 files
Get-ChildItem $PSScriptRoot -Filter *.ps1 | ForEach-Object { . $_ }
