# Test-Coverage.ps1
# -----------------
# PowerShell script to clean old reports and test results,
# run .NET tests with code coverage, generate a new report,
# and open the report in a web browser.

# [1] Remove previous coverage report folder
if (Test-Path "coveragereport") {
    Write-Host "Removing old coverage report folder 'coveragereport'..." -ForegroundColor Yellow
    Remove-Item -Recurse -Force "coveragereport"
}

# [2] Recursively find and remove all 'TestResults' folders in subdirectories
Write-Host "Searching and removing all 'TestResults' folders..." -ForegroundColor Yellow
Get-ChildItem -Recurse -Directory -Filter "TestResults" | ForEach-Object {
    Write-Host "Removing: $($_.FullName)" -ForegroundColor DarkYellow
    Remove-Item -Recurse -Force $_.FullName
}

# [3] Run tests with code coverage collection
Write-Host "Running tests with code coverage..." -ForegroundColor Cyan
dotnet test --collect:"XPlat Code Coverage"

# [4] Generate HTML coverage report
Write-Host "Generating HTML coverage report..." -ForegroundColor Cyan
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html

# [5] Open the generated HTML report (index.html)
$reportPath = Join-Path (Get-Location) "coveragereport/index.html"

if (Test-Path $reportPath) {
    Write-Host "Opening report: $reportPath" -ForegroundColor Green

    if ($IsLinux) {
        Start-Process "xdg-open" $reportPath
    }
    elseif ($IsWindows) {
        Start-Process $reportPath
    }
    elseif ($IsMacOS) {
        Start-Process "open" $reportPath
    }
}
else {
    Write-Host "❌ Report file not found: $reportPath" -ForegroundColor Red
}
