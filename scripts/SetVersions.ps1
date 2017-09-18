$csprojPath = $args[0]
$packageVersion = [convert]::ToString($args[1])
$assemblyVersion = [convert]::ToString($args[2])
$assemblyFileVersion = [convert]::ToString($args[3])

$xml = [xml](Get-Content $csprojPath)
$xml.Project.PropertyGroup[0].Version = $packageVersion
$xml.Project.PropertyGroup[0].AssemblyVersion= $assemblyVersion 
$xml.Project.PropertyGroup[0].FileVersion= $assemblyFileVersion 

$xml.Save($csprojPath)
