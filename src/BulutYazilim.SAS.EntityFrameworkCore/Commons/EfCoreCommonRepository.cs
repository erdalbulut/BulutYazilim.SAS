using BulutYazilim.SAS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Commons;

public class EfCoreCommonRepository<TEntity> : EfCoreRepository<SASDbContext, TEntity, Guid>,
	ICommonRepository<TEntity> where TEntity : class, IEntity<Guid>
{
	public EfCoreCommonRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}

	public async Task<TEntity> GetAsync(object id, Expression<Func<TEntity, bool>> predicate = null,
	params Expression<Func<TEntity, object>>[] includeProperties)
	{
		//WithDetailsAsync ile beraber dataları getirirken includeProperties parametresi ile birlikte vermiş olduğum navigation propertileri de dolu getir.
		//Bu sayede, belirli bir id'ye sahip olan entity ve ilişkili verileri tek bir sorguda alabilirsiniz.
		var queryable = await WithDetailsAsync(includeProperties);

		TEntity entity; 

		if (predicate != null) //Kullanıcı bir predicate (koşul) vermişse
		{
			entity = await queryable.FirstOrDefaultAsync(predicate); //FirstOrDefaultAsync kullanıyoruz çünkü null veri gelebilir hata vermemesi için.
			if (entity == null)
				throw new EntityNotFoundException(typeof(TEntity), id); //EntityNotFoundException kullanarak hata mesajı verdiriyoruz.
			return entity;
		}

		entity = await queryable.FirstOrDefaultAsync();
		if (entity == null)
			throw new EntityNotFoundException(typeof(TEntity), id);
		return entity;
	}

	public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
		params Expression<Func<TEntity, object>>[] includeProperties)
	{
		var queryable = await WithDetailsAsync(includeProperties);

		if (predicate != null)
			return await queryable.FirstOrDefaultAsync(predicate);

		return await queryable.FirstOrDefaultAsync();
	}

	public async Task<TEntity> GetAsync(object id, Expression<Func<TEntity, bool>> predicate = null)
	{
		var queryable = await WithDetailsAsync();

		TEntity entity;

		if (predicate != null)
		{
			entity = await queryable.FirstOrDefaultAsync(predicate);
			if (entity == null)
				throw new EntityNotFoundException(typeof(TEntity), id);
			return entity;
		}

		entity = await queryable.FirstOrDefaultAsync();
		if (entity == null)
			throw new EntityNotFoundException(typeof(TEntity), id);
		return entity;
	}

	public async Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount,
		Expression<Func<TEntity, bool>> predicate = null,
		Expression<Func<TEntity, TKey>> orderBy = null,
		params Expression<Func<TEntity, object>>[] includeProperties)
	{
		var queryable = await WithDetailsAsync(includeProperties);

		if (predicate != null)
			queryable = queryable.Where(predicate);

		if (orderBy != null)
			queryable = queryable.OrderBy(orderBy);

		return await queryable
			.Skip(skipCount)
			.Take(maxResultCount)
			.ToListAsync();
	}

	public async Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount,
		Expression<Func<TEntity, bool>> predicate = null,
		Expression<Func<TEntity, TKey>> orderBy = null)
	{
		var queryable = await WithDetailsAsync();

		if (predicate != null)
			queryable = queryable.Where(predicate);

		if (orderBy != null)
			queryable = queryable.OrderBy(orderBy);

		return await queryable
			.Skip(skipCount)
			.Take(maxResultCount)
			.ToListAsync();
	}

	public async Task<List<TEntity>> GetPagedLastListAsync<TKey>(int skipCount, int maxResultCount,
		Expression<Func<TEntity, bool>> predicate = null,
		Expression<Func<TEntity, TKey>> orderBy = null,
		params Expression<Func<TEntity, object>>[] includeProperties)
	{
		var queryable = await WithDetailsAsync(includeProperties);

		if (predicate != null)
			queryable = queryable.Where(predicate);

		if (orderBy != null)
			queryable = queryable.OrderByDescending(orderBy);

		return await queryable
			.Skip(skipCount)
			.Take(maxResultCount)
			.ToListAsync();
	}

	public async Task<string> GetCodeAsync(Expression<Func<TEntity, string>> propertySelector,
		Expression<Func<TEntity, bool>> predicate = null) //Pazar-01-Kasa-001
	{
		static string CreateNewCode(string code)
		{
			var number = "";

			foreach (var character in code)
			{
				if (char.IsDigit(character))
					number += character;
				else
					number = ""; //Pazar-01-Kasa-001 örnek gibi sayısal değerden sonra string değer gelirse number sıfırlanır ve sadece son sayısal değer alınır.
			}

			//number boş ise "1" olarak başlatılır, değilse number'ı long.Parse ile sayıya çevirip 1 eklenir ve tekrar string'e çevrilir.
			var newNumber = number == "" ? "1" : (long.Parse(number) + 1).ToString();
			var difference = code.Length - newNumber.Length; //pazar-001=9 -7 = 2
			if (difference < 0) //Cari-9999 10000 gibi bir durumda difference negatif olursa 0 olarak ayarlanır.
				difference = 0;

			var newCode = code.Substring(0, difference); //Cari-
			newCode += newNumber; //Cari-10000 gibi bir değer döndürür.

			return newCode;
		}

		var dbSet = await GetDbSetAsync(); //GetDbSetAsync metodu ile DbSet<TEntity> nesnesini alıyoruz.
		var maxCode = predicate == null ?
			await dbSet.MaxAsync(propertySelector) : //predicate null ise propertySelector ile belirtilen property'nin maksimum değerini alıyoruz. Kod gönderirsek kodun ad gönderirsek adın maksimum değerini alıyoruz.
			await dbSet.Where(predicate).MaxAsync(propertySelector); //eğer predicate verilmişse, sadece predicate koşulunu sağlayan entity'ler arasında propertySelector ile belirtilen property'nin maksimum değerini alıyoruz.
		return maxCode == null ? "0000000000000001" : CreateNewCode(maxCode);
	}

	public async Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters)
	{
		var context = await GetDbContextAsync();
		return await context.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();
	}

	public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
	{
		var dbSet = await GetDbSetAsync();
		return predicate == null ? await dbSet.AnyAsync() : await dbSet.AnyAsync(predicate);
	}
}