using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMVC.Models;
using SalesMVC.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace SalesMVC.Services
{
    public class SellerService
    {
        private readonly SalesMVCContext _context;

        public SellerService(SalesMVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindByID(int ID)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.ID == ID);
        }

        public void Remove(int ID)
        {
            var obj = _context.Seller.Find(ID);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if(!_context.Seller.Any(x => x.ID == obj.ID))
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
