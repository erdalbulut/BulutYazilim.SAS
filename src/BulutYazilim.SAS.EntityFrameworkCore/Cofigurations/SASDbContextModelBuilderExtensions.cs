using BulutYazilim.SAS.BankaHesaplar;
using BulutYazilim.SAS.Bankalar;
using BulutYazilim.SAS.BankaSubeler;
using BulutYazilim.SAS.Birimler;
using BulutYazilim.SAS.Cariler;
using BulutYazilim.SAS.Consts;
using BulutYazilim.SAS.Depolar;
using BulutYazilim.SAS.Donemler;
using BulutYazilim.SAS.Faturalar;
using BulutYazilim.SAS.Hizmetler;
using BulutYazilim.SAS.Kasalar;
using BulutYazilim.SAS.MakbuzHareketler;
using BulutYazilim.SAS.Makbuzlar;
using BulutYazilim.SAS.Masraflar;
using BulutYazilim.SAS.Ozelkodlar;
using BulutYazilim.SAS.Parametreler;
using BulutYazilim.SAS.Stoklar;
using BulutYazilim.SAS.Subeler;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace BulutYazilim.SAS.Cofigurations;

public static class SASDbContextModelBuilderExtensions
{
	public static void ConfigureBanka(this ModelBuilder builder)
	{
		builder.Entity<Banka>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Bankalar", SASConsts.DbSchema);
			b.ConfigureByConvention(); //auto configure for the base class props

			//properties
			b.Property(x => x.Kod)
				.IsRequired() //zorunlu olmasını sağlar
				.HasColumnType(SqlDbType.VarChar.ToString()) //sql serverde kolonun tipini belirler. SqlDbType enum unu kullanıyoruz. Enum olduğu için ToString() ile stringe çeviriyoruz.
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString()); //Guid in sql serverde karşılığı uniqueidentifier dir.

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString()); //bool un sql serverde karşılığı bit dir.

			//indexs
			//sql serverde yazmış olduğumuz sorguda where koşulunda kullandığımız propertiyler için index oluşturmak performansı artırır.
			b.HasIndex(x => x.Kod); //Arama ve sıralama işlemlerinde performansı artırmak için index oluşturuyoruz.

			//relations
			//Banka tablosu ile OzelKod1 tablosu arasında bire çok ilişki var. Bir banka bir OzelKod1 e ait olabilir. Bir OzelKod1 e birden fazla banka ait olabilir.
			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1Bankalar)
				.OnDelete(DeleteBehavior.NoAction); //silme işlemi yapıldığında ilişkili tabloyu silme. NoAction ile ilişkili tabloyu silme işlemi yapmaz.
			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2Bankalar)
				.OnDelete(DeleteBehavior.NoAction);
		});



	}

	public static void ConfigureBankaSube(this ModelBuilder builder)
	{
		builder.Entity<BankaSube>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "BankaSubeler", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.BankaId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
			b.HasOne(x => x.Banka)
				.WithMany(x => x.BankaSubeler)
				.OnDelete(DeleteBehavior.Cascade); //Cascade ile silme işlemi yapıldığında ilişkili tabloyu da siler. Banka silindiğinde ilişkili banka şubeleri de silinir.

			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1BankaSubeler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2BankaSubeler)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureBankaHesap(this ModelBuilder builder)
	{
		builder.Entity<BankaHesap>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "BankaHesaplar", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.HesapTuru)
				.IsRequired()
				.HasColumnType(SqlDbType.TinyInt.ToString());

			b.Property(x => x.HesapNo)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(BankaHesapConsts.MaxHesapNoLength); //Domain.Shared projesinde tanımladığımız BankaHesapConsts sınıfındaki sabitini kullanıyoruz.

			b.Property(x => x.IbanNo)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(BankaHesapConsts.MaxIbanNoLength);

			b.Property(x => x.BankaSubeId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.SubeId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
			b.HasOne(x => x.BankaSube)
				.WithMany(x => x.BankaHesaplar)
				.OnDelete(DeleteBehavior.Cascade);

			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1BankaHesaplar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2BankaHesaplar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Sube)
				.WithMany(x => x.BankaHesaplar)
				.OnDelete(DeleteBehavior.Cascade);
		});
	}

	public static void ConfigureBirim(this ModelBuilder builder)
	{
		builder.Entity<Birim>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Birimler", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1Birimler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2Birimler)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureCari(this ModelBuilder builder)
	{
		builder.Entity<Cari>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Cariler", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.VergiDairesi)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(CariConsts.MaxVergiDairesiLength); //Domain.Shared projesinde tanımladığımız CariConsts sınıfındaki sabitini kullanıyoruz.

			b.Property(x => x.VergiNo)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(CariConsts.MaxVergiNoLength);

			b.Property(x => x.Telefon)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxTelefonLength);

			b.Property(x => x.Adres)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdresLength);

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1Cariler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2Cariler)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureDepo(this ModelBuilder builder)
	{
		builder.Entity<Depo>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Depolar", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.SubeId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1Depolar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2Depolar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Sube)
				.WithMany(x => x.Depolar)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureDonem(this ModelBuilder builder)
	{
		builder.Entity<Donem>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Donemler", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
		});
	}

	public static void ConfigureFatura(this ModelBuilder builder)
	{
		builder.Entity<Fatura>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Faturalar", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.FaturaTuru)
				.IsRequired()
				.HasColumnType(SqlDbType.TinyInt.ToString());

			b.Property(x => x.FaturaNo)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(FaturaConsts.MaxFaturaNoLength); //Domain.Shared projesinde tanımladığımız FaturaConsts sınıfındaki sabitini kullanıyoruz.

			b.Property(x => x.Tarih)
				.IsRequired()
				.HasColumnType(SqlDbType.Date.ToString());

			b.Property(x => x.BrutTutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.IndirimTutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.KdvHaricTutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.KdvTutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.NetTutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.HareketSayisi)
				.IsRequired()
				.HasColumnType(SqlDbType.Int.ToString());

			b.Property(x => x.CariId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.SubeId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.DonemId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.FaturaNo);

			//relations
			b.HasOne(x => x.Cari)
				.WithMany(x => x.Faturalar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1Faturalar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2Faturalar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Sube)
				.WithMany(x => x.Faturalar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Donem)
				.WithMany(x => x.Faturalar)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureFaturaHareket(this ModelBuilder builder)
	{
		builder.Entity<FaturaHareket>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "FaturaHareketler", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.FaturaId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.HareketTuru)
				.IsRequired()
				.HasColumnType(SqlDbType.TinyInt.ToString());

			b.Property(x => x.StokId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.HizmetId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.MasrafId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.DepoId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Miktar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.BirimFiyat)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.BrutTutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.IndirimTutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.KdvOrani)
				.IsRequired()
				.HasColumnType(SqlDbType.Int.ToString());

			b.Property(x => x.KdvHaricTutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.KdvTutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.NetTutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			//indexs

			//relations
			b.HasOne(x => x.Fatura)
				.WithMany(x => x.FaturaHareketler)
				.OnDelete(DeleteBehavior.Cascade); //Cascade ile silme işlemi yapıldığında ilişkili tabloyu da siler. Fatura silindiğinde ilişkili fatura hareketleri de silinir.

			b.HasOne(x => x.Stok)
				.WithMany(x => x.FaturaHareketler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Hizmet)
				.WithMany(x => x.FaturaHareketler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Masraf)
				.WithMany(x => x.FaturaHareketler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Depo)
				.WithMany(x => x.FaturaHareketler)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureFirmaParametre(this ModelBuilder builder)
	{
		builder.Entity<FirmaParametre>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "FirmaParametreler", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.UserId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.SubeId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.DonemId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.DepoId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			//indexs

			//relations
			b.HasOne(x => x.User)
				.WithOne()
				.HasForeignKey<FirmaParametre>(x => x.UserId);

			b.HasOne(x => x.Sube)
				.WithMany(x => x.FirmaParemetreler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Donem)
				.WithMany(x => x.FirmaParametreler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Depo)
				.WithMany(x => x.FirmaParametreler)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureHizmet(this ModelBuilder builder)
	{
		builder.Entity<Hizmet>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Hizmetler", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.KdvOrani)
				.IsRequired()
				.HasColumnType(SqlDbType.Int.ToString());

			b.Property(x => x.BirimFiyat)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.Barkod)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxBarkodLength);

			b.Property(x => x.BirimId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
			b.HasOne(x => x.Birim)
				.WithMany(x => x.Hizmetler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1Hizmetler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2Hizmetler)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureKasa(this ModelBuilder builder)
	{
		builder.Entity<Kasa>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Kasalar", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.SubeId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1Kasalar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2Kasalar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Sube)
				.WithMany(x => x.Kasalar)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureMakbuz(this ModelBuilder builder)
	{
		builder.Entity<Makbuz>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Makbuzlar", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.MakbuzTuru)
				.IsRequired()
				.HasColumnType(SqlDbType.TinyInt.ToString());

			b.Property(x => x.MakbuzNo)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(MakbuzConsts.MaxMakbuzNoLength);

			b.Property(x => x.Tarih)
				.IsRequired()
				.HasColumnType(SqlDbType.Date.ToString());

			b.Property(x => x.CariId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.KasaId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.BankaHesapId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.HareketSayisi)
				.IsRequired()
				.HasColumnType(SqlDbType.Int.ToString());

			b.Property(x => x.CekToplam)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.SenetToplam)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.PosToplam)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.NakitToplam)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.BankaToplam)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.SubeId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.DonemId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.MakbuzNo);

			//relations
			b.HasOne(x => x.Cari)
				.WithMany(x => x.Makbuzlar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Kasa)
				.WithMany(x => x.Makbuzlar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.BankaHesap)
				.WithMany(x => x.Makbuzlar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1Makbuzlar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2Makbuzlar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Sube)
				.WithMany(x => x.Makbuzlar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Donem)
				.WithMany(x => x.Makbuzlar)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureMakbuzHareket(this ModelBuilder builder)
	{
		builder.Entity<MakbuzHareket>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "MakbuzHareketler", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.MakbuzId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OdemeTuru)
				.IsRequired()
				.HasColumnType(SqlDbType.TinyInt.ToString());

			b.Property(x => x.TakipNo)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(MakbuzHareketConsts.MaxTakipNoLength);

			b.Property(x => x.CekBankaId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.CekBankaSubeId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.CekHesapNo)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(MakbuzHareketConsts.MaxCekHesapNoLength);

			b.Property(x => x.BelgeNo)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(MakbuzHareketConsts.MaxBelgeNoLength);

			b.Property(x => x.Vade)
				.IsRequired()
				.HasColumnType(SqlDbType.Date.ToString());

			b.Property(x => x.AsilBorclu)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(MakbuzHareketConsts.MaxAsilBorcluLength);

			b.Property(x => x.Ciranta)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(MakbuzHareketConsts.MaxCirantaLength);

			b.Property(x => x.KasaId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.BankaHesapId)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Tutar)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.BelgeDurumu)
				.IsRequired()
				.HasColumnType(SqlDbType.TinyInt.ToString());

			b.Property(x => x.KendiBelgemiz)
				.HasColumnType(SqlDbType.Bit.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			//indexs
			b.HasIndex(x => x.TakipNo);

			//relations
			b.HasOne(x => x.Makbuz)
				.WithMany(x => x.MakbuzHareketler)
				.OnDelete(DeleteBehavior.Cascade);

			b.HasOne(x => x.CekBanka)
				.WithMany(x => x.MakbuzHareketler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.CekBankaSube)
				.WithMany(x => x.MakbuzHareketler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.Kasa)
				.WithMany(x => x.MakbuzHareketler)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.BankaHesap)
				.WithMany(x => x.MakbuzHareketler)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureMasraf(this ModelBuilder builder)
	{
		builder.Entity<Masraf>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Masraflar", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.KdvOrani)
				.IsRequired()
				.HasColumnType(SqlDbType.Int.ToString());

			b.Property(x => x.BirimFiyat)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.Barkod)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxBarkodLength);

			b.Property(x => x.BirimId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
			b.HasOne(x => x.Birim)
				.WithMany(x => x.Masraflar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1Masraflar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2Masraflar)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureOzelKod(this ModelBuilder builder)
	{
		builder.Entity<OzelKod>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "OzelKodlar", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.KodTuru)
				.IsRequired()
				.HasColumnType(SqlDbType.TinyInt.ToString());

			b.Property(x => x.KartTuru)
				.IsRequired()
				.HasColumnType(SqlDbType.TinyInt.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
		});
	}

	public static void ConfigureStok(this ModelBuilder builder)
	{
		builder.Entity<Stok>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Stoklar", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.KdvOrani)
				.IsRequired()
				.HasColumnType(SqlDbType.Int.ToString());

			b.Property(x => x.BirimFiyat)
				.IsRequired()
				.HasColumnType(SqlDbType.Money.ToString());

			b.Property(x => x.Barkod)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxBarkodLength);

			b.Property(x => x.BirimId)
				.IsRequired()
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod1Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.OzelKod2Id)
				.HasColumnType(SqlDbType.UniqueIdentifier.ToString());

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
			b.HasOne(x => x.Birim)
				.WithMany(x => x.Stoklar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod1)
				.WithMany(x => x.OzelKod1Stoklar)
				.OnDelete(DeleteBehavior.NoAction);

			b.HasOne(x => x.OzelKod2)
				.WithMany(x => x.OzelKod2Stoklar)
				.OnDelete(DeleteBehavior.NoAction);
		});
	}

	public static void ConfigureSube(this ModelBuilder builder)
	{
		builder.Entity<Sube>(b =>
		{
			b.ToTable(SASConsts.DbTablePrefix + "Subeler", SASConsts.DbSchema);
			b.ConfigureByConvention();

			//properties
			b.Property(x => x.Kod)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxKodLength);

			b.Property(x => x.Ad)
				.IsRequired()
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAdLength);

			b.Property(x => x.Aciklama)
				.HasColumnType(SqlDbType.VarChar.ToString())
				.HasMaxLength(EntityConsts.MaxAciklamaLength);

			b.Property(x => x.Durum)
				.HasColumnType(SqlDbType.Bit.ToString());

			//indexs
			b.HasIndex(x => x.Kod);

			//relations
		});
	}
}