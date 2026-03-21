## PowerShell Blazor WebAssembly demo

This directory contains an experimental browser-hosted PowerShell demo that was ported forward from the 2020 WASM prototype branch.

## Requirements

- .NET SDK version from `global.json`
- A successful local build of the PowerShell repo dependencies

## Run

```bash
cd demos/Blazor/powershell-blazor-wasm-demo
dotnet run
```

Then open the local URL shown by `dotnet run`.

## Notes

- This is still an experiment, not a production support statement for browser-hosted PowerShell.
- The demo intentionally creates a small PowerShell module root under `/opt/microsoft/powershell/Modules` so browser runs do not depend on the normal desktop install layout.
