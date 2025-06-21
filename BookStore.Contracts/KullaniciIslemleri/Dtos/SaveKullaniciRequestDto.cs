 

namespace BookStore.Contracts.KullaniciIslemleri.Dtos;
public class SaveKullaniciRequestDto
{
    public int? ID { get; set; }
    public int TenantId { get; set; }
    public string AdiSoyadi { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string CepTel { get; set; }
    public bool Aktif { get; set; } = true;
}