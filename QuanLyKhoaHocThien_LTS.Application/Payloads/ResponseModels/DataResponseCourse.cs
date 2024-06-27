using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.Payloads.ResponseModels
{
    public class DataResponseCourse
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Introduce { get; set; } = default!;

        public string ImageCourse { get; set; } = default!;

        public int CreatorId { get; set; }

        public string Code { get; set; } = default!;

        public decimal Price { get; set; }

        public int TotalCourseDuration { get; set; }

        public int NumberOfStudent { get; set; }

        public int NumberOfPurchases { get; set; }
    }
}
