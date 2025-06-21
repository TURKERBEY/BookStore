🔧 Kullanılan Mimariler ve Teknolojiler
•	Clean Architecture
•	Minimal API (.NET 8)
•	CQRS + MediatR
•	Entity Framework Core
•	Ms sql
•	Swagger
•	JWT Authentication
•	Modüler yapı
________________________________________
📁 Klasör Yapısı ve Katmanlar
1. Modules
Her modül kendi başına bağımsız bir bounded context mantığıyla geliştirilmiştir.
Her modül aşağıdaki alt katmanlardan oluşur:
•	Application:
İş kuralları, CQRS handler'lar, validation'lar, DTO'lar, ve business servisler burada bulunur.
•	Contracts:
Public API'ye sunulacak veri transfer nesneleri (DTO'lar) bu katmanda bulunur. Başka modüller bu nesnelerle haberleşebilir.
•	Domain:
DDD yaklaşıma uygun olarak entity’ler, enum’lar ve value object’ler burada bulunur.
•	Persistence:
Sadece ilgili modüle özel DbContext konfigurasyonları, repository implementasyonları burada yer alır.
________________________________________
2. Shared
Ortak kullanılacak tüm altyapı servislerini barındırır.
•	Core:
o	Middleware
o	Exception handling
o	Logging
o	JWT işlemleri
o	User session yönetimi
o	Infrastructure bağımlılıklarını soyutlayan arayüzler
o	Base entity, Auditing
•	Domain:
Tüm projeler için ortak domain modelleri. Örneğin BaseEntity, AuditableEntity gibi soyutlamalar.
•	Infrastructure:
Ortak servis sağlayıcıların gerçek implementasyonları (ör. Mail, Cache, Logger vs.)
•	Persistence:
Ortak kullanılan DbContext, genel repository yapıları burada konumlanır. Tek bir veritabanı üzerinden birden fazla modül erişim sağlar.
________________________________________
3. Services
Dış servislerle (örneğin SMS, Email, 3rd party API’ler) olan bağlantılar bu katmanda toplanır.![Api](https://github.com/user-attachments/assets/8c74f667-d676-42cc-9bd6-af1a051a1a55)



