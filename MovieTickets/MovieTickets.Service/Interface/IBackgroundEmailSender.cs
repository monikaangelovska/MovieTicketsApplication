using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Service.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
