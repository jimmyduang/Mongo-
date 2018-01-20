using MongoGetdataApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoGetdataApi
{
    /// <summary>
    /// GetDataAPI 的摘要说明
    /// </summary>
    public class GetDataAPI : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            if (String.IsNullOrWhiteSpace(action))
            {
                context.Response.Write("{\"status\":\"0\",\"data\":\"\",\"message\":\"非法请求\"}");
            }
            else {
                if (action == "GetTotal")
                {
                    //获取总条数
                    context.Response.Write(AjaxHandle.GetTotal());
                }
                else if (action == "AddOrder")
                {
                    //添加订单
                    context.Response.Write(AjaxHandle.AddOrders());
                }
                else if (action == "SerchByTime")
                {
                    //时间查询订单
                    string fromTime = context.Request.Params["fromTime"];
                    string endTime = context.Request.Params["endTime"];
                    string pageIndex = context.Request.Params["pageIndex"];
                    string pageSize = context.Request.Params["pageSize"];
                    context.Response.Write(AjaxHandle.SerchByTime(fromTime, pageIndex, pageSize, endTime));
                }
                else {
                    context.Response.Write("{\"status\":\"0\",\"data\":\"\",\"message\":\"非法请求\"}");
                }
            }

            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}