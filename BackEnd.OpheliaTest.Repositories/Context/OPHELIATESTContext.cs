using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BackEnd.OpheliaTest.Entities.Models;

namespace BackEnd.OpheliaTest.Repositories.Context
{
    public partial class OPHELIATESTContext : DbContext
    {
        public OPHELIATESTContext()
        {
        }

        public OPHELIATESTContext(DbContextOptions<OPHELIATESTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORIES");

                entity.HasIndex(e => e.Category1, "UQ_CATEGORIES")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category1)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_AT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_AT")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("CLIENTS");

                entity.HasIndex(e => new { e.Email, e.IdentificationNumber }, "UQ_CLIENT_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_AT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.IdentificationNumber).HasColumnName("IDENTIFICATION_NUMBER");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_AT")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("INVOICES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_AT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("datetime")
                    .HasColumnName("INVOICE_DATE");

                entity.Property(e => e.SellerId).HasColumnName("SELLER_ID");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_AT")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLIENT_INVOICE");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SELLER_INVOICE");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.ToTable("INVOICE_DETAIL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_AT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Cuantity).HasColumnName("CUANTITY");

                entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");

                entity.Property(e => e.Invoice).HasColumnName("INVOICE");

                entity.Property(e => e.PrinceInvoice).HasColumnName("PRINCE_INVOICE");

                entity.Property(e => e.PrinceProduct).HasColumnName("PRINCE_PRODUCT");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_AT")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INVOICE_INVOICEDETAIL");

                entity.HasOne(d => d.IdProduct1)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCT_INVOICEDETAIL");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCTS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_AT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Cuantity).HasColumnName("CUANTITY");

                entity.Property(e => e.IdCategory).HasColumnName("ID_CATEGORY");

                entity.Property(e => e.Prince).HasColumnName("PRINCE");

                entity.Property(e => e.Product1)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_AT")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCT_CATEGORIE");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.ToTable("SELLERS");

                entity.HasIndex(e => e.SellerCode, "UQ_SELLER_CODE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_AT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SellerCode)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("SELLER_CODE");

                entity.Property(e => e.SellerName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("SELLER_NAME");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_AT")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
