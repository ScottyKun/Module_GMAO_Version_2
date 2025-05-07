using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class InscriptionRepository
    {
        private readonly DbManager _context;

        public InscriptionRepository()
        {
            _context = new DbManager();
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Any(u => u.username == username);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }

}
