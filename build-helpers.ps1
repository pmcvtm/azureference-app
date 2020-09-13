function set-project-properties($targetVersion) {
    $date = Get-Date
    $year = $date.Year
    $copyrightSpan = if ($year -eq $yearInitiated) { $year } else { "$yearInitiated-$year" }
    $copyright = "© $copyrightSpan $owner"

    write-host "$product $targetVersion"
    write-host $copyright

    regenerate-file "$pwd/Directory.Build.props" @"
<Project>
    <PropertyGroup>
        <Product>$product</Product>
        <Version>$targetVersion</Version>
        <Copyright>$copyright</Copyright>
        <LangVersion>latest</LangVersion>
    </PropertyGroup>
</Project>
"@
}

function publish-project {
    $project = Split-Path $pwd -Leaf
    Write-Host "Publishing $project"
    dotnet publish --configuration $configuration --self-contained -r "win-x64" --output $stage/$project /nologo
    exec { Compress-Archive $stage/$project/* -DestinationPath $publish/"$project.zip"}
}

function regenerate-file($path, $newContent) {
    $oldContent = [IO.File]::ReadAllText($path)

    if ($newContent -ne $oldContent) {
        write-host "Generating $path"
        [System.IO.File]::WriteAllText($path, $newContent, [System.Text.Encoding]::UTF8)
    }
}

function delete-directory($path) {
    if (test-path $path) {
        write-host "Deleting $path"
        rd $path -recurse -force -ErrorAction SilentlyContinue | out-null
    }
    New-Item $path -ItemType "directory" | out-null
 }
