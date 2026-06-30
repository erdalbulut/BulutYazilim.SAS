namespace BulutYazilim.SAS.Faturalar;

public enum FaturaHareketTuru
{
	//Burada değişiklik olacak çünkü biz depo fişini faturaya çağıracağız.
	//Depo fişinden oluşmayan faturalar içinde bir çözüm bulacağız. Örneğin hizmet ve masraf faturaları gibi.
	Stok = 1,
	Hizmet = 2,
	Masraf = 3
}