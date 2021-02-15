$csprojPath = $args[0]
$packageVersion = [convert]::ToString($args[1])
$assemblyVersion = [convert]::ToString($args[2])
$assemblyFileVersion = [convert]::ToString($args[3])

$xml = [xml](Get-Content $csprojPath)
$xml.Project.PropertyGroup.Version = $packageVersion
$xml.Project.PropertyGroup.AssemblyVersion= $assemblyVersion 
$xml.Project.PropertyGroup.FileVersion= $assemblyFileVersion 

$xml.Save($csprojPath)
