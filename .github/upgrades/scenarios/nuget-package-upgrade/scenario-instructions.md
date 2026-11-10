# NuGet Package Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Scope**: `CS2Tracker.slnx`
- **Package**: `System.Security.Cryptography.Pkcs`
- **Version Policy**: Latest supported stable version
- **Full Scan**: Quick assessment; enable full scan only if exact breaking-change locations are needed
- **Vulnerability**: Resolve `GHSA-2fmv-rv2c-59x7`

## Source Control
- **Source Branch**: `main`
- **Working Branch**: `nuget-package-upgrade`
- **Pending Changes**: Committed before branching
- **Commit Strategy**: After Each Task
- **Branch Sync**: Disabled

## Decisions
- Use `System.Security.Cryptography.Pkcs` 10.0.14 as the unified stable version. The API project already pins it directly; Core and Data require explicit pins because they resolve 9.0.4 transitively.
- No Central Package Management file was found; apply the package version directly in the affected project files.
