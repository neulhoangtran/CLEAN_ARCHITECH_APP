namespace BlazorWebAssembly.Models.User
{
    public class UserListResponse
    {
        public List<UserModel> Data { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
