using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
   public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal brandDal)
        {
            _colorDal = brandDal;
        }

        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(Color entity)
        {
            _colorDal.Add(entity);
            return new SuccessResult(Messages.AddedColor);
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.DeletedColor);

        }

        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());

        }

        [CacheAspect]
        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == id));
        }

        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color entity)
        {
            _colorDal.Update(entity);
            return new SuccessResult(Messages.UpdatedColor);

        }
    }
}
