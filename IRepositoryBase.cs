using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace prmToolkit.EF.RepositoryBase
{
    public interface IRepositoryBase<TEntidade>
       where TEntidade : class
       
    {
        IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll);
        
        IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params Expression<Func<TEntidade, object>>[] includeProperties);
        
        IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params string[] includeProperties);
        
        IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params Expression<Func<TEntidade, object>>[] includeProperties);
        
        IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params string[] includeProperties);
        
        IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params Expression<Func<TEntidade, object>>[] includeProperties);
        
        IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params string[] includeProperties);
        
        TEntidade ObterPor(Func<TEntidade, bool> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params Expression<Func<TEntidade, object>>[] includeProperties);
        TEntidade ObterPor(Func<TEntidade, bool> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params string[] includeProperties);
        TEntidade ObterPor(Func<TEntidade, bool> where, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll);
        bool Existe(Func<TEntidade, bool> where);
        bool Existe(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] includeProperties);
        bool Existe(Func<TEntidade, bool> where, params string[] includeProperties);
        IQueryable<TEntidade> Listar(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params Expression<Func<TEntidade, object>>[] includeProperties);
        
        IQueryable<TEntidade> Listar(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, params string[] includeProperties);
        
        IQueryable<TEntidade> Listar(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll);
        
        TEntidade Adicionar(TEntidade entidade);
        TEntidade Editar(TEntidade entidade);
        void Remover(TEntidade entidade);
        void Remover(IEnumerable<TEntidade> entidades);
        void AdicionarLista(IEnumerable<TEntidade> entidades);
    }
}
