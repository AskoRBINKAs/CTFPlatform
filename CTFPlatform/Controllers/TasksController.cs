using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using CTFPlatform.Models;
using System.Security.Claims;

namespace CTFPlatform.Controllers
{
    [ApiController]
    [Route("v1/tasks")]
    [Authorize(Roles = "Admin,User")]
    public class TasksController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public TasksController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("")]
        public IActionResult GetAllTasks()
        {
            var role = GetRole();
            if (role != "Admin")
            {
                if (!ContestSettings.CTF.Started)
                {
                    return BadRequest("Contest not started yet");
                }
            }
            List<TaskModel> response = new List<TaskModel>();
            var tasks = from b in _dbContext.Tasks select b;
            foreach(var task in tasks)
            {
                if(task.IsVisible)
                {
                    if (role != "Admin") task.Flag = "--hidden--";
                    response.Add(task);
                }
            }
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTaskById(int id)
        {
            if (GetRole() != "Admin")
            {
                if(!ContestSettings.CTF.Started)
                {
                    return Forbid("Contest not started yet");
                }
            }
            var role = GetRole();
            var task = (from b in _dbContext.Tasks where b.Id == id select b).FirstOrDefault();
            if (task == null)
            {
                return NotFound("task not found");
            }
            if (role != "Admin") task.Flag = "--hidden--";
            return Ok(task);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTask(TaskModelDTO taskDTO)
        {
            Console.WriteLine(HttpContext.User.Claims.ToString());
            TaskModel task = new TaskModel();
            task.Flag = taskDTO.Flag;
            task.Name = taskDTO.Name;
            task.Description = taskDTO.Description;
            task.Type = taskDTO.Type;
            task.Cost = taskDTO.Cost;
            task.Attachments = taskDTO.Attachments;
            task.DeployedEnvironment = taskDTO.DeployedEnv;
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
            return Ok(task);
        }

        [HttpGet("solved/{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetSolvedTasks(int id)
        {
            var user = (from u in _dbContext.Users where u.Id == id select u).FirstOrDefault();
            if (user == null) return NotFound("user not found");
            return Ok(user.SolvedTasks);
        }
        
        [HttpPost("submit/{id:int}")]
        public IActionResult SubmitFlag(int id,string flag)
        {
            if (!ContestSettings.CTF.Started) return Forbid("Contest not yet started");
            return SumbitFlagAsSoloUser(id, flag);
        }
        private IActionResult SumbitFlagAsSoloUser(int id,string flag)
        {
            var task = (from b in _dbContext.Tasks where b.Id == id select b).FirstOrDefault() as TaskModel;
            var login = HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType);
            if (login == null)
            {
                return BadRequest("user does not exists");
            }
            var user = (from b in _dbContext.Users where b.Login == login.Value select b).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("user not found");
            }
            if (task == null)
            {
                return NotFound("task not found");
            }
            if (task.Flag == flag)
            {
                foreach (var taskId in user.SolvedTasks.Split(";"))
                {
                    if (taskId == id.ToString()) return BadRequest("you already submited this flag");
                }
                user.SolvedTasks += id.ToString() + ";";
                user.Score += task.Cost;
                task.Solves += 1;
                _dbContext.Tasks.Update(task);
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
                return Ok("success");
            }
            return BadRequest("wrong flag");
        }
        private string GetRole()
        {
            var roleContext = HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
            if (roleContext == null)
            {
                return "nobody";
            }
            var role = roleContext.Value;
            return role;
        }

    }
}
