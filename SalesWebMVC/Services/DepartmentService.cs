using SalesWebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context; // Objeto de contexto de comunicacao com banco de dados.

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync() // funcao assincrona que retorna uma task
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); // await informa o compilador que a chamada e assincrona
        }

    }
}
