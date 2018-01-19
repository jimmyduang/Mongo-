using FrameWork.Extension;
using FrameWork.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace MongoGetdataApi.Common
{
    public class Common
    {

        private MongoClient _mongoClient;
        private string _connString = "MongoDb".ValueOfAppSetting();
        private string _database;
        private string _tbName;
        public Common(string Database,string tbName) {
            _mongoClient = new MongoClient(_connString);
            _database = Database;
            _tbName = tbName;
        }

        /// <summary>
        /// 按条件查询指定页面指定条数数据
        /// </summary>
        /// <typeparam name="Order"></typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="pageindex">页数</param>
        /// <param name="pagesize">条数</param>
        /// <returns>查询结果</returns>
        public  async Task<List<Order>> GetItemAsync<Order>(Expression<Func<Order, bool>> predicate, int pageindex, int pagesize)
        {
            var db = _mongoClient.GetDatabase(_database);
            var coll = db.GetCollection<Order>(_tbName);
            var find = coll.Find(predicate);
            find = find.Skip((pageindex - 1) * pagesize).Limit(pagesize);
            var items = await find.ToListAsync().ConfigureAwait(false);
            return items.ToList();
        }

        /// <summary>
        ///获取指定条件下的总数据
        /// </summary>
        /// <typeparam name="Order"></typeparam>
        /// <param name="predicate">查询条件</param>
        /// <returns>查询结果</returns>
        public  async Task<int> GetCountAsync<Order>(Expression<Func<Order, bool>> predicate)
        {
            var db = _mongoClient.GetDatabase(_database);
            var coll = db.GetCollection<Order>(_tbName);
            var count = (int)await coll.CountAsync<Order>(predicate).ConfigureAwait(false);
            return count;
        }


        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public string Add()
        {
            var id = Guid.NewGuid().ToString();
            string No = new Random().Next(1000, 9999).ToString();
            var _Price = int.Parse(No) % 800;
            var _Count = new Random().Next(1, 10);
            try
            {
                var order = new Order
                {
                    _id = id,
                    orderId = DateTime.Now.ToString("yyyyMMddHHmmssfff") + No,
                    goodsInfo = new GoodsInfo
                    {
                        GoodsNo = No,
                        GoodsName = "goodsno-T" + No,
                        Price = _Price
                    },
                    Count = _Count,
                    Total = _Price * _Count,
                    OrderDateTime = DateTime.Now
                };
                new MongoDbService().Add(order);
                return order.orderId;
            }
            catch (Exception)
            {

               
            }

            return "插入错误";
        }
    }
}