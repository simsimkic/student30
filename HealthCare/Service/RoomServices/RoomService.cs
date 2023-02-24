/***********************************************************************
 * Module:  RoomService.cs
 * Purpose: Definition of the Class Service.RoomService
 ***********************************************************************/

using Model.DataFiltration;
using Model.Rooms;
using Repository.AppointmentRepository;
using Repository.RoomRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.RoomServices
{
    public class RoomService : IRoomService
    {
        public Repository.RoomRepositories.IRoomRepository _roomRepository;
        public Repository.RoomRepositories.IInventoryAmountRepository _inventoryAmountRepository;
        public IInventoryService _inventoryService;
        public IRenovateRepository _renovateRepository;
        public IAppointmentRepository _appointmentRepository;

        public RoomService(IRoomRepository repository, IInventoryAmountRepository amountRepository, 
                            IInventoryService inventoryService, IRenovateRepository renovateRepository, IAppointmentRepository appointmentRepository)
        {
            _roomRepository = repository;
            _inventoryAmountRepository = amountRepository;
            _inventoryService = inventoryService;
            _renovateRepository = renovateRepository;
            _appointmentRepository = appointmentRepository;
        }

        public Room Create(Room entity) => _roomRepository.Create(entity);

        public Room Delete(Room entity)
        {
            if (!DoesRoomHavePatients(entity) && !_appointmentRepository.DoesRoomHasFutureAppointment(entity)) {
                _inventoryService.MoveInventoryBackToStorage(_inventoryAmountRepository.GetInventoryFromRoom(entity));
                _renovateRepository.DeleteAllRoomRenovations(entity);
                return _roomRepository.Delete(entity);
            }
            return null;
        }

        private bool DoesRoomHavePatients(Room entity) => GetFromID(entity.RoomNumber).patients.Count() > 0;

        public IEnumerable<Room> GetAll() => _roomRepository.GetAll();

        public IEnumerable<Room> GetFilteredRooms(RoomFilter roomFilter) => _roomRepository.GetFilteredRooms(roomFilter);

        public IEnumerable<InventoryAmount> GetInventoryFromRoom(Room room) => _inventoryAmountRepository.GetInventoryFromRoom(room);

        public Room GetFromID(int id) => _roomRepository.GetFromID(id);

        public Room Update(Room entity) => _roomRepository.Update(entity);
    }
}