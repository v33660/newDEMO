using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface ILoginService
    {
        Task<List<LoginResposeModel>> GetAllLoginList();

        Task<LoginResposeModel> Addlogin(AddLoginRequest loginRequest);
    }
}
