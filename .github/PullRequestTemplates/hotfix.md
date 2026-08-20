## What
<!-- Describe the production incident and its user impact -->

## Why
<!-- Business impact + ticket reference -->

## Root Cause
<!-- What caused this incident? -->

## Fix
<!-- List changes by file path -->
- `path/to/file.cs` — description

## Testing
- Unit tests: [ ] added / [ ] updated
- Regression tests: [ ] added / [ ] updated

## Risk Mitigation
- Rollback plan: <!-- describe -->
- Feature flag: [ ] N/A / [ ] enabled
- Monitoring: <!-- alert / dashboard link -->

## Post-Release Validation
<!-- Steps to confirm fix in production -->
1.
2.

## Breaking Changes
<!-- None / describe impact -->

## Closes
Closes #<ticket>

---
**Enterprise Principles**
- [ ] Works — incident is fully resolved
- [ ] Secure — no new vulnerabilities
- [ ] Scales — tenant-isolated, async-safe
- [ ] Maintainable — documented, no dead code
- [ ] User Impact — rollback plan in place

**Quality Checklist**
- [ ] All tests pass
- [ ] Coverage >= 90%
- [ ] Zero build warnings
- [ ] Zero high/critical security issues
- [ ] Branch: `hotfix/<id>-<short-description>`
- [ ] PR title: `[Hotfix-<id>] <title>`
- [ ] Conventional commit messages
- [ ] Azure DevOps work item linked
- [ ] Tenant isolation validated
- [ ] Input validation and security checks added
