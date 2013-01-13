function Update-SourceVersion ( $updatedVersion )
{ 
	echo $updatedVersion 
	$NewVersion = 'AssemblyVersion("' + $updatedVersion + '")';
	$NewFileVersion = 'AssemblyFileVersion("' + $updatedVersion + '")';

	echo $NewVersion
	foreach ($o in $input) 
	{
	Write-output $o.FullName
	$TmpFile = $o.FullName + ".tmp"

	 get-content $o.FullName | 
		%{$_ -replace 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $NewVersion } |
		%{$_ -replace 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $NewFileVersion }  > $TmpFile

	 move-item $TmpFile $o.FullName -force
	}
}


function Update-AllAssemblyInfoFiles ( $updatedVersion )
{
	foreach ($file in "AssemblyInfo.cs", "AssemblyInfo.vb" ) 
	{
		echo $updatedVersion
		get-childitem -recurse |? {$_.Name -eq $file} | Update-SourceVersion $updatedVersion ;
	}
}

$scriptpath = split-path -parent $MyInvocation.MyCommand.Path
$buildnumberfile = resolve-path "$scriptpath/../buildnumber.xml"
#read in the xml
$xml = [xml](Get-Content $buildnumberfile)

#get the name of the app pool
$major = $xml.BuildNumber.Major
$minor = $xml.BuildNumber.Minor
$version = $xml.BuildNumber.Build
$revision = [string]([int]$xml.BuildNumber.Revision + 1)
$updatedVersion = $major + "." + $minor + "." + $version + "." + $revision
Update-AllAssemblyInfoFiles $updatedVersion;
$xml.BuildNumber.Major = $major
$xml.BuildNumber.Minor = $minor
$xml.BuildNumber.Build = $version
$xml.BuildNumber.Revision = $revision
$xml.Save($buildnumberfile)