using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ListResponse<TEntity>
    {
        public List<TEntity> entities { get; set; }
        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
    }
}
