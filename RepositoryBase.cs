using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace prmToolkit.EF.RepositoryBase
{
    public class RepositoryBase<TEntidade> : IRepositoryBase<TEntidade>
       where TEntidade : class
    {
        private readonly DbContext _context;
        protected RepositoryBase()
        {

        }
        public RepositoryBase(DbContext context)
        {
            _context = context;
        }
        public IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll)
        {
            return Listar(queryTrackingBehavior).Where(where);
        }
        
        public IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return Listar(queryTrackingBehavior, includeProperties).Where(where);
        }
        
        public IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params string[] includeProperties)
        {
            return Listar(queryTrackingBehavior, includeProperties).Where(where);
        }
        
        public IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return ascendente ? ListarPor(where, queryTrackingBehavior, includeProperties).OrderBy(ordem) : ListarPor(where, queryTrackingBehavior, includeProperties).OrderByDescending(ordem);
        }
        
        public IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params string[] includeProperties)
        {
            return ascendente ? ListarPor(where, queryTrackingBehavior, includeProperties).OrderBy(ordem) : ListarPor(where, queryTrackingBehavior, includeProperties).OrderByDescending(ordem);
        }
        
        public TEntidade ObterPor(Func<TEntidade, bool> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return Listar(queryTrackingBehavior, includeProperties).FirstOrDefault(where);
        }
        public TEntidade ObterPor(Func<TEntidade, bool> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params string[] includeProperties)
        {
            return Listar(queryTrackingBehavior, includeProperties).FirstOrDefault(where);
        }
        public TEntidade ObterPor(Func<TEntidade, bool> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll)
        {
            return Listar(queryTrackingBehavior).FirstOrDefault(where);
        }
        public IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return ascendente ? Listar(queryTrackingBehavior, includeProperties).OrderBy(ordem) : Listar(queryTrackingBehavior, includeProperties).OrderByDescending(ordem);
        }
        
        public IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params string[] includeProperties)
        {
            return ascendente ? Listar(queryTrackingBehavior, includeProperties).OrderBy(ordem) : Listar(queryTrackingBehavior, includeProperties).OrderByDescending(ordem);
        }
        
        public TEntidade Adicionar(TEntidade entidade)
        {
            var entity = _context.Add<TEntidade>(entidade);
            return entity.Entity;
        }
        public TEntidade Editar(TEntidade entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;

            return entidade;
        }
        public void Remover(TEntidade entidade)
        {
            _context.Set<TEntidade>().Remove(entidade);
        }
        public void Remover(IEnumerable<TEntidade> entidades)
        {
            _context.Set<TEntidade>().RemoveRange(entidades);
        }
        public void AdicionarLista(IEnumerable<TEntidade> entidades)
        {
            _context.AddRange(entidades);
        }
        public bool Existe(Func<TEntidade, bool> where)
        {
            return _context.Set<TEntidade>().Any(where);
        }
        public bool Existe(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            if (includeProperties.Any())
            {
                return Include(_context.Set<TEntidade>(), includeProperties).Any(where);
            }
            else
            {
                return _context.Set<TEntidade>().Any(where);
            }
        }
        public bool Existe(Func<TEntidade, bool> where, params string[] includeProperties)
        {
            if (includeProperties.Any())
            {
                return Include(_context.Set<TEntidade>(), includeProperties).Any(where);
            }
            else
            {
                return _context.Set<TEntidade>().Any(where);
            }
        }
        public IQueryable<TEntidade> Listar(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll)
        {
            //Habilita o Tracking
            _context.ChangeTracker.QueryTrackingBehavior = queryTrackingBehavior;

            IQueryable<TEntidade> query = _context.Set<TEntidade>();

            return query;
        }
        
        public IQueryable<TEntidade> Listar(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            _context.ChangeTracker.QueryTrackingBehavior = queryTrackingBehavior;

            IQueryable<TEntidade> query = _context.Set<TEntidade>();

            if (includeProperties.Any())
            {
                return Include(_context.Set<TEntidade>(), includeProperties);
            }

            return query;
        }
        
        public IQueryable<TEntidade> Listar(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params string[] includeProperties)
        {
            //Tracking
            _context.ChangeTracker.QueryTrackingBehavior = queryTrackingBehavior;

            IQueryable<TEntidade> query = _context.Set<TEntidade>();

            if (includeProperties.Any())
            {
                return Include(_context.Set<TEntidade>(), includeProperties);
            }

            return query;
        }
        
        private IQueryable<TEntidade> Include(IQueryable<TEntidade> query, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            return query;
        }
        private IQueryable<TEntidade> Include(IQueryable<TEntidade> query, params string[] navProperties)
        {
            foreach (var navProperty in navProperties)
                query = query.Include(navProperty);

            return query;
        }
    }
}
