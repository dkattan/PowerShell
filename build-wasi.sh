#!/bin/sh

set -e
#pwsh -c "Import-Module ./build.psm1 -Force; Start-PSBuild -Clean -Runtime fxdependent"
export WASI_VERSION=20
export WASI_VERSION_FULL=${WASI_VERSION}.0
compiler_dir="$(readlink -f ../compiler)"
export WASI_SDK_PATH="$(readlink -f /root/.wasi-sdk/wasi-sdk-${WASI_VERSION_FULL})"

echo "Building WASI.."
echo "WASI SDK path: $WASI_SDK_PATH"
dotnet publish "/mnt/c/Users/DKattan/source/repos/PowerShellWasm/PowerShellWasm.sln" -c Release \
    /p:RuntimeIdentifier=wasi-wasm \
    /p:PublishSingleFile=false \
    /p:WasmSingleFileBundle=true \
    /p:WASI_SDK_PATH="$WASI_SDK_PATH" \
    /p:InvariantGlobalization=true \
    /p:PublishTrimmed=false \
    /p:DebuggerSupport=false \
    /p:EventSourceSupport=false \
    /p:StackTraceSupport=false \
    /p:UseSystemResourceKeys=true \
    /p:NativeDebugSymbols=false \
    /p:PublishReadyToRun=false \
    /p:WasmInitialHeapSize=4294967296 \
