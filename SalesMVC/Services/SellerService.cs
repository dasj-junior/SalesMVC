using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMVC.Models;

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
            return _context.Seller.FirstOrDefault(obj => obj.ID == ID);
        }

        public void Remove(int ID)
        {
            var obj = _context.Seller.Find(ID);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

    }
}
