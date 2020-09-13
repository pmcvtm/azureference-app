include "./build-helpers.ps1"

properties {
    $configuration = 'Release'
    $version = '1.0.0'
    $owner = 'Loud and Abrasive and Co.'
    $product = 'Azurereference'
    $yearInitiated = '2020'
    $projectRootDirectory = "$(resolve-path .)"
    $publish = "$projectRootDirectory/Publish"
    $stage = "$projectRootDirectory/Stage"
}

task default -depends Compile
task ci -depends Clean, Compile, Publish -description "Continuous Integration process"

task Compile -description "Compile the solution" {
    exec { set-project-properties $version } -workingDirectory src
    exec { dotnet build --configuration $configuration /nologo } -workingDirectory src
}

task Publish -depends Compile -description "Publish the primary projects for distribution" {
    delete-directory $publish
    delete-directory $stage
    exec { publish-project } -workingDirectory 'src/Azureference.Web'
}
  
task Clean -description "Clean out all the binary folders" {
    exec { dotnet clean --configuration $configuration /nologo } -workingDirectory src
    delete-directory $publish
    delete-directory $stage
}

task ? -alias help -description "Display help content and possible targets" {
	WriteDocumentation
}
