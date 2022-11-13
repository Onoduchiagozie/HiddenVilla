using HiddenVillaServer.Model.MetaData;

namespace HiddenVillaServer.Data.Repository.IRepository;

public interface IHotelImagesRepository
{
    public Task<int> CreateImage(HotelImage hotelImage);
    public Task<int> DeleteImageById(int ImageId);
    public Task<int> DeleteImageByIdByRoomId(int roomId);
    public Task<IEnumerable<HotelImage>> GetRoomImages(int roomId);

}