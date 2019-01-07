using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDevCoursework.Models;

namespace WebDevCoursework.Repository
{
    public class IForumRepository
    {
        void Create(Forum forum);

        void Edit(Forum forum);

        Forum GetForumId(int id);

        void Delete(Forum forum);

        List<Forum> ListOfForums();
    }
}
