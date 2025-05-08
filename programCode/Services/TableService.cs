using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Controllers;
using RestaurantReservierung.Data;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Services
{
    public class TableService
    {
        private readonly AppDbContext _context;

        public TableService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTable(TableFormModel model, int restaurantId)
        {
            var table = new Table
            {
                Area = model.Area,
                Capacity = model.Capacity,
                TableNr = model.TableNr,
                RestaurantId = restaurantId
            };

            
            _context.Tables.Add(table);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            
            return false;
        }

        public async Task<List<Table>> GetAllTables(int restaurantId)
        {
            return await _context.Tables
                .Where(t => t.RestaurantId == restaurantId)
                .ToListAsync();

        }

        public async Task<Table> GetTableById(int tableId)
        {
            return await _context.Tables.FirstOrDefaultAsync(t => t.TableId == tableId);
        }

        public async Task<bool> RemoveTable(Table table)
        {
            _context.Tables.Remove(table);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTable(Table table, TableFormModel model)
        {
            table.TableNr = model.TableNr;
            table.Area = model.Area;
            table.Capacity = model.Capacity;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
