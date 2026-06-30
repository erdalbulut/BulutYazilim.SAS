using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BulutYazilim.SAS.Commons;

public interface ICommonNoKeyRepository<TEntity> : IRepository<TEntity>
	where TEntity : class, IEntity
{
	//Raporlarda kullanılacak. Örnek finansal durum raporunda giren çıkan bakiye ID diye bir alan yok.
	//Tek bir veri çekmek için FromSqlRawSingleAsync kullanılacak.
	Task<TEntity> FromSqlRawSingleAsync(string sql, params object[] parameters);
	//Liste halinde veri çekmek için FromSqlRawAsync kullanılacak.
	Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters);
}