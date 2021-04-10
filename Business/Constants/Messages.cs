using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public class Messages
    {
        public static string InvalidUser = "Kullanıcı bilgileri hatalı";
        public static string ColorsListed = "Renkler listelendi";
        public static string CarsListed = "Araçlar listelendi";
        public static string CarAdded = "Araç eklendi";
        public static string RentalAdded = "Kiralama bilgisi eklendi";
        public static string UserAdded = "Kullanıcı eklendi";
        public static string CarDeleted = "Araç silindi.";
        public static string UserDeleted = "Kullanıcı silindi.";
        public static string CarUpdated = "Araç güncellendi.";
        public static string UserUpdated = "Kullanıcı güncellendi.";
        public static string RentalUpdated = "Kiralama bilgisi güncellendi.";
        public static string CustomerUpdated = "Müşteri güncellendi.";
        public static string BrandAdded = "Marka eklendi.";
        public static string BrandDeleted = "Marka silindi.";
        public static string CustomerDeleted = "Müşteri silindi.";
        public static string RentalDeleted = "Kiralama bilgisi silindi.";
        public static string BrandUpdated = "Marka güncellendi";
        public static string ColorAdded = "Renk eklendi.";
        public static string ColorDeleted = "Renk silindi.";
        public static string ColorUpdated = "Renk güncellendi";
        public static string InvalidCarName = "Araba ismi en az 2 karakterden oluşmalıdır.";
        public static string InvalidDailyPrice = "Arabanın günlük fiyatı 0'dan büyük olmalıdır.";
        public static string CarNotFound = "Araç bulunamadı.";
        public static string BrandNotFound = "Marka bulunamadı.";
        public static string ColorNotFound = "Renk bulunamadı.";
        public static string CustomerNotFound = "Müşteri bulunamadı.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string RentalNotFound = "Kiralama bilgisi bulunamadı.";
        public static string BrandAlreadyExists = "Marka veritabanında zaten var!";
        public static string ColorAlreadyExists = "Renk veritabanında zaten var!";
        public static string CustomerAlreadyExists = "Bu userID ye sahip müşteri veritabanında zaten var!";
        public static string CustomerAdded = "Müşteri eklendi";
        public static string CarImageAdded = "Araba fotoğrafı eklendi";
        public static string CarImageNotFound = "Fotograf bulunamadi";
        public static string CarImageLimitExceeded = "Bir arabanın en fazla 5 fotoğrafı olabilir";
        public static string PasswordError = "Parola hatalı";
        public static string SuccesfulLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "Bu maile sahip kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
        internal static string AuthorizationDenied;
    }
}
