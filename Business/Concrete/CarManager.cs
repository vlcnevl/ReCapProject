using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.AddedCar);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.DeletedCar);
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAllCars()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetAllCarsWithDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails());
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetByBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails(c=> c.BrandId == brandId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetByColor(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails(c => c.ColorId == colorId));
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetByDailyPrice(decimal mininum, decimal maximum)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.DailyPrice >=mininum && c.DailyPrice<=maximum));
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id));
        }

        [CacheAspect]
        public IDataResult<CarDetailDto> GetCarDetalis(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetails(carId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarsBrandAndColor(int brandId, int colorId)
        {
            List<CarDetailDto> cars = (_carDal.GetCarsDetails(c => c.BrandId == brandId && c.ColorId == colorId));

            if(cars==null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.CarNotFound);
            }

            return new SuccessDataResult<List<CarDetailDto>>(cars);
        }

    }
}
