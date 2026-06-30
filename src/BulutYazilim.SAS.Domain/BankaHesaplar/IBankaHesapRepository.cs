using BulutYazilim.SAS.Commons;

namespace BulutYazilim.SAS.BankaHesaplar;

public interface IBankaHesapRepository : ICommonRepository<BankaHesap>
{
	//Banka Hesapla işlem yapacağımız zaman IBankaHesapRepository kullanıyoruz. ICommonRepository'den kalıtım aldığımız içinde içerisindeki functionları kullanabiliriz.
	//BankaHesap entitysinide gönderdiğimiz için de yapacağımız işlemler BankaHesap entitysi üzerinden yapılır.
	//ICommonRepository'de ortak olarak tanımlayamadığımız functionları burada tanımlanabilir.
}