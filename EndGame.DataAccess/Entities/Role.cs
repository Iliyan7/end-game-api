using System.Collections.Generic;

namespace EndGame.DataAccess.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
