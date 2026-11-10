# 01-pin-pkcs-transitive-dependency: Pin System.Security.Cryptography.Pkcs in Core and Data

## Objective
Add an explicit stable `System.Security.Cryptography.Pkcs` 10.0.14 reference to the library projects that currently resolve vulnerable 9.0.4 transitively.

## Research Findings
- `CS2Tracker_Api/CS2Tracker_Api.csproj` already directly references `System.Security.Cryptography.Pkcs` 10.0.14.
- `CS2Tracker_Core/CS2Tracker_Core.csproj` targets `net10.0`, has standard per-project `PackageReference` management, and resolves `System.Security.Cryptography.Pkcs` 9.0.4 transitively during restore.
- `CS2Tracker_Data/CS2Tracker_Data.csproj` targets `net10.0`, has standard per-project `PackageReference` management, and resolves the same vulnerable package transitively.
- `get_project_dependencies` found no Central Package Management file or package declaration for this package.
- `dotnet list package --include-transitive` confirmed the vulnerable 9.0.4 resolution and NU1902/NU1903 warnings in Core/Data.
- Assessment selected unified stable version 10.0.14 and reported no API breaking-change findings; validation will be build-driven.

## Scope
Modify only `CS2Tracker_Core/CS2Tracker_Core.csproj` and `CS2Tracker_Data/CS2Tracker_Data.csproj`. Restore and build both affected projects, then build `CS2Tracker.slnx` and run discovered tests if any.

## Steps
1. Add `System.Security.Cryptography.Pkcs` version `10.0.14` to Core and Data using standard per-project `PackageReference` entries.
2. Restore and build the affected projects.
3. Confirm package resolution is 10.0.14 and NU1902/NU1903 are absent.
4. Build the full solution and run tests if test projects are present.

**Done when**: Core, Data, and the full solution restore/build without NU1902 or NU1903 warnings for `System.Security.Cryptography.Pkcs`; the resolved package version is 10.0.14 in all affected projects; tests, if present, pass.
