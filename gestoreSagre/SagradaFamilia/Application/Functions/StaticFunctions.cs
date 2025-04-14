using Application.Abstraction.Requests;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Functions
{
    static public class StaticFunctions
    {
        static public void CheckUser(string userId, BaseRequest request)
        {
            if (userId == null)
            {
                throw new Exception("Utente non autorizzato");
            }
            if (request.IdUser == null)
            {
                request.IdUser = int.Parse(userId);
            }
            else if (userId != request.IdUser.ToString())
            {
                throw new Exception("L'utente non può fingersi un altro utente!");
            }
        }
        static public Ruolo GetRuoloByString(string ruolo)
        {
            if (ruolo == null)
            {
                throw new Exception("Ruolo non valido");
            }
            return Enum.Parse<Ruolo>(ruolo);
        }


    }
}
