using GMAO_Data.DataBaseManager;
using GMAO_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Data.Repositories
{
    public class AuthentificationRepository
    {
        private readonly DbManager _context;

        public AuthentificationRepository()
        {
            _context = new DbManager();
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.username == username);
        }
    }

}
