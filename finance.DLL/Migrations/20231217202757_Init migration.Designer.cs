﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using finance.DLL;

#nullable disable

namespace finance.DLL.Migrations
{
    [DbContext(typeof(FinanceContext))]
    [Migration("20231217202757_Init migration")]
    partial class Initmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("finance.DLL.Models.FinancialCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FinancialCategories");
                });

            modelBuilder.Entity("finance.DLL.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("LeftBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId")
                        .IsUnique();

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("finance.DLL.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FinancialCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("HistoryRecordId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ReceivingWalletId")
                        .HasColumnType("int");

                    b.Property<int>("SendingWalletId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FinancialCategoryId");

                    b.HasIndex("ReceivingWalletId");

                    b.HasIndex("SendingWalletId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("finance.DLL.Models.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes");
                });

            modelBuilder.Entity("finance.DLL.Models.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("finance.DLL.Models.History", b =>
                {
                    b.HasOne("finance.DLL.Models.Transaction", "Transaction")
                        .WithOne("HistoryRecord")
                        .HasForeignKey("finance.DLL.Models.History", "TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("finance.DLL.Models.Transaction", b =>
                {
                    b.HasOne("finance.DLL.Models.FinancialCategory", "FinancialCategory")
                        .WithMany("Transactions")
                        .HasForeignKey("FinancialCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("finance.DLL.Models.Wallet", "ReceivingWallet")
                        .WithMany("ReceivedTransactions")
                        .HasForeignKey("ReceivingWalletId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("finance.DLL.Models.Wallet", "SendingWallet")
                        .WithMany("SendTransactions")
                        .HasForeignKey("SendingWalletId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("finance.DLL.Models.TransactionType", "TransactionType")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FinancialCategory");

                    b.Navigation("ReceivingWallet");

                    b.Navigation("SendingWallet");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("finance.DLL.Models.FinancialCategory", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("finance.DLL.Models.Transaction", b =>
                {
                    b.Navigation("HistoryRecord");
                });

            modelBuilder.Entity("finance.DLL.Models.TransactionType", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("finance.DLL.Models.Wallet", b =>
                {
                    b.Navigation("ReceivedTransactions");

                    b.Navigation("SendTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
