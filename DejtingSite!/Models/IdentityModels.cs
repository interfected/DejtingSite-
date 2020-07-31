using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DejtingSite_.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Profile { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //public ICollection<Posts> PostsMade { get; set; }
        //public ICollection<Posts> PostsGotten { get; set; }
        //public ICollection<Friends> Friend1 { get; set; }
        //public ICollection<Friends> Friend2 { get; set; }
        //public ICollection<FriendRequests> UserSentFriendRequest { get; set; }
        //public ICollection<FriendRequests> UserReceivedFriendRequest { get; set; }
    }

    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string UserPostedId { get; set; }
        public string UserProfileId { get; set; }
        public string Comment { get; set; }
        //public virtual ApplicationUser UserPosted { get; set; }
        //public virtual ApplicationUser UserProfile { get; set; }

    }

    public class Friends
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }
        //public virtual ApplicationUser User1 { get; set; }
        //public virtual ApplicationUser User2 { get; set; }
    }

    public class FriendRequests
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string UserSentId { get; set; }
        public string UserReceivedId { get; set; }
        //public virtual ApplicationUser UserSent { get; set; }
        //public virtual ApplicationUser UserReceived { get; set; }
    }

    public class OwnContext : DbContext
    {
        public OwnContext() : base("DbTables")
        {
            //this.Database.
            //Database.SetInitializer<OwnContext>(null); // Remove default initializer
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public virtual DbSet<Friends> Friends { get; set; }
        public virtual DbSet<FriendRequests> FriendRequests { get; set; }
        public virtual DbSet<Post> PostLista { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Friends>();
            modelBuilder.Entity<FriendRequests>();
            modelBuilder.Entity<Post>();
        }
        //public System.Data.Entity.DbSet<DatingSite.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}