using EtiqaFreelancerApi.Models;

namespace EtiqaFreelancerApi.Common
{
    public class UserResponseModel
    {
        public string Status { get; set; }
        public User Data { get; set; }
        public Exception Exception { get; set; }
    }
}
