using AutoMapper;
using CarRental.Infrastructure.Data;

namespace CarRental.Infrastructure.Repositories
{
    public abstract class BaseRepository<TDom, TInf>(AppDbContext context, IMapper mapper)
        where TDom : class 
        where TInf : BaseRecord
    {
        protected readonly AppDbContext _context = context;
        protected readonly IMapper _mapper = mapper;

        public virtual Task AddAsync(TDom entity)
        {
            var e = _mapper.Map<TInf>(entity);
            e.DateAdded = DateTime.UtcNow;
            _context.Add(e);
            return _context.SaveChangesAsync();
        }

        public virtual Task UpdateAsync(TDom entity)
        {
            var e = _mapper.Map<TInf>(entity);
            e.DateModified = DateTime.UtcNow;
            _context.Update(e);
            return _context.SaveChangesAsync();
        }

        public virtual Task DeleteAsync(TDom entity)
        {
            var e = _mapper.Map<TInf>(entity);
            _context.Remove(e);
            return _context.SaveChangesAsync();
        }
    }
}
