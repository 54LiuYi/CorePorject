using System;
using System.Collections.Generic;
using System.Text;
using CorePorject.Model;
using CorePorject.IRepository.Member;
using Microsoft.EntityFrameworkCore;

namespace CorePorject.Repository.Member
{
    public class ArchivesRepository : BaseRepository<Archive>, IArchivesRepository
    {
        public ArchivesRepository(HealthyDBContext context) : base(context)
        {
        }
    }
}
