using Microsoft.EntityFrameworkCore;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using Smart_Logistics_Mangment_System.Infrastruction.DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Infrastruction.Reposatories
{
    public class Reposatory<T>(Application_Context context) : IReposatory<T> where T : BaseEntity
    {
        public async Task<int> Add(T obj)
        {
            await context.AddAsync(obj);
            return obj.Id;
        }

        public async Task DeleteFromUpdate(int Id)
        {
            T? obj = await GetById(Id);

            if (obj == null)
            {
                return;
            }

            obj.IsDeleted = true;
        }


        public async Task Delete(int Id)
        {
            T obj = await GetById(Id);
            obj.IsDeleted = true;       //  soft delete
        }


        public async Task<Vehicle?> GetLastVehicle()
        {
            return await context.Vehicles
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
        }



        public async Task<int> GetAvailableVehicleId()
        {
            var vehicleId = await context.Vehicles
                .Where(v => !context.Drivers
                    .Any(d => d.VehicleId == v.Id))
                .Select(v => v.Id)
                .FirstOrDefaultAsync();

            return vehicleId;
        }



        public async Task<Driver?> CreateDriversByVehicleId(int vehicleId)
        {
            return await context.Drivers
                .FirstOrDefaultAsync(x =>
                    x.VehicleId == vehicleId &&
                    !x.IsDeleted);
        }



        public async Task<List<T>> GetAll(string Include = "")
        {
            if (Include != "")
            {
                return await context.Set<T>().Include(Include).Where(o => o.IsDeleted == false).ToListAsync();
            }
            else
            {
                return await context.Set<T>().Where(o => o.IsDeleted == false).ToListAsync();

            }
        }


        public async Task<T?> GetAllCustomersById(int id, params string[] includes)
        {
            IQueryable<T> query = context.Set<T>()
                .Where(x => !x.IsDeleted && x.Id == id);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync();
        }



        public async Task<List<T>> GetAllDrivers(params string[] includes)
        {
            IQueryable<T> query = context.Set<T>()
                .Where(x => !x.IsDeleted);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }



        public async Task<Driver?> GetDriverByUserId(int userId)
        {
            return await context.Drivers
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    !x.IsDeleted);
        }

        public async Task<T?> GetAllDriversById(int id, params string[] includes)
        {
            IQueryable<T> query = context.Set<T>()
                .Where(x => !x.IsDeleted && x.Id == id);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetAllEmployeesById(int id, params string[] includes)
        {
            IQueryable<T> query = context.Set<T>()
                .Where(x => !x.IsDeleted && x.Id == id);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync();
        }


        public async Task<ShipmentRequest?> GetByIdWithDetails(int id)
        {
          
            return await context.Set<ShipmentRequest>()
                .AsNoTracking()
                .AsSplitQuery()

                .Include(x => x.Customer)
                    .ThenInclude(x => x.User)

                .Include(x => x.PickupWarehouse)

                .Include(x => x.Items
                    .Where(i => !i.IsDeleted))

                .FirstOrDefaultAsync(x =>
                    !x.IsDeleted &&
                    x.Id == id);
        
            //return await context.Set<ShipmentRequest>()
            //    .AsNoTracking()
            //    .AsSplitQuery()

            //    .Include(x => x.Customer)
            //        .ThenInclude(x => x.User)

            //    .Include(x => x.PickupWarehouse)

            //    .Include(x => x.Items)

            //    .FirstOrDefaultAsync(x =>
            //        !x.IsDeleted &&
            //        x.Id == id);
        }





        public async Task<List<ShipmentRequest>> GetAllByCustomerIdWithDetails(int customerId,int pageNumber, int pageSize)
        {
  
            return await context.ShipmentRequests
                .Where(x =>
                    !x.IsDeleted &&
                    x.CustomerId == customerId)

                .Include(x => x.Customer)
                    .ThenInclude(x => x.User)

                .Include(x => x.PickupWarehouse)

                .Include(x => x.Items
                    .Where(i => !i.IsDeleted))

                .OrderByDescending(x => x.Id)

                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)

                .ToListAsync();
        
            //return await context.ShipmentRequests
            //    .Where(x =>
            //        !x.IsDeleted &&
            //        x.CustomerId == customerId)
            //    .Include(x => x.Customer)
            //        .ThenInclude(x => x.User)
            //    .Include(x => x.PickupWarehouse)
            //    .Include(x => x.Items)
            //    .OrderByDescending(x => x.Id)
            //    .Skip((pageNumber - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();
        }

        public async Task<List<ShipmentRequest>> GetAllWithDetails(int pageNumber,  int pageSize)
        {
       
            return await context.Set<ShipmentRequest>()
                .AsNoTracking()
                .AsSplitQuery()

                .Include(x => x.Customer)
                    .ThenInclude(x => x.User)

                .Include(x => x.PickupWarehouse)

                .Include(x => x.Items
                    .Where(i => !i.IsDeleted))

                .Where(x => !x.IsDeleted)

                .OrderByDescending(x => x.Id)

                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)

                .ToListAsync();
        
            //return await context.Set<ShipmentRequest>()
            //    .AsNoTracking()
            //    .AsSplitQuery()

            //    .Include(x => x.Customer)
            //        .ThenInclude(x => x.User)

            //    .Include(x => x.PickupWarehouse)

            //    .Include(x => x.Items)

            //    .Where(x => !x.IsDeleted)

            //    .OrderByDescending(x => x.Id)

            //    .Skip((pageNumber - 1) * pageSize)
            //    .Take(pageSize)

            //    .ToListAsync();
        }



        public async Task<List<Shipment>> GetAllShipments( int pageNumber,int pageSize)
        {
            return await context.Set<Shipment>()
                .AsNoTracking()
                .AsSplitQuery()

                .Include(x => x.Customer)
                    .ThenInclude(x => x.User)

                .Include(x => x.PickupWarehouse)

                .Include(x => x.Driver)
                    .ThenInclude(x => x.User)

                .Include(x => x.Vehicle)

                .Include(x => x.Items)

                .Where(x => !x.IsDeleted)

                .OrderByDescending(x => x.Id)

                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)

                .ToListAsync();
        }


        public async Task<Driver?> GetDriverById(int id)
        {
            return await context.Drivers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }


        public async Task<Shipment?> GetActiveShipmentByDriverId(int driverId)
        {
            return await context.Shipments
                .FirstOrDefaultAsync(x =>
                    !x.IsDeleted &&
                    x.DriverId == driverId &&
                    x.Status != ShipmentStatus.Delivered &&
                    x.Status != ShipmentStatus.Cancelled);
        }

        public async Task<Shipment?> GetActiveShipmentByVehicleId(int vehicleId)
        {
            return await context.Shipments
                .FirstOrDefaultAsync(x =>
                    !x.IsDeleted &&
                    x.VehicleId == vehicleId &&
                    x.Status != ShipmentStatus.Delivered &&
                    x.Status != ShipmentStatus.Cancelled);
        }


        public async Task<Shipment?> GetShipmentByIdWithDetails(int id)
        {
            return await context.Set<Shipment>()
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Customer)
                    .ThenInclude(x => x.User)
                .Include(x => x.PickupWarehouse)
                .Include(x => x.Driver)
                    .ThenInclude(x => x.User)
                .Include(x => x.Vehicle)
                .Include(x => x.Items
                .Where(i => !i.IsDeleted)).Include(x => x.ShipmentRequest)
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }


        public async Task<List<ShipmentItem>> GetAllShipmentItems()
        {
            return await context.Set<ShipmentItem>()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .Take(50)
                .ToListAsync();
        }



        public async Task<List<ShipmentTracking>> GetAllTracking()
        {
            return await context.Set<ShipmentTracking>()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .Take(50)
                .ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(o => o.IsDeleted == false && o.Id == Id);
        }


        public async Task<RefreshToken?> GetByRefreshToken(string refreshToken)
        {
            return await context.RefreshTokens
                .FirstOrDefaultAsync(u => u.Token == refreshToken);
        }

        public async Task<Customer?> GetByCustomerId(int userId)
        {
            return await context.Customers
                .FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted);
        }


        public async Task<ShipmentLocation?> GetLastShipmentLocation(  int shipmentId)
        {
            return await context.ShipmentLocations
                .Where(x =>
                    x.ShipmentId == shipmentId &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.RecordedAt)
                .FirstOrDefaultAsync();
        }

        public async Task AddShipmentLocation(ShipmentLocation location)
        {
            await context.ShipmentLocations.AddAsync(location);
        }




        public async Task<AppUser?> GetByEmail(string email)
        {
            return await context.Set<AppUser>()
                .FirstOrDefaultAsync(x =>
                    !x.IsDeleted &&
                    x.Email == email);
        }

        public async Task<int> Save()
        {
            int num = await context.SaveChangesAsync();
            return num;
        }

        public void Update(T obj)
        {
            context.Update(obj);
            //context.SaveChangesAsync();
        }
    }
}
