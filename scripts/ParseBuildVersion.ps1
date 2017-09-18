function ParseBuildVersion($buildVersion) 
{
	$splitVersion = $buildVersion.Split(".")

	$majorNumber = $splitVersion[0]
	$minorNumber = $splitVersion[1]
	$patchNumber = $splitVersion[2]

	$matches = [regex]::match($patchNumber, '^(\d+)-alpha(\d+)$')
	if ($matches.Groups[1].Value -ne "")
	{
		$isAlpha = $true
		$patchNumber = $matches.Groups[1].Value
		$buildId = $matches.Groups[2].Value
	}
	else 
	{
		$isAlpha = $false
		$buildId = $splitVersion[3]
	}

	if ($isAlpha) 
	{
		$packageVersion = $majorNumber + "." + $minorNumber + "." + $patchNumber + "-alpha" + [convert]::ToString([convert]::ToInt32($buildId)).PadLeft(5, '0')
	}
	else 
	{
		$packageVersion = $majorNumber + "." + $minorNumber + "." + $patchNumber
	}

	$assemblyVersion = $majorNumber + "." + $minorNumber
	$assemblyFileVersion = $majorNumber + "." + $minorNumber + "." + $patchNumber + "." + $buildId

	Write-Output $packageVersion
	Write-Output $assemblyVersion
	Write-Output $assemblyFileVersion
}