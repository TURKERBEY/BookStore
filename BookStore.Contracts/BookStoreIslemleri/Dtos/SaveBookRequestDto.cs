using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts.BookStoreIslemleri.Dtos;
public class SaveBookRequestDto
{
    public int? ID { get; set; }
    public int TenantId { get; set; }
    public string KitapAdi { get; set; }
}