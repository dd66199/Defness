using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User : IdentityUser, IBaseModel
    {
        public DateTime CreateDate { get ; set ; }
        public DateTime? UpdateDate { get ; set ; }
        public DateTime? DeleteDate { get; set ; }
    }
}
