using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MongoGetdataApi.Common
{
    public class AjaxHandle
    {
        public static string Database = "OrderSys";
        public static string tbName = "order";

        /// <summary>
        /// 获取数据总条数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string  GetTotal()
        {
            string data = "[";
            Common com = new Common(Database, tbName);
            var count = com.GetCountAsync<Order>(a => true).Result.ToString();
            data += "{\"Total\":"+count+"}]";

            return "{\"status\":\"1\",\"data\":"+data+",\"message\":\"请求成功\"}";
        }

        public static string AddOrders()
        {
            string data = "[";
            Common com = new Common(Database, tbName);
            var orderId = com.Add();
            data += "{\"orderId\":\"" + orderId + "\"}]";

            return "{\"status\":\"1\",\"data\":" + data + ",\"message\":\"请求成功\"}";
        }

        public static string SerchByTime(string fromTime,  string pageIndex, string pageSize, string endTime = "") {
            try
            {
                DateTime dtFrom = DateTime.Parse(fromTime);
                DateTime dtEnd = DateTime.Parse(endTime==""?DateTime.Now.ToString():endTime);
                int pageI = int.Parse(pageIndex);
                int pageS = int.Parse(pageSize);
                Common com = new Common(Database, tbName);
               
                var item = com.GetItemAsync<Order>(a => a.OrderDateTime > dtFrom && a.OrderDateTime < dtEnd,pageI,pageS).Result.ToList();
                var data = "[";
                StringBuilder sb = new StringBuilder();
                item.ForEach(p =>
                {
                    sb.Append( "{\"orderId\":\""+ p.orderId + "\",\"goodsInfo\":[{\"goodsNo\":\""+ p.goodsInfo.GoodsNo + "\",\"goodsName\":\""+ p.goodsInfo.GoodsName + "\",\"price\":\""+ p.goodsInfo.Price + "\"}],\"orderDatetime\":\""+ p.OrderDateTime + "\",\"count\":\""+ p.Count + "\",\"total\":\""+ p.Total + "\"},");
                    
                });
                data += sb.ToString().TrimEnd(',') + "]";
                return "{\"status\":\"1\",\"data\":" + data + ",\"message\":\"请求成功\"}";
            }
            catch (Exception)
            {

                return "{\"status\":\"0\",\"data\":\"\",\"message\":\"请求失败,数据不合法\"}";
            }
        }
    }
}