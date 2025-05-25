# İK Sitesi Onion Architecture

Bu proje, Onion Architecture kullanılarak geliştirilmiş bir İnsan Kaynakları (İK) yönetim sistemidir. Proje, Web API tabanlı ve modüler bir yapıya sahiptir.

## 🚧 Durum
Proje, Web API yapısı ve katmanlı mimari ile geliştirilmiştir.

## 🔧 Kullanılan Teknolojiler
- ASP.NET Core Web API
- Onion Architecture
- Entity Framework Core
- MSSQL Server

## 📁 Proje Yapısı
- `IK.Api`: Web API
- `IK.ApplicationLayer`: Servisler ve DTO’lar
- `IK.InfrastructureLayer`: Veri erişimi
- `IK.CoreLayer`: Temel iş mantığı

## 🛠️ Kurulum
1. `git clone https://github.com/oguzhnkorkmz/IKSitesi.Onion.git`
2. `dotnet ef database update` ile veritabanını güncelleyin.
3. `appsetting.json daki smpt` ayarlarınızı yaparak mail göndermeyi etkinleştirin. 
4. Projeyi başlatın.

## 📝 Katkı
Katkı sağlamak için `Pull Request` gönderebilirsiniz.
