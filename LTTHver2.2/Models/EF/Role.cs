namespace LTTHver2._2.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role : Microsoft.AspNet.Identity.EntityFramework.IdentityRole
    {
        
    }
}
