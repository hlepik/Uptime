using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ReservationMapper : BaseMapper<DAL.App.DTO.Reservation, Domain.App.Reservation>,
        IBaseMapper<DAL.App.DTO.Reservation, Domain.App.Reservation>
    {
        public ReservationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}