$assemblyInfoPath = $args[0]
$packageVersion =[convert]::ToString($args[1])
$assemblyVersion =[convert]::ToString($args[2])
$assemblyFileVersion = [convert]::ToString($args[3])

$assemblyInformationalVersionPattern = '^\[assembly: AssemblyInformationalVersion\("(.*)"\)\]$'
$assemblyVersionPattern = '^\[assembly: AssemblyVersion\("(.*)"\)\]$'
$assemblyFileVersionPattern = '^\[assembly: AssemblyFileVersion\("(.*)"\)\]$'

(Get-Content $assemblyInfoPath) | ForEach-Object{
	if ($_ -match $assemblyInformationalVersionPattern)
	{        
        '[assembly: AssemblyInformationalVersion("{0}")]' -f $packageVersion
    } 
    elseif ($_ -match $assemblyVersionPattern)
	{        
        '[assembly: AssemblyVersion("{0}")]' -f $assemblyVersion
    } 
    elseif ($_ -match $assemblyFileVersionPattern)
	{        
        '[assembly: AssemblyFileVersion("{0}")]' -f $assemblyFileVersion
    } 
	else 
	{
        $_
    }
} | Set-Content $assemblyInfoPath