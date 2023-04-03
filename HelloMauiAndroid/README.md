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

## Inspecting method invoker dumps via `IP_Diagnostics`

1. Build the dotnet/runtime repository from: https://github.com/ivanpovazan/runtime/tree/wip-method-invoke-stats with: `./build.sh mono+libs -os android -arch arm64`
2. Build the sample with locally built runtime
3. Run the sample
4. Grep logcat to get stats on dynamically invoked methods with: `adb logcat | grep "<<<---"`
