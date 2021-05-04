using Core.DataAccess;
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
   public class EfCarDal : EfEntityRepositoryBase<Car,RentACarContext>,ICarDal
    {

        public CarDetailDto GetCarDetails(int carId)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from car in context.Cars.Where(c => c.CarId == carId)

                             join color in context.Colors
                             on car.ColorId equals color.ColorId

                             join brand in context.Brands
                             on car.BrandId equals brand.BrandId

                             select new CarDetailDto()
                             {
                                 CarId = car.CarId,
                                 Descriptions = car.Descriptions,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 BrandId = brand.BrandId,
                                 BrandName = brand.BrandName,
                                 ColorId = color.ColorId,
                                 ColorName = color.ColorName
                             };

                        return result.SingleOrDefault();
            }
        }

        public List<CarDetailDto> GetCarsDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from car in context.Cars

                             join color in context.Colors
                             on car.ColorId equals color.ColorId

                             join brand in context.Brands
                             on car.BrandId equals brand.BrandId

                             select new CarDetailDto()
                             {
                                 CarId = car.CarId,
                                 Descriptions = car.Descriptions,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 BrandId = brand.BrandId,
                                 BrandName = brand.BrandName,
                                 ColorId = color.ColorId,
                                 ColorName = color.ColorName
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }
    }
}
