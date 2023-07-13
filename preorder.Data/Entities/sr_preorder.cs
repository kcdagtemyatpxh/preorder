using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace preorder.Data.Entities
{
    public class sr_header
    {
        public int id { get; set; }

        [Comment("Họ tên khách hàng")]
        [MaxLength(100)]
        public string name { get; set; }

        [Comment("địa chỉ")]
        [MaxLength(100)]
        public string address { get; set; }

        [Comment("ngày giao hàng")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime deliveryDate { get; set; }
    }
}
