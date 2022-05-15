namespace LTTHver2._2.Models.EF
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class User : Microsoft.AspNet.Identity.EntityFramework.IdentityUser
    {
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            BlogComments = new HashSet<BlogComment>();
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        
        

        
        [StringLength(50)]
        public string Password { get; set; }

        
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        
        

        public string Description { get; set; }

        public string Image { get; set; }

        public long? BirthDay { get; set; }

        [StringLength(100)]
        public string Career { get; set; }

        public long CreatedDay { get; set; }

        public int Level { get; set; }

        public int IDChucVu { get; set; }

        public int? Wallet { get; set; }

        public bool? RequiresVerification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BlogComment> BlogComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}
