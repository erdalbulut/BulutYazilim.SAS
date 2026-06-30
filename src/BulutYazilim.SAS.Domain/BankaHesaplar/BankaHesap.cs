namespace BulutYazilim.SAS.BankaHesaplar;

public class BankaHesap : FullAuditedAggregateRoot<Guid>
{
	public string Kod { get; set; }
	public string Ad { get; set; }
	public BankaHesapTuru HesapTuru { get; set; }
	public string HesapNo { get; set; }
	public string IbanNo { get; set; }
	public Guid BankaSubeId { get; set; }
	public Guid? OzelKod1Id { get; set; } //? işareti, bu property' zorunlu olmadığını anlamına gelir, yani null olabilir.
	public Guid? OzelKod2Id { get; set; }
	public Guid SubeId { get; set; }
	public string Aciklama { get; set; }
	public bool Durum { get; set; }

	//navigation properties
	public BankaSube BankaSube { get; set; }
	public OzelKod OzelKod1 { get; set; }
	public OzelKod OzelKod2 { get; set; }
	public Sube Sube { get; set; }

	//collection navigation properties
	public ICollection<Makbuz> Makbuzlar { get; set; }
	public ICollection<MakbuzHareket> MakbuzHareketler { get; set; }

}