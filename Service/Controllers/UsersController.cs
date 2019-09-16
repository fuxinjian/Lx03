using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Json(DAL.UserInfo.Instance.GetCount());
        }

        // GET api/<controller>/5
        [HttpPut("{username}")]
        public ActionResult getUser(string username)
        {
            var result = DAL.UserInfo.Instance.GetModel(username);
            if (result != null)
                return Json(Result.Ok(result));
            else
                return Json(Result.Err("用户名不存在"));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Model.UserInfo users)
        {
            try
            {
                int n = DAL.UserInfo.Instance.Add(users);
                return Json(Result.Ok("添加成功"));
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("primary"))
                    return Json(Result.Err("用户名已存在"));
                else if (ex.Message.ToLower().Contains("null"))
                    return Json(Result.Err("用户名、密码、身份证不能为空"));
                else
                    return Json(Result.Err(ex.Message));
            }
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
      
            public ActionResult Put([FromBody]Model.UserInfo users)
            {
                try
                {
                    int n = DAL.UserInfo.Instance.Update(users);
                if(n!=0)

                    return Json(Result.Ok("修改成功"));
                else
                    return Json(Result.Err("用户名不存在"));
            }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Contains("null"))
                        return Json(Result.Err("密码、身份证不能为空"));
                    else
                        return Json(Result.Err(ex.Message));
                }
            }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string username)
        {
            try
            {
                int n = DAL.UserInfo.Instance.Delete(username);
                if (n != 0)

                    return Json(Result.Ok("删除成功"));
                else
                    return Json(Result.Err("用户名不存在"));
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("foreign"))
                    return Json(Result.Err("发不了作品或活动的用户不能删除"));
                else
                    return Json(Result.Err(ex.Message));
            }
        }
        [HttpPost("check")]

        public ActionResult UserCheck([FromBody]Model.UserInfo users)
        {
            try
            {
                var result = DAL.UserInfo.Instance.GetModel(users.userName);
                if (result == null)

                    return Json(Result.Err("用户名错误"));
                else if (result.passWord == users.passWord)
                {
                    if (result.type == "管理员")
                    {
                        result.passWord = "*********";
                        return Json(Result.Ok("管理员登录成功", result));
                    }
                    else
                        return Json(Result.Err("只有管理员能够进入后台管理"));
                }
                else
                    return Json(Result.Err("密码错误"));
            }
            catch (Exception ex)
            {
                 
                    return Json(Result.Err(ex.Message));
            }
        }
        [HttpPost("gencheck")]

        public ActionResult genUserCheck([FromBody]Model.UserInfo users)
        {
            try
            {
                var result = DAL.UserInfo.Instance.GetModel(users.userName);
                if (result == null)

                    return Json(Result.Err("用户名错误"));
                else if (result.passWord == users.passWord)
                {
                   
                        result.passWord = "*********";
                        return Json(Result.Ok("登录成功", result));
                    
                
                }
                else
                    return Json(Result.Err("密码错误"));
            }
            catch (Exception ex)
            {

                return Json(Result.Err(ex.Message));
            }
        }
    }
}
