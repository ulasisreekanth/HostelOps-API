## What
<!-- Describe the bug and its user impact -->

## Why
<!-- Business impact + ticket reference -->

## Root Cause
<!-- What caused this bug? -->

## Fix
<!-- List changes by file path -->
- `path/to/file.cs` — description

## Testing
- Unit tests: [ ] added / [ ] updated
- Regression tests: [ ] added / [ ] updated

## Breaking Changes
<!-- None / describe impact -->

## Closes
Closes #<ticket>

---
**Enterprise Principles**
- [ ] Works — bug is fully resolved
- [ ] Secure — no new vulnerabilities
- [ ] Scales — tenant-isolated, async-safe
- [ ] Maintainable — documented, no dead code
- [ ] User Impact — regression risk assessed

**Quality Checklist**
- [ ] All tests pass
- [ ] Coverage >= 90%
- [ ] Zero build warnings
- [ ] Zero high/critical security issues
- [ ] Branch: `bug/<id>-<short-description>`
- [ ] PR title: `[Bug-<id>] <title>`
- [ ] Conventional commit messages
- [ ] Azure DevOps work item linked
- [ ] Tenant isolation validated
- [ ] Input validation and security checks added
