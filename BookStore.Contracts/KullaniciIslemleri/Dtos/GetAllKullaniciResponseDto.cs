using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts.KullaniciIslemleri.Dtos;
public class GetAllKullaniciResponseDto
{
    public int ID { get; set; }
    public int TenantId { get; set; }
    public string AdiSoyadi { get; set; }
    public string UserName { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}
