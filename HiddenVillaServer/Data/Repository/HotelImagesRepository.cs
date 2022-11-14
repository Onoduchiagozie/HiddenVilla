using HiddenVillaServer.Data.Repository.IRepository;
using HiddenVillaServer.Model.MetaData;
using Microsoft.EntityFrameworkCore;

namespace HiddenVillaServer.Data.Repository;

public class HotelImagesRepository:IHotelImagesRepository
{
    private readonly VillaDbContext _db;
    public HotelImagesRepository(VillaDbContext db)
    {
        _db=db;
    }
    
    
    public async  Task<int> CreateImage(HotelImage hotelImage)
    {
        await _db.HotelImages.AddAsync(hotelImage);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteImageById(int ImageId)
    {
        var image = await _db.HotelImages.FindAsync(ImageId);
        _db.HotelImages.Remove(image);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteImageByIdByRoomId(int roomId)
    {
        // ReSharper disable once HeapView.BoxingAllocation
        var roomImages = await _db.HotelImages.Where(x => x.RoomId == roomId).ToListAsync();
        _db.HotelImages.RemoveRange(roomImages);
        return await _db.SaveChangesAsync();

    }

    public async Task<int> DeleteImageByIdByImageUrl(string imageUrl)
    {
        var allImages = await _db.HotelImages.FirstOrDefaultAsync(
            x => x.ImageUrl.ToLower() == imageUrl.ToLower()
            );
        _db.HotelImages.Remove(allImages);
        return await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<HotelImage>> GetRoomImages(int roomId)
    {
        return await _db.HotelImages.Where(x => x.RoomId == roomId).ToListAsync();
    }
}