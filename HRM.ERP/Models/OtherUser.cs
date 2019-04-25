using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HRM.ERP.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class OtherUser : BaseClass
    {
public Guid Id { get; set; }    
    public Guid OrganisationId { get; set; }
    public Guid UserId { get; set; }
    public Guid HostId { get; set; }
     
        //public string Password { get; set; }
        //public string ComfirmPassword { get; set; }
    }
}
