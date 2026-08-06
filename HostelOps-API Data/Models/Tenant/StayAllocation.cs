namespace HostelOps_API_Data.Models.Tenant;

public class StayAllocation
{
    // =========================
    // Primary Key (PK)
    // =========================
    public int AllocationId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Reservations.ReservationId
    // Nullable
    // =========================
    public int? ReservationId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Residents.ResidentId
    // =========================
    public int ResidentId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers Beds.BedId
    // =========================
    public int BedId { get; set; }

    // =========================
    // Foreign Key (FK)
    // Refers RentPlans.RentPlanId
    // =========================
    public int RentPlanId { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly? CheckOutDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal SecurityDeposit { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Properties

    public Reservation? Reservation { get; set; }

    public Resident Resident { get; set; } = null!;

    public Bed Bed { get; set; } = null!;

    public RentPlan RentPlan { get; set; } = null!;
}