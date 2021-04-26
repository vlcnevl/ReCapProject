using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [TransactionScopeAspect]
        [SecuredOperation("brand.add,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
                _brandDal.Add(brand);
                return new SuccessResult(Messages.AddedBrand);
        }

        [SecuredOperation("brand.delete,admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.DeletedBrand);
        }
        [CacheAspect]
        [SecuredOperation("admin")]
        [PerformanceAspect(5)] 
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(c => c.BrandId == id));
        }

        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            if (brand.BrandName.Length > 2)
            {
                _brandDal.Update(brand);
                return new SuccessResult(Messages.UpdatedBrand);
            }
            return new ErrorResult(Messages.FailedBrandAddOrUpdate);
        }
    }
}
