using System.Text.Json.Serialization;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Dtos
{
    public class TableDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? TableId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? RestaurantId { get; set; }

        public int? TableNr { get; set; }

        public int? Capacity { get; set; }

        public string? Area { get; set; }

        public static TableDto MapToDto(Table table)
        {
            if (table != null)
            {
                return new TableDto
                {
                    TableId = table.TableId,
                    TableNr = table.TableNr,
                    Capacity = table.Capacity,
                    Area = table.Area,
                };
            }
            else
            {
                return null;
            }
        }

        public static List<TableDto> MapToDtos(List<Table> tables)
        {
            var tableDtos = new List<TableDto>();

            foreach (var table in tables)
            {
                tableDtos.Add(MapToDto(table));
            }

            return tableDtos;
        }
    }
}
