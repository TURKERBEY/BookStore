namespace BookStore.Contracts.TenantIslemleri.Dtos;
public class GetAllTenantResponseDto
{
    public int ID { get; set; }
    public string Adi { get; set; }
    public string Number { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}