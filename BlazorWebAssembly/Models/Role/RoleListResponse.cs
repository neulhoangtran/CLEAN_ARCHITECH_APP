
namespace BlazorWebAssembly.Models.Role
{
    public class RoleListResponse
    {
        public List<RoleModel> Data { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
