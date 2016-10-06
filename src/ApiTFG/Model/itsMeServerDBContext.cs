using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiTFG
{
    public partial class itsMeServerDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseNpgsql(@"Host=127.0.0.1;Port=5432;Database=itsMeServerDB;Username=postgres;Password=1234;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("chat");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Chat_id_seq\"'::regclass)");

                entity.Property(e => e.EndDatetime).HasColumnName("end_datetime");

                entity.Property(e => e.InitDatetime).HasColumnName("init_datetime");

                entity.Property(e => e.UserRequest).HasColumnName("user_request");

                entity.Property(e => e.UserRequested).HasColumnName("user_requested");

                entity.HasOne(d => d.UserRequestNavigation)
                    .WithMany(p => p.ChatUserRequestNavigation)
                    .HasForeignKey(d => d.UserRequest)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("user_request");

                entity.HasOne(d => d.UserRequestedNavigation)
                    .WithMany(p => p.ChatUserRequestedNavigation)
                    .HasForeignKey(d => d.UserRequested)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("user_requested");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Message_id_seq\"'::regclass)");

                entity.Property(e => e.Chat).HasColumnName("chat");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.UserEmitter).HasColumnName("user_emitter");

                entity.Property(e => e.UserReceiver).HasColumnName("user_receiver");

                entity.HasOne(d => d.ChatNavigation)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.Chat)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("chat");

                entity.HasOne(d => d.UserEmitterNavigation)
                    .WithMany(p => p.MessageUserEmitterNavigation)
                    .HasForeignKey(d => d.UserEmitter)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("user_emitter");

                entity.HasOne(d => d.UserReceiverNavigation)
                    .WithMany(p => p.MessageUserReceiverNavigation)
                    .HasForeignKey(d => d.UserReceiver)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("user_receiver");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.BluetoothMac)
                    .HasName("bluetooth_mac_unique")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("name_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"User_id_seq\"'::regclass)");

                entity.Property(e => e.BluetoothMac)
                    .IsRequired()
                    .HasColumnName("bluetooth_mac");

                entity.Property(e => e.CityAndCountry).HasColumnName("city_and_country");

                entity.Property(e => e.FilmsTastes).HasColumnName("films_tastes");

                entity.Property(e => e.Hobbies).HasColumnName("hobbies");

                entity.Property(e => e.Job).HasColumnName("job");

                entity.Property(e => e.MusicTastes).HasColumnName("music_tastes");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.ReadingTastes).HasColumnName("reading_tastes");
            });

            modelBuilder.HasSequence("Chat_id_seq");

            modelBuilder.HasSequence("Message_id_seq");

            modelBuilder.HasSequence("User_id_seq");
        }

        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}