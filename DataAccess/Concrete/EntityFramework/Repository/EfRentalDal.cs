using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Repository
{
   public class EfRentalDal : EfEntityRepositoryBase<Rental,RentACarContext>,IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from rental in filter == null ? context.Rentals : context.Rentals.Where(filter)
                            
                             join car in context.Cars
                             on rental.CarId equals car.CarId

                             join customer in context.Customers
                             on rental.CustomerId equals customer.CustomerId

                             join brand in context.Brands
                             on car.BrandId equals brand.BrandId

                             join user in context.Users
                             on customer.UserId equals user.Id

                             select new RentalDetailDto
                             {
                                 RentalId = rental.RentalId,
                                 CarId = car.CarId,
                                 BrandName = brand.BrandName,
                                 CompanyName = customer.CompanyName,
                                 UserName = user.FirstName + " " + user.LastName,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
