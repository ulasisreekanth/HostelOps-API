# HostelOps API — Agent & Contribution Rules

## Pull Request Instructions

Summarized from existing AIMunshi PR / branch standards.

---

### Branch Naming

| Type    | Format                                        | PR Title                  |
|---------|-----------------------------------------------|---------------------------|
| Feature | `features/<work-item-id>-<short-description>` | `[Feature-<id>] <title>`  |
| Bug     | `bug/<work-item-id>-<short-description>`      | `[Bug-<id>] <title>`      |
| Hotfix  | `hotfix/<work-item-id>-<short-description>`   | `[Hotfix-<id>] <title>`   |

**Examples:**
- `features/8955-AddTenantAIModel`
- `bug/4678-QuickBooksAuthFix`
- `hotfix/5022-TenantIsolationBreach`

#### Rules
- Lowercase hyphens for description; no spaces or special characters.
- Link work item; include `Closes #<id>` in the PR body.
- Commits: clear, conventional when applicable, reference work item IDs, atomic.

---

### PR Templates

#### Feature
- **What** / **Why** (ticket/user story)
- **Changes** (by file path)
- **Testing** (unit %, integration, E2E if relevant)
- **Breaking Changes**
- **Closes**: `#<ticket>`

#### Bug
- **What** / **Why** (impact + ticket)
- **Root Cause**
- **Fix** (by path)
- **Testing** (unit + regression)
- **Breaking Changes**
- **Closes**: `#<ticket>`

#### Hotfix
- Same as Bug, plus **Risk Mitigation** (rollback/flags/monitoring) and **Post-Release Validation**
- **Closes**: `#<ticket>`

---

### Enterprise Principles Check

Every PR must satisfy all of the following before merge:

- Works — feature behaves as specified
- Secure — no new vulnerabilities introduced
- Scales — no O(n^2) hotpaths, async-safe, tenant-isolated
- Maintainable — clear naming, documented, no dead code
- User Impact — UX / downstream impact considered

---

### Quality Checklist

- [ ] All tests pass
- [ ] Coverage >= 90% (line and branch) where required by team process
- [ ] Zero build warnings
- [ ] Zero high/critical security issues
- [ ] Correct PR template completed
- [ ] Branch naming convention followed
- [ ] Conventional commit messages
- [ ] Azure DevOps work item linked
- [ ] Tenant isolation validated
- [ ] Input validation and security checks added



