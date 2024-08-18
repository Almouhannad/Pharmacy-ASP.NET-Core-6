using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.General.Logs;
using Pharmacy.Core.Entities.General.Users;

namespace Pharmacy.Infrastructure.Data
{
    public partial class PharmacyDbContext : IdentityDbContext<ApplicationUser>
    {
        public PharmacyDbContext()
        {
        }

        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options)
            : base(options)
        {
        }

        #region DbSets
        public virtual DbSet<Case> Cases { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Medicine> Medicines { get; set; } = null!;
        public virtual DbSet<MedicineCase> MedicineCases { get; set; } = null!;
        public virtual DbSet<MedicineIngredient> MedicineIngredients { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<AddNewCaseLog> AddNewCaseLogs { get; set; } = null!;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Case>(entity =>
            {
                entity.ToTable("Case");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Case_Patient");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.ToTable("Medicine");

                entity.Property(e => e.Company).HasMaxLength(50);

                entity.Property(e => e.Dose).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Form).HasMaxLength(50);

                entity.Property(e => e.ScientificName).HasMaxLength(50);

                entity.Property(e => e.TradeName).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Medicine_Category");
            });

            modelBuilder.Entity<MedicineCase>(entity =>
            {
                entity.ToTable("MedicineCase");

                entity.HasIndex(e => new { e.MedicineId, e.CaseId }, "Unique_MedicineId_CaseId")
                    .IsUnique();

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.MedicineCases)
                    .HasForeignKey(d => d.CaseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MedicineCase_Case");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.MedicineCases)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MedicineCase_Medicine");
            });

            modelBuilder.Entity<MedicineIngredient>(entity =>
            {
                entity.ToTable("MedicineIngredient");

                entity.HasIndex(e => new { e.IngredientId, e.MedicineId }, "Unique_MedicineId_IngredientId")
                    .IsUnique();

                entity.Property(e => e.Ratio).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.MedicineIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MedicineIngredient_Ingredient");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.MedicineIngredients)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MedicineIngredient_Medicine");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(d => d.Patient)
                   .WithMany()
                   .HasForeignKey(d => d.PatientId)
                   .OnDelete(DeleteBehavior.Cascade);
            });

            //OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
