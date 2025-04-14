using CarParkSG.Models;
using CarParkSG.Models.ViewModel;
using Newtonsoft.Json;

public static class CarParkExtensions
{
    public static CarParkDataModelView FillSlotsInfo(this HDBCarParkModel carParkInfo, CarParkAvailabilityModel availabilityInfo)
    {
        if (carParkInfo != null && carParkInfo.result != null && carParkInfo.result.records != null
                            && availabilityInfo != null && availabilityInfo.items != null)
        {
            carParkInfo.result.records = carParkInfo.result.records.GroupBy(cp => new
            {
                cp.address,
                cp.car_park_type,
                cp.car_park_no,
                cp.free_parking,
                cp.lot_type,
                cp.night_parking,
                cp.short_term_parking,
                cp.type_of_parking_system,
                cp.x_coord,
                cp.y_coord
            }).Select(cp => new Record()
            {
                address = cp.Key.address,
                car_park_type = cp.Key.car_park_type,
                car_park_no = cp.Key.car_park_no,
                free_parking = cp.Key.free_parking,
                lot_type = cp.Key.lot_type,
                night_parking = cp.Key.night_parking,
                short_term_parking = cp.Key.short_term_parking,
                type_of_parking_system = cp.Key.type_of_parking_system,
                x_coord = cp.Key.x_coord,
                y_coord = cp.Key.y_coord
            }).ToList();
            carParkInfo.result.records.ForEach(x =>
            {
                if (availabilityInfo.items[0].carpark_data.Where(a => a.carpark_number == x.car_park_no).Any())
                {
                    var availableLots = availabilityInfo.items[0].carpark_data.Where(a => a.carpark_number == x.car_park_no).FirstOrDefault();
                    x.available_lots = availableLots != null ? availableLots.carpark_info.Select(a => $"{a.lots_available} lots available out of {a.total_lots}({a.lot_type}).").ToList() : new List<string>();
                    x.total_available_lot = availableLots != null ? availableLots.carpark_info.Sum(a => a.lots_available) : 0;
                }
                else
                {
                    x.available_lots = new List<string> { "No data" };
                    x.total_available_lot = 0;
                }
            });
        }


        if (carParkInfo != null && carParkInfo.result != null && carParkInfo.result.records != null)
        {
            carParkInfo.result.records = carParkInfo.result.records.OrderBy(a => a.address).ToList();
        }

        CarParkDataModelView cp = new CarParkDataModelView();
        cp.carParkDetail = JsonConvert.SerializeObject(carParkInfo);
        if (carParkInfo.success && carParkInfo.result != null && carParkInfo.result.records != null)
        {
            cp.car_park_type = carParkInfo.result.records.GroupBy(a => a.car_park_type).Select(a => a.Key).ToList();
            cp.free_parking = carParkInfo.result.records.GroupBy(a => a.free_parking).Select(a => a.Key).ToList();
            cp.night_parking = carParkInfo.result.records.GroupBy(a => a.night_parking).Select(a => a.Key).ToList();
            cp.short_term_parking = carParkInfo.result.records.GroupBy(a => a.short_term_parking).Select(a => a.Key).ToList();
            cp.car_park_basement = carParkInfo.result.records.GroupBy(a => a.car_park_basement).Select(a => a.Key).ToList();
            cp.gantry_height = carParkInfo.result.records.GroupBy(a => a.gantry_height).Select(a => a.Key).ToList();
        }
        return cp;
    }
}