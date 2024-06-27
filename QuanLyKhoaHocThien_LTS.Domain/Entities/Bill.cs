using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.Entities
{
    public class Bill
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public decimal Price { get; set; }

        public string TradingCode { get; set; } = default!;

        public DateTime CreateTime { get; set; }

        public int BillStatusId { get; set; }

        public User User { get; set; } = default!;
        public ICollection<BillStatus> billStatuses { get; set; }

        public Course Course { get; set; } = default!;
    }
}
