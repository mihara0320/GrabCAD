using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


namespace GrabCAD.API.Helpers
{
    public interface IUserManager
    {
        void Add(string id);
        void Remove(string id);
        HashSet<string> GetAll();
    }

    public class UserManager : IUserManager
    {
        private HashSet<string> ConnectedIds = new HashSet<string>();

        public void Add(string id)
        {
            ConnectedIds.Add(id);
        }

        public void Remove(string id)
        {
            ConnectedIds.Remove(id);
        }

        public HashSet<string> GetAll()
        {
            return ConnectedIds;
        }
    }
}
