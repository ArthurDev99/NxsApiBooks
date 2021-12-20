using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiBooks.Models.DbModels
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseOracle("user id=system;password=123456;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=xe)))", options => options.UseOracleSQLCompatibility("11"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("AUTHOR");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("AUTHOR_ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.Birthday)
                    .HasColumnName("BIRTHDAY")
                    .HasColumnType("DATE");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.Hometown)
                    .HasColumnName("HOMETOWN")
                    .HasColumnType("VARCHAR2(65)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR2(80)");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("BOOK");

                entity.Property(e => e.BookId)
                    .HasColumnName("BOOK_ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("AUTHOR_ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("GENDER")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.NumberPages)
                    .HasColumnName("NUMBER_PAGES")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.PublicationYear)
                    .HasColumnName("PUBLICATION_YEAR")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("TITLE")
                    .HasColumnType("VARCHAR2(80)");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AUTHOR_BOOK");
            });

            modelBuilder.HasSequence("LOGMNR_EVOLVE_SEQ$");

            modelBuilder.HasSequence("LOGMNR_SEQ$");

            modelBuilder.HasSequence("LOGMNR_UIDS$");

            modelBuilder.HasSequence("MVIEW$_ADVSEQ_GENERIC");

            modelBuilder.HasSequence("MVIEW$_ADVSEQ_ID");

            modelBuilder.HasSequence("REPCAT_LOG_SEQUENCE");

            modelBuilder.HasSequence("REPCAT$_EXCEPTIONS_S");

            modelBuilder.HasSequence("REPCAT$_FLAVOR_NAME_S");

            modelBuilder.HasSequence("REPCAT$_FLAVORS_S");

            modelBuilder.HasSequence("REPCAT$_REFRESH_TEMPLATES_S");

            modelBuilder.HasSequence("REPCAT$_REPPROP_KEY");

            modelBuilder.HasSequence("REPCAT$_RUNTIME_PARMS_S");

            modelBuilder.HasSequence("REPCAT$_TEMP_OUTPUT_S");

            modelBuilder.HasSequence("REPCAT$_TEMPLATE_OBJECTS_S");

            modelBuilder.HasSequence("REPCAT$_TEMPLATE_PARMS_S");

            modelBuilder.HasSequence("REPCAT$_TEMPLATE_REFGROUPS_S");

            modelBuilder.HasSequence("REPCAT$_TEMPLATE_SITES_S");

            modelBuilder.HasSequence("REPCAT$_USER_AUTHORIZATIONS_S");

            modelBuilder.HasSequence("REPCAT$_USER_PARM_VALUES_S");

            modelBuilder.HasSequence("TEMPLATE$_TARGETS_S");
        }
    }
}
