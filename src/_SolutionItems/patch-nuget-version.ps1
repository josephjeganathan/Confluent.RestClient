function xmlPoke($file, $xpath, $value){
    $filePath = $file.FullName

    [xml] $fileXml = Get-Content $filePath
    $node = $fileXml.SelectSingleNode($xpath)
    if ($node) { 
        $node.InnerText = $value
        $fileXml.Save($filePath)
    }
}

if ($env:APPVEYOR_REPO_TAG -eq $true){
	xmlPoke (Get-ChildItem ".\src\_SolutionItems\Confluent.RestClient.nuspec") "/package/metadata/version" $env:APPVEYOR_REPO_TAG_NAME
}
else{
	xmlPoke (Get-ChildItem ".\src\_SolutionItems\Confluent.RestClient.nuspec") "/package/metadata/version" $env:APPVEYOR_BUILD_VERSION
}