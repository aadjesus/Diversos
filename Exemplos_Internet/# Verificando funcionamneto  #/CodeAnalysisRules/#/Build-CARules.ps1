$PSScriptFilePath = (Get-Item $MyInvocation.MyCommand.Path).FullName

$SolutionFolderPath = Split-Path -Path $PSScriptFilePath -Parent

$Is64BitSystem = (Get-WmiObject -Class Win32_OperatingSystem).OsArchitecture -eq "64-bit"
$Is64BitProcess = [IntPtr]::Size -eq 8

$RegistryArchitecturePath = ""
if ($Is64BitProcess) { $RegistryArchitecturePath = "\Wow6432Node" }
$ClrVersion = (Get-ItemProperty -path "HKLM:\SOFTWARE$RegistryArchitecturePath\Microsoft\VisualStudio\10.0")."CLR Version"
$VSInstallDir = (Get-ItemProperty -path "HKLM:\SOFTWARE$RegistryArchitecturePath\Microsoft\VisualStudio\10.0").InstallDir

$FrameworkArchitecturePath = ""
if ($Is64BitSystem) { $FrameworkArchitecturePath = "64" }
$MSBuild = "$Env:SYSTEMROOT\Microsoft.NET\Framework$FrameworkArchitecturePath\$ClrVersion\MSBuild.exe"

# Build the solution in release mode
$ProjectFolderPath = Join-Path -Path $SolutionFolderPath -ChildPath "BlogDemo.CodeAnalysisRules"
$ProjectFilePath = Join-Path -Path $ProjectFolderPath -ChildPath "BlogDemo.CodeAnalysisRules.csproj"
& $MSBuild "$ProjectFilePath" /p:Configuration=Release /maxcpucount /t:Clean
if (-not $?)
{
	throw "The MSBuild process returned an error code."
}
& $MSBuild "$ProjectFilePath" /p:Configuration=Release /maxcpucount
if (-not $?)
{
	throw "The MSBuild process returned an error code."
}

# Create the output folder
$OutputBinFolder = Join-Path -Path $SolutionFolderPath -ChildPath "Release"
if ((Get-Item $OutputBinFolder -ErrorAction SilentlyContinue) -ne $null)
{
	Remove-Item $OutputBinFolder -Recurse
}
New-Item $OutputBinFolder -Type directory

# Copy VS2010 CA rules to output folder (dll + pdb)
$ProjectBinFolder = Join-Path -Path $ProjectFolderPath -ChildPath "bin\Release"
Copy-Item "$ProjectBinFolder\BlogDemo.CodeAnalysisRules.dll" `
	-Destination (Join-Path -Path $OutputBinFolder -ChildPath "BlogDemo.CodeAnalysisRules.VS2010.dll")
Copy-Item "$ProjectBinFolder\BlogDemo.CodeAnalysisRules.pdb" `
	-Destination (Join-Path -Path $OutputBinFolder -ChildPath "BlogDemo.CodeAnalysisRules.VS2010.pdb")

# Build the CA rules again for FxCop 1.36 (different SDK + runtime)
$FxCop136DependencyPath = Join-Path -Path $SolutionFolderPath -ChildPath "FxCop136\"
& $MSBuild "$ProjectFilePath" /p:Configuration=Release /maxcpucount /t:Clean
if (-not $?)
{
	throw "The MSBuild process returned an error code."
}
& $MSBuild "$ProjectFilePath" /p:Configuration=Release /maxcpucount /p:CodeAnalysisPath="$FxCop136DependencyPath" /p:DefineConstants="" /p:TargetFrameworkVersion="v3.5"
if (-not $?)
{
	throw "The MSBuild process returned an error code."
}

# Copy FxCop 1.36 CA rules to output folder (dll + pdb)
Copy-Item "$ProjectBinFolder\BlogDemo.CodeAnalysisRules.dll" `
	-Destination (Join-Path -Path $OutputBinFolder -ChildPath "BlogDemo.CodeAnalysisRules.FxCop136.dll")
Copy-Item "$ProjectBinFolder\BlogDemo.CodeAnalysisRules.pdb" `
	-Destination (Join-Path -Path $OutputBinFolder -ChildPath "BlogDemo.CodeAnalysisRules.FxCop136.pdb")