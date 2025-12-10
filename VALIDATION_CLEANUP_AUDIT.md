# Legacy Validation Code Audit

## Overview
This document tracks handlers that contain legacy validation code that should be removed, as validation is now handled by the ValidationBehavior pipeline with FluentValidation.

## Handlers with Try-Catch Blocks (Non-Business Logic)

### Partners
- ✅ `DeletePartnerHandler.cs` - Try-catch around SaveChangesAsync
- ✅ `UpdatePartnerHandler.cs` - Try-catch around SaveChangesAsync
- ✅ `CreatePartnerHandler.cs` - Try-catch around SaveChangesAsync

### Team
- ✅ `CreatePositionHandler.cs` - Try-catch around SaveChangesAsync

### AdditionalContent
- ✅ `CreateTagHandler.cs` - Try-catch around SaveChangesAsync

### Streetcode
- ✅ `CreateStreetcodeHandler.cs` - Large try-catch block with validation comments
- ✅ `UpdateStreetcodeHandler.cs` - Try-catch around SaveChangesAsync
- ✅ `DeleteFactHandler.cs` - Try-catch around SaveChangesAsync
- ✅ `CreateFactHandler.cs` - Try-catch around SaveChangesAsync
- ✅ `UpdateFactHandler.cs` - Try-catch around SaveChangesAsync
- ✅ `GetRelatedFiguresByStreetcodeIdHandler.cs` - ArgumentNullException catch

## Manual Null Checks to Remove
All manual null checks that are covered by FluentValidation validators should be reviewed and removed where appropriate.

## Validation Pattern to Apply
Handlers should:
1. Only contain business logic
2. Trust that validation has been performed by ValidationBehavior
3. Only catch truly exceptional system errors (DB connection issues, etc.)
4. Not catch validation-related exceptions

## Notes
- ValidationBehavior.cs already implements validation pipeline
- All validation logic should be in separate validator classes
- Keep error logging for genuine system exceptions
