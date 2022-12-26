using DemoApplication.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DemoApplication.Database.Configurations
{
    public class AdressConfigurations : IEntityTypeConfiguration<Adress>
    {
        public void Configure(EntityTypeBuilder<Adress> builder)
        {
            builder
               .ToTable("Adresses");
              

          
        }
    }
}
