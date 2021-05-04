using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<List<Car>> GetAllCars();
        IDataResult<Car> GetById(int id);
        IDataResult<List<CarDetailDto>> GetByColor(int colorId);
        IDataResult<List<Car>> GetByDailyPrice(decimal mininum, decimal maximum);
        IDataResult<List<CarDetailDto>> GetAllCarsWithDetails();
        IDataResult<List<CarDetailDto>> GetByBrand(int brandId);
        IDataResult<CarDetailDto> GetCarDetalis(int carId);
        IDataResult<List<CarDetailDto>> GetCarsBrandAndColor(int brandId, int colorId);

    }
}
