namespace HostelOps_API_Data.Models.Tenant;

public class ResidentDocument
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int DocumentId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Residents.ResidentId
    // =========================
    public int ResidentId { get; set; }

    public string DocumentType { get; set; } = string.Empty;

    public string DocumentNumber { get; set; } = string.Empty;

    public string FileUrl { get; set; } = string.Empty;

    public DateTime UploadedAt { get; set; }

    public bool IsVerified { get; set; }

    public int? VerifiedBy { get; set; }

    public DateTime? VerifiedAt { get; set; }

    // Navigation Property

    public Resident Resident { get; set; } = null!;
}