namespace HostelOps_API_Data.Models.Tenant;

public class Resident
{
    // Primary Key (PK)
    public int ResidentId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Pincode { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string IdProofType { get; set; } = string.Empty;

    public string IdProofNumber { get; set; } = string.Empty;

    public string ProfileImageUrl { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Properties

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public ICollection<StayAllocation> StayAllocations { get; set; } = new List<StayAllocation>();

    public ICollection<ResidentDocument> ResidentDocuments { get; set; } = new List<ResidentDocument>();

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}