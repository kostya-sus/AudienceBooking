using System;
using System.Collections.Generic;
using System.Linq;
using Booking.Models.EfModels;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;

namespace Booking.Services.Services
{
    public class AudienceMapService : IAudienceMapService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AudienceMapService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AudienceMap> GetAllAudienceMaps()
        {
            return _unitOfWork.AudienceMapRepository.GetAllAudienceMaps();
        }

        public AudienceMap GetAudienceMap(Guid id)
        {
            return _unitOfWork.AudienceMapRepository.GetAudienceById(id);
        }

        public bool Exists(Guid id)
        {
            return _unitOfWork.AudienceMapRepository.GetAllAudienceMaps().Any(map => map.Id == id);
        }

        public void CreateAudienceMap(AudienceMap audienceMap)
        {
            _unitOfWork.AudienceMapRepository.CreateAudienceMap(audienceMap);
            _unitOfWork.Save();
        }

        public void DeleteAudienceMap(AudienceMap audienceMap)
        {
            _unitOfWork.AudienceMapRepository.DeleteAudienceMap(audienceMap);
            _unitOfWork.Save();
        }
    }
}