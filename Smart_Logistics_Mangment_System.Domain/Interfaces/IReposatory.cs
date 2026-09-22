using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Interfaces
{
    public interface IReposatory<T> where T : BaseEntity
    {
        public Task<int> Add(T obj);
        public void Update(T obj);
        public Task Delete(int Id);
        public Task DeleteFromUpdate(int Id);

        public Task<int> Save();
       
        public Task<Customer?> GetByCustomerId(int userId);
        
        public Task<RefreshToken?> GetByRefreshToken(string refreshToken);
        public Task<T> GetById(int Id);
        public Task<List<T>> GetAll(string Include = "");
        public Task<List<T>> GetAllDrivers(params string[] includes);
        public Task<T?> GetAllDriversById(int id, params string[] includes);
        public Task<T?> GetAllEmployeesById(int id, params string[] includes);
        Task<List<ShipmentRequest>> GetAllWithDetails(int pageNumber,int pageSize);
        Task<List<ShipmentRequest>> GetAllByCustomerIdWithDetails(int customerId,int pageNumber,int pageSize);
        Task<List<Shipment>> GetAllShipments(int pageNumber, int pageSize);
        Task<Shipment?> GetActiveShipmentByDriverId(int driverId);
        Task<Driver?> GetDriverByUserId(int userId);
        Task<Shipment?> GetActiveShipmentByVehicleId(int vehicleId);
        Task<List<ShipmentItem>> GetAllShipmentItems();
        Task<List<ShipmentTracking>> GetAllTracking();

        Task<Shipment?> GetShipmentByIdWithDetails(int id);
        Task<ShipmentRequest?> GetByIdWithDetails(int id);
        Task<AppUser?> GetByEmail(string email);
        public Task<T?> GetAllCustomersById(int id, params string[] includes);
        Task<Driver?> GetDriverById(int id);
        Task<Vehicle?> GetLastVehicle();
        Task<int> GetAvailableVehicleId();
        Task<Driver?> CreateDriversByVehicleId(int vehicleId);
        Task<ShipmentLocation?> GetLastShipmentLocation(int shipmentId);
        Task AddShipmentLocation(ShipmentLocation location);





    }
}
