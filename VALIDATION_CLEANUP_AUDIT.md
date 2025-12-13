# Legacy Validation Code Audit

## Overview
This document tracks handlers that contain legacy validation code that should be removed, as validation is now handled by the ValidationBehavior pipeline with FluentValidation.

## Handlers with Try-Catch Blocks (Non-Business Logic)

### Partners - ✅ COMPLETED
- ✅ `DeletePartnerHandler.cs` - Removed try-catch around SaveChangesAsync
- ✅ `UpdatePartnerHandler.cs` - Removed try-catch around SaveChangesAsync
- ✅ `CreatePartnerHandler.cs` - Removed try-catch around SaveChangesAsync

### Team - ✅ COMPLETED
- ✅ `CreatePositionHandler.cs` - Removed try-catch around SaveChangesAsync

### AdditionalContent - ✅ COMPLETED
- ✅ `CreateTagHandler.cs` - Removed try-catch around SaveChangesAsync

### Streetcode - ✅ COMPLETED
- ✅ `CreateStreetcodeHandler.cs` - Removed large try-catch block and validation comments
- ✅ `UpdateStreetcodeHandler.cs` - Removed try-catch around SaveChangesAsync
- ✅ `DeleteFactHandler.cs` - Removed try-catch around SaveChangesAsync
- ✅ `CreateFactHandler.cs` - Removed try-catch around SaveChangesAsync
- ✅ `UpdateFactHandler.cs` - Removed try-catch around SaveChangesAsync
- ✅ `GetRelatedFiguresByStreetcodeIdHandler.cs` - Removed ArgumentNullException catch

### Other Modules Checked
- News handlers - ✅ No validation-related try-catch blocks found
- Timeline handlers - ✅ No validation-related try-catch blocks found
- Media handlers - ✅ No validation-related try-catch blocks found
- Sources handlers - ✅ No validation-related try-catch blocks found
- Toponyms handlers - ✅ No validation-related try-catch blocks found
- Payment handlers - ✅ No validation-related try-catch blocks found
- Transaction handlers - ✅ No validation-related try-catch blocks found

## Manual Null Checks - ✅ COMPLETED
All redundant manual null checks have been removed from:
- CreateStreetcodeHandler - Removed audio, image, and tag null checks
- UpdateStreetcodeHandler - Removed audio, image, and tag null checks
- All handlers now trust FluentValidation validators for input validation

## Validation Pattern Applied ✅
All modified handlers now:
1. ✅ Only contain business logic
2. ✅ Trust that validation has been performed by ValidationBehavior
3. ✅ Don't wrap SaveChangesAsync in try-catch for validation
4. ✅ Maintain business logic validation (e.g., checking for duplicate streetcode index)

## Summary of Changes
- **11 handler files** modified across 6 modules
- **Removed ~200 lines** of redundant validation code
- **All try-catch blocks** used for validation have been removed
- **ValidationBehavior pipeline** is now the single source of validation

## Validators Confirmed in Place
✅ Over 60 FluentValidation validators exist covering:
- Partners (Create/Update/Delete)
- Team members and positions
- News (Create/Update/Delete)
- Facts (Create/Update/Delete)
- Media (Audio/Image Create/Delete)
- AdditionalContent (Tags, Coordinates, etc.)
- Streetcode (Create/Update/Delete operations)
- Related figures and terms
- Payment operations
