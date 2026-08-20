#Requires -Version 5.1
<#
.SYNOPSIS
  Repository/source/configuration checks from the instruction files.

.DESCRIPTION
  Complements .NET tests and analyzers. Does not rewrite source.
  C# architecture, naming styles, Console/ILogger usage, and structured
  log templates are enforced by dotnet build / dotnet test.

  Run from the repository root:
    .\.github\scripts\validate-instructions.ps1
#>
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

if ($PSVersionTable.PSVersion.Major -ge 7) {
    [Console]::OutputEncoding = [System.Text.Encoding]::UTF8
}

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$violations = New-Object System.Collections.Generic.List[object]

function Add-Violation {
    param(
        [Parameter(Mandatory = $true)][string] $Rule,
        [Parameter(Mandatory = $true)][string] $Issue,
        [string] $File = '',
        [string] $Line = ''
    )

    $violations.Add([pscustomobject]@{
            Rule  = $Rule
            File  = $File
            Line  = $Line
            Issue = $Issue
        }) | Out-Null
}

function Get-RelativePath([string] $fullPath) {
    $full = [System.IO.Path]::GetFullPath($fullPath)
    $root = [System.IO.Path]::GetFullPath($repoRoot)
    if (-not $root.EndsWith([System.IO.Path]::DirectorySeparatorChar)) {
        $root += [System.IO.Path]::DirectorySeparatorChar
    }

    if ($full.StartsWith($root, [System.StringComparison]::OrdinalIgnoreCase)) {
        return $full.Substring($root.Length).Replace('\', '/')
    }

    return $full.Replace('\', '/')
}

function Test-ExcludedPath([string] $fullPath) {
    $parts = $fullPath -split '[\\/]'
    foreach ($part in $parts) {
        if ($part -in @('bin', 'obj', '.git', '.vs', 'node_modules')) {
            return $true
        }
    }
    return $false
}

function Test-TestPath([string] $fullPath) {
    $relative = Get-RelativePath $fullPath
    foreach ($part in ($relative -split '/')) {
        if ($part -match 'Test') {
            return $true
        }
    }
    return $false
}

function Get-SourceLines([string] $fullPath) {
    return [System.IO.File]::ReadAllLines($fullPath)
}

function Test-CommentOrStringNoise([string] $line) {
    $trimmed = $line.TrimStart()
    return $trimmed.StartsWith('//') -or $trimmed.StartsWith('*') -or $trimmed.StartsWith('#')
}

function Test-PlaceholderValue([string] $value) {
    if ([string]::IsNullOrWhiteSpace($value)) { return $true }
    $normalized = $value.Trim()
    $placeholders = @(
        '*', '***', 'xxxx', 'TODO', 'CHANGE_ME', 'CHANGEME', 'REPLACE_ME',
        'YOUR_SECRET', 'YOUR-SECRET', '<secret>', '{Secret}', 'secret',
        'password', 'Password', 'PLACEHOLDER'
    )
    foreach ($item in $placeholders) {
        if ($normalized.Equals($item, [System.StringComparison]::OrdinalIgnoreCase)) {
            return $true
        }
    }
    if ($normalized -match '^\$\{' -or $normalized -match '^%' -or $normalized.StartsWith('<')) {
        return $true
    }
    return $false
}

# ---------------------------------------------------------------------------
# Required enforcement / configuration files (repository-level)
# ---------------------------------------------------------------------------
$requiredFiles = @(
    @{ Path = '.github/instructions/architecture.md'; Why = 'Architecture instruction file is required.' },
    @{ Path = '.github/instructions/logging.md'; Why = 'Logging instruction file is required.' },
    @{ Path = '.github/instructions/naming-conventions.md'; Why = 'Naming instruction file is required.' },
    @{ Path = '.editorconfig'; Why = '.editorconfig is required to enforce naming conventions in the build.' },
    @{ Path = 'Directory.Build.props'; Why = 'Directory.Build.props is required to enable EnforceCodeStyleInBuild.' },
    @{ Path = 'BannedSymbols.txt'; Why = 'BannedSymbols.txt is required to ban Console/Debug logging APIs.' }
)

foreach ($item in $requiredFiles) {
    $full = Join-Path $repoRoot $item.Path
    if (-not (Test-Path -LiteralPath $full)) {
        Add-Violation -Rule 'Repository' -File $item.Path -Issue $item.Why
    }
}

$editorConfig = Join-Path $repoRoot '.editorconfig'
if (Test-Path -LiteralPath $editorConfig) {
    $editorText = [System.IO.File]::ReadAllText($editorConfig)
    if ($editorText -notmatch 'dotnet_naming_rule' -and $editorText -notmatch 'IDE1006') {
        Add-Violation -Rule 'Naming configuration' -File '.editorconfig' -Issue 'Expected .editorconfig to contain .NET naming rules (dotnet_naming_rule or IDE1006).'
    }
}

$directoryBuild = Join-Path $repoRoot 'Directory.Build.props'
if (Test-Path -LiteralPath $directoryBuild) {
    $propsText = [System.IO.File]::ReadAllText($directoryBuild)
    if ($propsText -notmatch 'EnforceCodeStyleInBuild') {
        Add-Violation -Rule 'Naming configuration' -File 'Directory.Build.props' -Issue 'Expected EnforceCodeStyleInBuild so naming rules fail the build.'
    }
    if ($propsText -notmatch 'BannedApiAnalyzers') {
        Add-Violation -Rule 'Logging configuration' -File 'Directory.Build.props' -Issue 'Expected Microsoft.CodeAnalysis.BannedApiAnalyzers for Console/Debug bans.'
    }
}

$bannedSymbols = Join-Path $repoRoot 'BannedSymbols.txt'
if (Test-Path -LiteralPath $bannedSymbols) {
    $bannedText = [System.IO.File]::ReadAllText($bannedSymbols)
    if ($bannedText -notmatch 'System\.Console') {
        Add-Violation -Rule 'Logging configuration' -File 'BannedSymbols.txt' -Issue 'Expected System.Console to be banned in favor of ILogger.'
    }
}

$architectureTests = Join-Path $repoRoot 'HostelOps-API Unit Tests\Architecture'
if (-not (Test-Path -LiteralPath $architectureTests)) {
    Add-Violation -Rule 'Architecture configuration' -File 'HostelOps-API Unit Tests/Architecture' -Issue 'Expected architecture test folder so architecture.md rules are enforced by dotnet test.'
}

$requiredTestFiles = @(
    @{ Path = 'HostelOps-API Unit Tests/Architecture/LayerDependencyTests.cs'; Why = 'Architecture layer tests are required.' },
    @{ Path = 'HostelOps-API Unit Tests/Architecture/NamingConventionTests.cs'; Why = 'Naming convention tests are required.' },
    @{ Path = 'HostelOps-API Unit Tests/Architecture/LoggingConventionTests.cs'; Why = 'Logging convention tests are required.' }
)
foreach ($item in $requiredTestFiles) {
    $full = Join-Path $repoRoot ($item.Path -replace '/', [IO.Path]::DirectorySeparatorChar)
    if (-not (Test-Path -LiteralPath $full)) {
        Add-Violation -Rule 'Repository' -File $item.Path -Issue $item.Why
    }
}

# ---------------------------------------------------------------------------
# Hardcoded secrets (architecture.md section 51, logging.md section 19)
# Not the same as logging placeholder names like {Password} in ILogger templates.
# ---------------------------------------------------------------------------
$secretAssignment = [regex]'(?i)\b(Password|Pwd|Secret|ApiKey|AccessToken|RefreshToken|ConnectionString)\b["''\s:=]+["'']([^"'']{8,})["'']'
$connectionStringPassword = [regex]'(?i)(Password|Pwd)\s*=\s*([^;''"\s]{4,})'
$jsonSecret = [regex]'(?i)"(Password|Pwd|Secret|ApiKey|AccessToken|RefreshToken|ConnectionString)"\s*:\s*"([^"]{4,})"'

$scanExtensions = @('*.cs', '*.json', '*.xml', '*.config', '*.yml', '*.yaml', '*.env', '*.http')
foreach ($filter in $scanExtensions) {
    Get-ChildItem -Path $repoRoot -Recurse -File -Filter $filter -ErrorAction SilentlyContinue |
        Where-Object { -not (Test-ExcludedPath $_.FullName) } |
        ForEach-Object {
            $relative = Get-RelativePath $_.FullName
            if ($relative -eq 'BannedSymbols.txt') { return }
            $lineNumber = 0
            foreach ($line in (Get-SourceLines $_.FullName)) {
                $lineNumber++
                if (Test-CommentOrStringNoise $line) { continue }

                foreach ($match in $jsonSecret.Matches($line)) {
                    $value = $match.Groups[2].Value
                    if (-not (Test-PlaceholderValue $value)) {
                        Add-Violation -Rule 'Secrets' -File $relative -Line $lineNumber -Issue "Hardcoded secret-like value for '$($match.Groups[1].Value)' is not allowed (architecture.md section 51 / logging.md section 19)."
                    }
                }

                if ($_.Extension -eq '.json') { continue }

                foreach ($match in $secretAssignment.Matches($line)) {
                    $value = $match.Groups[2].Value
                    if (-not (Test-PlaceholderValue $value)) {
                        Add-Violation -Rule 'Secrets' -File $relative -Line $lineNumber -Issue "Hardcoded '$($match.Groups[1].Value)' literal is not allowed (architecture.md section 51)."
                    }
                }

                foreach ($match in $connectionStringPassword.Matches($line)) {
                    $value = $match.Groups[2].Value
                    if (-not (Test-PlaceholderValue $value)) {
                        Add-Violation -Rule 'Secrets' -File $relative -Line $lineNumber -Issue 'Hardcoded connection-string password is not allowed (architecture.md section 51).'
                    }
                }
            }
        }
}

# ---------------------------------------------------------------------------
# Production default log level must not be Debug/Trace (logging.md §2, §23)
# ---------------------------------------------------------------------------
Get-ChildItem -Path $repoRoot -Recurse -File -Filter 'appsettings*.json' -ErrorAction SilentlyContinue |
    Where-Object { -not (Test-ExcludedPath $_.FullName) } |
    Where-Object { $_.Name -notmatch 'Development|Local' } |
    ForEach-Object {
        $settingsFile = $_
        $relative = Get-RelativePath $settingsFile.FullName
        try {
            $json = Get-Content -LiteralPath $settingsFile.FullName -Raw | ConvertFrom-Json
        }
        catch {
            Add-Violation -Rule 'Logging configuration' -File $relative -Issue "appsettings file is not valid JSON."
            return
        }

        $defaultLevel = $null
        if ($null -ne $json.Logging -and $null -ne $json.Logging.LogLevel) {
            $defaultLevel = [string] $json.Logging.LogLevel.Default
        }

        if ($defaultLevel -match '^(Debug|Trace)$') {
            Add-Violation -Rule 'Logging' -File $relative -Issue "Default log level '$defaultLevel' must not be used in shared/production settings (logging.md: avoid excessive Debug/Trace in production)."
        }
    }

# ---------------------------------------------------------------------------
# Blocking async calls in production C# (architecture.md §38)
# High-confidence patterns only; Task.Result is skipped (too many false positives).
# ---------------------------------------------------------------------------
$blockingAsync = [regex]'GetAwaiter\s*\(\s*\)\s*\.\s*GetResult\s*\(|\bTask\.(WaitAll|WaitAny|Wait)\s*\('

Get-ChildItem -Path $repoRoot -Recurse -File -Filter '*.cs' -ErrorAction SilentlyContinue |
    Where-Object { -not (Test-ExcludedPath $_.FullName) -and -not (Test-TestPath $_.FullName) } |
    ForEach-Object {
        $relative = Get-RelativePath $_.FullName
        $lineNumber = 0
        foreach ($line in (Get-SourceLines $_.FullName)) {
            $lineNumber++
            if (Test-CommentOrStringNoise $line) { continue }
            if ($blockingAsync.IsMatch($line)) {
                Add-Violation -Rule 'Architecture' -File $relative -Line $lineNumber -Issue 'Avoid blocking waits on Task (architecture.md section 38). Use await instead of GetAwaiter().GetResult() / Task.Wait*.'
            }
        }
    }

# ---------------------------------------------------------------------------
# .gitignore should keep local secrets out of source control (architecture.md §51)
# ---------------------------------------------------------------------------
$gitignore = Join-Path $repoRoot '.gitignore'
if (-not (Test-Path -LiteralPath $gitignore)) {
    Add-Violation -Rule 'Repository' -File '.gitignore' -Issue '.gitignore is required so secrets and local configuration are not committed.'
}
else {
    $ignoreText = [System.IO.File]::ReadAllText($gitignore)
    $requiredIgnore = @(
        @{ Pattern = 'secrets\.json'; Why = 'User-secret files should be ignored.' },
        @{ Pattern = 'appsettings\.(Development|Local)\.json'; Why = 'Local/development settings often contain secrets and should be ignored.' }
    )
    foreach ($item in $requiredIgnore) {
        if ($ignoreText -notmatch $item.Pattern) {
            Add-Violation -Rule 'Repository' -File '.gitignore' -Issue $item.Why
        }
    }
}

# ---------------------------------------------------------------------------
# Naming conventions.md — source patterns analyzers may miss (file names, m_, snake constants)
# ---------------------------------------------------------------------------
$hungarianField = [regex]'\bm_[A-Za-z]'
$upperSnakeConst = [regex]'\bconst\s+[^=;\r\n]+?\s+[A-Z][A-Z0-9]*_[A-Z0-9_]+\s*='
$pascalFile = [regex]'^[A-Z][A-Za-z0-9]*$'

Get-ChildItem -Path $repoRoot -Recurse -File -Filter '*.cs' -ErrorAction SilentlyContinue |
    Where-Object { -not (Test-ExcludedPath $_.FullName) -and -not (Test-TestPath $_.FullName) } |
    ForEach-Object {
        $relative = Get-RelativePath $_.FullName
        $fileStem = [System.IO.Path]::GetFileNameWithoutExtension($_.Name)
        if (-not $pascalFile.IsMatch($fileStem)) {
            Add-Violation -Rule 'Naming' -File $relative -Issue 'C# file names must be PascalCase (naming-conventions.md).'
        }

        $lineNumber = 0
        foreach ($line in (Get-SourceLines $_.FullName)) {
            $lineNumber++
            if (Test-CommentOrStringNoise $line) { continue }
            if ($hungarianField.IsMatch($line)) {
                Add-Violation -Rule 'Naming' -File $relative -Line $lineNumber -Issue 'Use _camelCase for fields, not m_ prefixes (naming-conventions.md).'
            }
            if ($upperSnakeConst.IsMatch($line)) {
                Add-Violation -Rule 'Naming' -File $relative -Line $lineNumber -Issue 'Constants must use PascalCase, not UPPER_SNAKE_CASE (naming-conventions.md).'
            }
        }
    }

# ---------------------------------------------------------------------------
# Architecture.md — UTC timestamps (section 46)
# ---------------------------------------------------------------------------
$localNow = [regex]'\bDateTime\.(Now|Today)\b'

Get-ChildItem -Path $repoRoot -Recurse -File -Filter '*.cs' -ErrorAction SilentlyContinue |
    Where-Object { -not (Test-ExcludedPath $_.FullName) -and -not (Test-TestPath $_.FullName) } |
    ForEach-Object {
        $relative = Get-RelativePath $_.FullName
        $lineNumber = 0
        foreach ($line in (Get-SourceLines $_.FullName)) {
            $lineNumber++
            if (Test-CommentOrStringNoise $line) { continue }
            if ($localNow.IsMatch($line)) {
                Add-Violation -Rule 'Architecture' -File $relative -Line $lineNumber -Issue 'Do not use DateTime.Now/Today for internal timestamps; use UTC (architecture.md section 46).'
            }
        }
    }

# ---------------------------------------------------------------------------
# Logging.md — Debug/Trace.WriteLine in source (BannedApiAnalyzers cover compiled Console usage)
# ---------------------------------------------------------------------------
$debugTraceWrite = [regex]'\b(Debug|Trace)\.Write(Line)?\s*\('

Get-ChildItem -Path $repoRoot -Recurse -File -Filter '*.cs' -ErrorAction SilentlyContinue |
    Where-Object { -not (Test-ExcludedPath $_.FullName) -and -not (Test-TestPath $_.FullName) } |
    ForEach-Object {
        $relative = Get-RelativePath $_.FullName
        $lineNumber = 0
        foreach ($line in (Get-SourceLines $_.FullName)) {
            $lineNumber++
            if (Test-CommentOrStringNoise $line) { continue }
            if ($debugTraceWrite.IsMatch($line)) {
                Add-Violation -Rule 'Logging' -File $relative -Line $lineNumber -Issue 'Use ILogger instead of Debug/Trace.WriteLine (logging.md).'
            }
        }
    }

# ---------------------------------------------------------------------------
# Report
# ---------------------------------------------------------------------------
Write-Host 'Instruction Validation'
Write-Host '----------------------'
Write-Host 'Architecture: .NET tests + PowerShell (blocking async, DateTime.Now, secrets, gitignore)'
Write-Host 'Naming: .NET analyzers/tests + PowerShell (file PascalCase, m_ prefix, UPPER_SNAKE constants)'
Write-Host 'Logging: BannedApiAnalyzers/.NET tests + PowerShell (log levels, Debug/Trace.WriteLine, secrets)'
Write-Host ''

if ($violations.Count -eq 0) {
    Write-Host '✓ No hardcoded secrets found in source or configuration'
    Write-Host '✓ Required enforcement configuration found'
    Write-Host '✓ Production/shared log levels are not Debug/Trace by default'
    Write-Host '✓ No Debug/Trace.WriteLine in production source'
    Write-Host '✓ No blocking async waits found in production source'
    Write-Host '✓ No DateTime.Now/Today in production source'
    Write-Host '✓ Naming source patterns passed'
    Write-Host '✓ Repository checks passed'
    Write-Host ''
    Write-Host 'Instruction validation PASSED'
    exit 0
}

foreach ($item in $violations) {
    Write-Host "✗ $($item.Rule) rule violation"
    if ($item.File) {
        Write-Host "  File: $($item.File)"
    }
    if ($item.Line) {
        Write-Host "  Line: $($item.Line)"
    }
    Write-Host "  Issue: $($item.Issue)"
    Write-Host ''
}

Write-Host 'Instruction validation FAILED'
exit 1
