using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Enums;
using Booking.Models;
using Booking.Models.EfModels;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;

namespace Booking.Services.Services
{
    public class AudienceService : IAudienceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AudienceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Audience GetAudience(Guid audienceId)
        {
            return _unitOfWork.AudienceRepository.GetAudienceById(audienceId);
        }

        public void CreateAudience(Audience audience)
        {
            _unitOfWork.AudienceRepository.CreateAudience(audience);
            _unitOfWork.Save();
        }

        public void UpdateAudience(Audience audience)
        {
            _unitOfWork.AudienceRepository.UpdateAudience(audience);
            _unitOfWork.Save();
        }

        public void DeleteAudienceById(Guid id)
        {
            _unitOfWork.AudienceRepository.DeleteAudienceById(id);
            _unitOfWork.Save();
        }

        public void CloseAudience(Guid audienceId)
        {
            var currentAudience = _unitOfWork.AudienceRepository.GetAudienceById(audienceId);
            if (currentAudience != null)
            {
                currentAudience.IsBookingAvailable = false;
                _unitOfWork.AudienceRepository.UpdateAudience(currentAudience);
                _unitOfWork.Save();
            }
        }

        public void OpenAudience(Guid audienceId)
        {
            var currentAudience = _unitOfWork.AudienceRepository.GetAudienceById(audienceId);
            if (currentAudience != null)
            {
                currentAudience.IsBookingAvailable = true;
                _unitOfWork.AudienceRepository.UpdateAudience(currentAudience);
                _unitOfWork.Save();
            }
        }

        public bool IsFree(Guid audienceId, DateTime eventStart, DateTime eventEnd, Guid? currentEventId)
        {
            var events =
                _unitOfWork.EventRepository.GetAllEvents()
                    .Where(x => x.AudienceId == audienceId && x.Id != currentEventId.Value);

            if (eventStart.DayOfWeek != 0 && (int) eventStart.DayOfWeek != 6)
            {
                if ((eventEnd.Hour < (int) BookingHoursBoundsEnum.Upper ||
                     (eventEnd.Hour == (int) BookingHoursBoundsEnum.Upper & eventEnd.Minute == 0))
                    && eventStart.Hour >= (int) BookingHoursBoundsEnum.Lower)
                {
                    foreach (var currentEvent in events)
                    {
                        var endOfCurrentEvent = currentEvent.EndTime;
                        if (eventStart <= currentEvent.StartTime && currentEvent.StartTime < eventEnd)
                        {
                            return false;
                        }
                        if (eventStart > currentEvent.StartTime && eventStart < endOfCurrentEvent)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Audience> GetAllAudiences()
        {
            return _unitOfWork.AudienceRepository.GetAllAudiences();
        }

        public string GetStyleString(Audience audience)
        {
            return string.Format("left:{0}px; top: {1}px; width: {2}px; height: {3}px",
                audience.Left, audience.Top, audience.Width, audience.Height);
        }
    }
}