# NuGet Package Upgrade Plan

## Overview

**Target**: Remove vulnerable `System.Security.Cryptography.Pkcs` 9.0.4 assets from the solution.
**Scope**: Two SDK-style library projects currently resolving the vulnerable package transitively.

## Tasks

### 01-pin-pkcs-transitive-dependency: Pin System.Security.Cryptography.Pkcs in Core and Data

Add an explicit stable `System.Security.Cryptography.Pkcs` 10.0.14 reference to `CS2Tracker_Core` and `CS2Tracker_Data`, which currently resolve 9.0.4 transitively through their .NET 10 dependency graph. The API project already references 10.0.14 directly. Restore the solution and verify that the vulnerable 9.0.4 package is no longer resolved and that no API changes require source fixes.

**Done when**: Core, Data, and the full solution restore/build without NU1902 or NU1903 warnings for `System.Security.Cryptography.Pkcs`; the resolved package version is 10.0.14 in all affected projects; tests, if present, pass.
