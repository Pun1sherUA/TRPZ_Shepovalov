using System.Linq;
using AutoMapper;
using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class TicketService : ITicketService
    {
        private IInMemoryStorage _inMemoryStorage;
        private IMapper _mapper;

        public TicketService(IInMemoryStorage inMemoryStorage, IMapper mapper)
        {
            _inMemoryStorage = inMemoryStorage;
            _mapper = mapper;
        }

        public Ticket TakeTicket(Ticket ticket)
        {
            if (_inMemoryStorage.Tickets.LastOrDefault() != null)
                // ReSharper disable once PossibleNullReferenceException
                ticket.Number = ++_inMemoryStorage.Tickets.LastOrDefault().Number;
            else
                ticket.Number = 1;

            var ticketEntity = _mapper.Map<TicketEntity>(ticket);
            _inMemoryStorage.Tickets.Add(ticketEntity);
            _inMemoryStorage.Save();
            return ticket;
        }
    }
}