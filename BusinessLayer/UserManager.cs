using BusinessLayer.Abstract;
using BusinessLayer.Result;
using Common.Helpers;
using Entities;
using Entities.Messages;
using Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BusinessLayer
{
    public class UserManager : MyEntityBase<Users>
    {
        public BusinessLayerResult<Users> res = new BusinessLayerResult<Users>();

        public BusinessLayerResult<Users> RegisterUser(RegisterViewModel data)
        {
            res.Result = Find(x => x.Username == data.Username || x.Email == data.Email);

            if(res.Result != null)
            {
                if(res.Result.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EMailAlreadyExists, "Bu email adresi zaten kayıtlı");
                }
                else if (res.Result.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Bu kullanıcı adı zaten kayıtlı");
                }
            }
            else
            {
                int dbResult = Insert(new Users()
                {
                    Name = data.Name,
                    Surname = data.Surname,
                    Username = data.Username,
                    Email = data.Email,
                    Twitter = data.Twitter,
                    Instagram = data.Instagram,
                    Facebook = data.Facebook,
                    Linkedin = data.Linkedin,
                    Degree = data.Degree,
                    Task = data.Task,
                    IsAdmin = false,
                    Password = Crypto.Hash(data.Password.ToString(), "MD5"),
                    ProfilImage = "user.png"
                }) ;

                if(dbResult == 0)
                {
                    res.AddError(ErrorMessageCode.CouldNotUserRegister, "Kullanıcı kayıt edilirken hata oluştu");
                }
            }

            return res;
        }

        public BusinessLayerResult<Users> UpdateUser(Users data)
        {
            res.Result = Find(x => x.Email == data.Email || x.Username == data.Username);

            if(res.Result != null && res.Result.Id != data.Id)
            {
                if(res.Result.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EMailAlreadyExists, "Email adresi zaten kayıtlı");
                }

                if (res.Result.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı zaten kayıtlı");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);

            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Email = data.Email;
            res.Result.Username = data.Username;
            res.Result.Twitter = data.Twitter;
            res.Result.Instagram = data.Instagram;
            res.Result.Facebook = data.Facebook;
            res.Result.Linkedin = data.Linkedin;
            res.Result.Degree = data.Degree;
            res.Result.Task = data.Task;
            res.Result.IsAdmin = data.IsAdmin;
            if (string.IsNullOrEmpty(data.ProfilImage) == false)
            {
                res.Result.ProfilImage = data.ProfilImage; 
            }
            if(data.Password != null)
            {
                res.Result.Password = Crypto.Hash(data.Password.ToString(), "MD5");
            }

            if(Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdate, "Kullanıcı profili güncellenemedi");
            }

            return res;
            
        }

        public BusinessLayerResult<Users> GetUserById(int? id)
        {
            res.Result = Find(x => x.Id == id.Value);

            if(res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı");
            }
            return res;
        }

        public BusinessLayerResult<Users> LoginUser(LoginViewModel model)
        {
            string pass = Crypto.Hash(model.Password.ToString(), "MD5");
            res.Result = Find(x => x.Username == model.Username && x.Password == pass);

            if(res.Result == null)
            {
                res.AddError(ErrorMessageCode.EmailOrPassWrong, "Kullanıcı adı yada şifre uyuşmuyor");
            }

            return res;
        }

        public BusinessLayerResult<Users> DeleteUserById(int id)
        {
            res.Result = Find(x => x.Id == id);

            if (res.Result != null)
            {
                if (Delete(res.Result) == 0)
                {
                    res.AddError(ErrorMessageCode.CouldNotDeleteUser, "Kullanıcı silinemedi");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı");
            }

            return res;
        }

        public BusinessLayerResult<Users> ForgetPassword(ForgetPassViewModel data)
        {
            res.Result = Find(x => x.Email == data.Email);

            if(res.Result == null)
            {
                res.AddError(ErrorMessageCode.CouldNotEmail, "Sistemde böyle bir mail kayıtlı değil");
            }
            else
            {
                int newnumber = 0;
                Random rnd = new Random();
                newnumber = rnd.Next();
                res.Result.Password = Crypto.Hash(Convert.ToString(newnumber), "MD5");

                int dbResult = Update(res.Result);

                if(dbResult > 0)
                {
                    string body = $"Merhaba {res.Result.Name} {res.Result.Surname}; <br> <br> Yeni Şİfre : {newnumber}";
                    MailHelper.SendMail(body, res.Result.Email, "Doğu Zemin Laboratuvar - Yeni Şİfre Talebi", true);
                }
                else
                {
                    res.AddError(ErrorMessageCode.CouldNotSendPassword, "Şifre gönderilirken hata oluştu!");
                }

            }
            return res;
        }
    }
}
