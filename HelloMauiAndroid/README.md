# Maui Android sample

## How to build

- For release builds use:
```
<dotnet8-exe> publish -c Release -t:Run -r android-arm64 -bl
```
Where `<dotnet8-exe>` is the path to the dotnet8 executable

- For builds with locally build dotnet/runtime: 
```
<dotnet8-exe> publish -c Release -t:Run -r android-arm64 -bl -p:UseCurrentMainBranch=true -p:DotnetRuntimeRepo=/Users/ivan/repos/runtime-mono
```
Where `-p:DotnetRuntimeRepo=` should receive the path to the root of dotnet/runtime repository