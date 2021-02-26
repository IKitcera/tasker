using tasker.Models.TaskModel;

namespace tasker.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public TaskManager taskManager { get; set; }
    }
}
