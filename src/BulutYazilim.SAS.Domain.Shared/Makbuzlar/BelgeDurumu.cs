namespace BulutYazilim.SAS.Makbuzlar;

public enum BelgeDurumu : byte
{
	//Belge durumları tekrar gözden geçirilecek. BankayaTahsileGonderildi, BankadanPortfoyeIadeEdildi,BankadaTahsilEdildi vb.
	Portfoyde = 1,
	Odenecek = 2,
	CiroEdildi = 3,
	TahsilEdildi = 4,
	Odendi = 5
}