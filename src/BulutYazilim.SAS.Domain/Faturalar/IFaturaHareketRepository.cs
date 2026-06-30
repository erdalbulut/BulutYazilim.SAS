using BulutYazilim.SAS.Commons;

namespace BulutYazilim.SAS.Faturalar;

public interface IFaturaHareketRepository : ICommonRepository<FaturaHareket>
{
	//FaturaHareket üzerinde CRUD ilşemleri yapılmayacak. Bunları zaten FaturaRepository üzerinden yapacağız.
	//FaturaHareket Repository oluşturuyoruz çünkü Raporlama ile ilgili durumlarda FaturaHareketlere ihtiyacımız olacak. Aynı durum makbuz hareketleri için de geçerli.
}