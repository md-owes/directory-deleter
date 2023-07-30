param(
    [string]$newVersion
)

$manifestFilePath = Join-Path $PWD "directory-deleter\Platforms\Windows\app.manifest"
$appxmanifestFilePath = Join-Path $PWD "directory-deleter\Platforms\Windows\package.appxmanifest"

[xml]$xmlContent = Get-Content $manifestFilePath
$xmlContent.assembly.assemblyIdentity.version = $newVersion
$xmlContent.Save($manifestFilePath)

[xml]$xmlContent = Get-Content $appxmanifestFilePath
$xmlContent.Package.Identity.Version = $newVersion
$xmlContent.Save($appxmanifestFilePath)