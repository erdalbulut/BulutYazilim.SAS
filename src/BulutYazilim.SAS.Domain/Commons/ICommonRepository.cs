using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BulutYazilim.SAS.Commons;

public interface ICommonRepository<TEntity>:IRepository<TEntity,Guid> where TEntity : class, IEntity<Guid>
{
	//id gönderdiğimizde entity dönsün, predicate gönderdiyimizde predicate ile filtrelensin, includeProperties ile ilişkili tablolar gelsin
	Task<TEntity> GetAsync(object id, Expression<Func<TEntity, bool>> predicate = null,
		params Expression<Func<TEntity, object>>[] includeProperties);

	//ilgili entity çağırdığımızda bulunamadığında hata vermesin diye tanımladık. Örneğin şube ve dönem ilk girişte yoksa hata vermesin ilgili ekrana gitsin. Şube dönem seçimi ekranı.
	Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
		params Expression<Func<TEntity, object>>[] includeProperties);

	//Gönderdiğimiz id ile entity döndün. Bazı entitylerde navigation propertisi olmayanlar için kullanılacak.
	//Bazı entitylerin Icollection tipinde navigation propertyleri olabilir ve kendi içerisinde de navigation propertyleri olabilir.
	//Fatura entitysi örneğin, FaturaHareket entitysi ICollection tipinde navigation property olabilir ve FaturaDetay entitysi de kendi içerisinde navigation propertyleri olabilir. (Stok,Hizmet vb.)
	//Stok entitysi içinde de birim entitysi navigation property olabilir. Bu gibi durumlarda includeProperties ile ilişkili tabloları getirebiliriz.
	//Bunun için Include ve ThenInclude metotlarını kullanabiliriz. Ancak bu metotlar sadece navigation propertyleri olan entityler için geçerlidir. Navigation property olmayan entityler için kullanılmaz.
	Task<TEntity> GetAsync(object id, Expression<Func<TEntity, bool>> predicate = null);

	//Databaseden kayıtları çekerken skipCount ve maxResultCount parametreleri ile sayfalama yapabiliriz. predicate ile filtreleme yapabiliriz.
	//orderBy ile sıralama yapabiliriz. includeProperties ile ilişkili tabloları getirebiliriz.
	Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount,
		Expression<Func<TEntity, bool>> predicate = null,
		Expression<Func<TEntity, TKey>> orderBy = null,
		params Expression<Func<TEntity, object>>[] includeProperties);

	//ICollection tipinde navigation propertyleri olan entityler için kullanılacak. Yukarıdaki functiondan tek farkı bu.
	Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount,
		Expression<Func<TEntity, bool>> predicate = null,
		Expression<Func<TEntity, TKey>> orderBy = null);

	//Son eklenen kayıtları çekmek için kullanılacak. Örneğin son eklenen 10 kaydı çekmek için kullanılacak.
	Task<List<TEntity>> GetPagedLastListAsync<TKey>(int skipCount, int maxResultCount,
		Expression<Func<TEntity, bool>> predicate = null,
		Expression<Func<TEntity, TKey>> orderBy = null,
		params Expression<Func<TEntity, object>>[] includeProperties);

	//Entitylerin çoğunda kod alanı var. İlgili entitynin kod alanını bulup bir arttırarak yeni bir kod üretecek.
	Task<string> GetCodeAsync(Expression<Func<TEntity, string>> propertySelector,
		Expression<Func<TEntity, bool>> predicate = null);

	//Store procedure ile entityleri çekmek için kullanılacak. Sql sorgusu yada store procedure gönderilebilir.
	Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters);

	//Göndereceğimiz bir filtreye göre değer databasede var mı yok mu kontrol edecek. Örneğin kullanıcı adı, email, telefon numarası gibi alanlar için kullanılacak.
	Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);
}