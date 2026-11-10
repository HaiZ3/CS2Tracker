# Progress Details

## Changes
- Added `System.Security.Cryptography.Pkcs` version `10.0.14` to `CS2Tracker_Core/CS2Tracker_Core.csproj`.
- Added `System.Security.Cryptography.Pkcs` version `10.0.14` to `CS2Tracker_Data/CS2Tracker_Data.csproj`.
- The API project already had the package pinned to `10.0.14`.

## Validation
- Core targeted build: passed.
- Data targeted build: passed.
- Full `CS2Tracker.slnx` restore/build: passed with 0 warnings and 0 errors.
- `dotnet list package --include-transitive` confirms `System.Security.Cryptography.Pkcs` resolves to `10.0.14` in Core and Data.
- No test projects were discovered.
- No NU1902 or NU1903 warnings remain for the vulnerable package.

## Issues Resolved
- Removed the vulnerable `System.Security.Cryptography.Pkcs` 9.0.4 resolution that caused the restore warning and stopped the command under `ErrorActionPreference=Stop`.
