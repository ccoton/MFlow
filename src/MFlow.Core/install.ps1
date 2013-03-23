param($installPath, $toolsPath, $package, $project)

$project.ProjectItems.Item("Resources").ProjectItems.Item("Xml").ProjectItems.Item("Messages.en.xml").Properties.Item("CopyToOutputDirectory").Value = 1
$project.ProjectItems.Item("Resources").ProjectItems.Item("Xml").ProjectItems.Item("Messages.fr-FR.xml").Properties.Item("CopyToOutputDirectory").Value = 1

