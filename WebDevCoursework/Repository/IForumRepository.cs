using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDevCoursework.Models;

namespace WebDevCoursework.Repository
{
    public interface IForumRepository
    {
        void Create(Forum forum);

        void Edit(Forum forum);

        Task<Forum> GetForumIdAsync(int? id);

        void Delete(Forum forum);

        List<Forum> ListOfForums();
    }
}
