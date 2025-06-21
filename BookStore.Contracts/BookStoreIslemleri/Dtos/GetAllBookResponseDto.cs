

namespace BookStore.Contracts.BookStoreIslemleri.Dtos;
    public class GetAllBookResponseDto
{
    public int ID { get; set; }
    public int TenantId { get; set; }
    public string KitapAdi { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}