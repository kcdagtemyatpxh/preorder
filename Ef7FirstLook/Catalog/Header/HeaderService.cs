using Ef7FirstLook.Catalog.Header.Dtos;
using Ef7FirstLook.Data;
using Ef7FirstLook.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ef7FirstLook.Catalog.Header
{
    public class HeaderService : IHeaderService
    {
        private readonly DataContext _context;
        public HeaderService(DataContext DbContext)
        {
            _context = DbContext;
        }
        public async Task<int> Create(HeaderCreateRequest request)
        {
            var header = new sr_header()
            {
               name = "Tiêu đề",
               address = "Nơ Trang Long",
               deliveryDate = DateTime.Now,
            };
            _context.sr_header.Add(header);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(HeaderCreateRequest request)
        {
            var data = await _context.sr_header.FindAsync(1);
            if (data is null)
            {
                return 0;
            }
            data.name = "Tiêu đề 2";
            data.address = "Phan Đăng Lưu";

            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var data = await _context.sr_header.FindAsync(id);
            _context.sr_header.Remove(data);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<List<HeaderViewModel>> GetAll()
        {
            var query = from p in _context.sr_header select new { p };
            int total = query.Count();
            var data = await query.Select(x => new HeaderViewModel()
            {
                id = x.p.id,
                name = x.p.name,
                address = x.p.address,
            }).ToListAsync();
            return data;
        }


        public async Task<List<HeaderViewModel>> GetDetail(int id = 0)
        {
            var query = from p in _context.sr_header select new { p };
            if(id > 0)
            {
                query = query.Where(x => x.p.id == id);
            }
            int total = query.Count();
            var data = await query.Select(x => new HeaderViewModel()
            {
                id = x.p.id,
                name = x.p.name,
                address = x.p.address,
            }).ToListAsync();
            return data;
        }
    }
}
